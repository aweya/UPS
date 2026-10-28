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
    [SerializeField] private BalanceManager balanceManager;
    private GridPackage activePackage;
    private PlayerInput playerInput;
    private int currentPackageType = 0;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        //SelectPackage(packageTypes[currentPackageType]);
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
        Vector3 rotation = new Vector3(balanceManager.GetCurrentTilt().x, activePackage.transform.localEulerAngles.y, balanceManager.GetCurrentTilt().z);
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

    public void SelectPackage(PackageData data)
    {
        if (activePackage != null)
        {
            //Destroy(activePackage.gameObject);
        }
        activePackage = Instantiate(data.gridPackage.gameObject, GetMousePosition(), Quaternion.identity, grid.transform).GetComponent<GridPackage>();
        activePackage.transform.localRotation = Quaternion.Euler(Vector3.zero);
        activePackage.Initialize(data);
    }

    private void PlacePackage()
    {
        Debug.Log("Package placed");
        List<Vector3> allPlacementPoints = activePackage.PlacementPointCoordinates();
        if (grid.PlacementState(allPlacementPoints) == CellState.FULL)
        {
            Debug.Log("Cell full");
            return;
        }
        float height = 0;
        if (grid.PlacementState(allPlacementPoints) == CellState.EMPTY)
        {
            height = 0;
        }
        else if (grid.PlacementState(allPlacementPoints) == CellState.STACKABLE)
        {
            height = 1;
        }

        Vector3 placedRotation = new Vector3(0, activePackage.transform.localEulerAngles.y, 0);
        GridPackage placedPackage = Instantiate(activePackage.gameObject, Vector3.zero, Quaternion.identity, grid.transform).GetComponent<GridPackage>();
        placedPackage.transform.localPosition = GetGridSnapPosition(allPlacementPoints, height);
        placedPackage.transform.localRotation = Quaternion.Euler(placedRotation);
        placedPackage.Initialize(activePackage.Data);

        grid.PlacePackage(placedPackage, placedPackage.AllPlacementPoints());

        Destroy(activePackage.gameObject);
        activePackage = null;
        if (currentPackageType < packageTypes.Count() - 1)
        {
            //SelectPackage(packageTypes[currentPackageType]);
        }
    }

    private void TryPickUpPackage()
    {
        int x = grid.GetGridPosition(GetMousePosition()).x;
        int y = grid.GetGridPosition(GetMousePosition()).y;
        if (x <= grid.GridSize && y <= grid.GridSize)
        {
            Debug.Log("Cell " + x + ", " + y + " - " + grid.GridCells[x, y].GetState());
            if (grid.GridCells[x, y].GetState() != CellState.EMPTY)
            {
                GridPackage package = grid.GridCells[x, y].GetTopPackage();
                SelectPackage(package.Data);
                grid.RemovePackage(package.AllPlacementPoints());
                Destroy(package.gameObject);
            }
        }
    }

    private Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Plane ground = new(grid.transform.up, Vector3.zero);
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
        if (activePackage != null)
        {
            Debug.Log("Placed");
            PlacePackage();
        }
        else
        {
            TryPickUpPackage();
        }

    }

    void OnRotate(InputValue value)
    {
        Debug.Log("Rotated");
        if (activePackage != null)
        {
            activePackage.RotatePackage(new Vector3(0, 90f, 0));
        }
    }

    void OnSwitch(InputValue value)
    {
        /*if (currentPackageType < packageTypes.Count() - 1)
        {
            currentPackageType++;
            SelectPackage(packageTypes[currentPackageType]);
        }
        else if (currentPackageType == packageTypes.Count() - 1)
        {
            currentPackageType++;
            Destroy(activePackage.gameObject);
            activePackage = null;
        }
        else
        {
            currentPackageType = 0;
            SelectPackage(packageTypes[currentPackageType]);
        }*/
    }
}
