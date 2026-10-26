using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private PuzzleGrid grid;
    [SerializeField] private PackageData currentPackageData;
    [SerializeField] private LayerMask puzzleMask;
    [SerializeField] private GameObject test;
    private GridPackage activePackage;
    private PlayerInput playerInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        SelectPackage(currentPackageData);
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
        if (grid.CanPlacePackage(activePackage.AllPlacementPoints()))
        {
            Debug.Log("yay");
            activePackage.MovePackage(GetGridSnapPosition(activePackage.AllPlacementPoints()));
        }
    }

    private void SelectPackage(PackageData data)
    {
        activePackage = Instantiate(data.gridPackage.gameObject, GetMousePosition(), Quaternion.identity).GetComponent<GridPackage>();
        activePackage.Initialize(data);
    }

    private void PlacePackage()
    {
        if (activePackage != null)
        {
            if (grid.CanPlacePackage(activePackage.AllPlacementPoints()))
            {
                grid.PlacePackage(activePackage, activePackage.AllPlacementPoints());
                Instantiate(activePackage.gameObject, GetGridSnapPosition(activePackage.AllPlacementPoints()), activePackage.transform.rotation);
                activePackage = null;
                SelectPackage(currentPackageData);
            }
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

    private Vector3 GetGridSnapPosition(List<Vector3> placementPoints)
    {
        List<int> x = placementPoints.Select(p => Mathf.FloorToInt(p.x)).ToList();
        List<int> z = placementPoints.Select(p => Mathf.FloorToInt(p.z)).ToList();
        float snapX = (x.Min() + x.Max()) / 2f + grid.CellSize / 2f;
        float snapZ = (z.Min() + z.Max()) / 2f + grid.CellSize / 2f;
        return new Vector3(snapX, 0, snapZ);
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
}
