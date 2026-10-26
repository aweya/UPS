using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private PuzzleGrid grid;
    [SerializeField] private PackageData[] packageTypes;
    [SerializeField] private LayerMask puzzleMask;
    [SerializeField] private GameObject test;
    private GridPackage activePackage;
    private PlayerInput playerInput;
    private int currentPackageType = 0;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        SelectPackage(packageTypes[currentPackageType]);
    }

    void Update()
    {
        if (activePackage != null)
        {
            HandlePreview();
        }
    }

    private void HandlePreview()
    {
        activePackage.MovePackage(GetMousePosition());
        if (grid.PlacementState(activePackage.PlacementPointCoordinates()) == CellState.EMPTY)
        {
            activePackage.MovePackage(GetGridSnapPosition(activePackage.PlacementPointCoordinates(), 0.1f));
        }
        else if (grid.PlacementState(activePackage.PlacementPointCoordinates()) == CellState.STACKABLE)
        {
            activePackage.MovePackage(GetGridSnapPosition(activePackage.PlacementPointCoordinates(), 1.1f));
        }
    }

    private void SelectPackage(PackageData data)
    {
        if (activePackage != null)
        {
            Destroy(activePackage.gameObject);
        }
        activePackage = Instantiate(data.gridPackage.gameObject, GetMousePosition(), Quaternion.identity).GetComponent<GridPackage>();
        activePackage.Initialize(data);
    }

    private void PlacePackage()
    {
        Debug.Log("Package placed");
        if (activePackage != null)
        {
            List<Vector3> allPlacementPoints = activePackage.PlacementPointCoordinates();
            float height = 0;
            if (grid.PlacementState(allPlacementPoints) == CellState.EMPTY)
            {
                height = 0;
            }
            else if (grid.PlacementState(allPlacementPoints) == CellState.STACKABLE)
            {
                height = 1;
            }
            grid.PlacePackage(activePackage, activePackage.AllPlacementPoints());
            Instantiate(activePackage.gameObject, GetGridSnapPosition(allPlacementPoints, height), activePackage.transform.rotation);
            activePackage = null;
            SelectPackage(packageTypes[currentPackageType]);
        }
    }

    private Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Plane ground = new(Vector3.up, Vector3.zero);
        if (ground.Raycast(ray, out float dist))
        {
            return ray.GetPoint(dist);
        }
        return Vector3.zero;
    }

    private Vector3 GetGridSnapPosition(List<Vector3> placementPoints, float height)
    {
        List<int> x = placementPoints.Select(p => Mathf.FloorToInt(p.x)).ToList();
        List<int> z = placementPoints.Select(p => Mathf.FloorToInt(p.z)).ToList();
        float snapX = (x.Min() + x.Max()) / 2f + grid.CellSize / 2f;
        float snapZ = (z.Min() + z.Max()) / 2f + grid.CellSize / 2f;
        return new Vector3(snapX, height, snapZ);
    }

    void OnInteract(InputValue value)
    {
        Debug.Log("Placed");
        PlacePackage();
    }

    void OnRotate(InputValue value)
    {
        Debug.Log("Rotated");
        if (activePackage != null)
        {
            activePackage.RotatePackage(90f);
        }
    }

    void OnSwitch(InputValue value)
    {
        if (currentPackageType < packageTypes.Count())
        {
            currentPackageType++;
        }
        else
        {
            currentPackageType = 0;
        }

        SelectPackage(packageTypes[currentPackageType]);
    }
}
