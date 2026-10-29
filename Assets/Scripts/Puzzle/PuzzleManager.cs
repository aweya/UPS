using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private PuzzleGrid grid;
    [SerializeField] private LayerMask puzzleMask;
    [SerializeField] private GameObject test;
    [SerializeField] private BalanceManager balanceManager;
    private GridPackage activePackage;
    private PlayerInput playerInput;
    public PuzzleGrid Grid => grid;
    public BalanceManager BalanceManager => balanceManager;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
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
            //RuntimeManager.PlayOneShot(activePackage.Data.gridSound);
        }
        else if (grid.PlacementState(activePackage.PlacementPointCoordinates()) == CellState.STACKABLE)
        {
            activePackage.MovePackage(GetGridSnapPosition(activePackage.PlacementPointCoordinates(), 1.1f));
            //RuntimeManager.PlayOneShot(activePackage.Data.gridSound);
        }
    }

    public void ResetPuzzle()
    {
        if (activePackage != null)
        {
            Destroy(activePackage.gameObject);
        }
        activePackage = null;
        grid.ResetGrid();
    }

    public void SelectPackage(PackageData data)
    {
        if (activePackage != null)
        {
            Destroy(activePackage.gameObject);
        }
        activePackage = Instantiate(data.gridPackage.gameObject, GetMousePosition(), Quaternion.identity, grid.transform).GetComponent<GridPackage>();
        activePackage.transform.localRotation = Quaternion.Euler(Vector3.zero);
        activePackage.Initialize(data);
    }

    public bool HasActivePackage()
    {
        return activePackage != null;
    }

    public int AmountOfPackagesOnGrid()
    {
        return grid.TotalPackagesOnGrid;
    }

    public List<GridPackage> GetPackagesOnGrid()
    {
        return grid.PackagesOnGrid;
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

        RuntimeManager.PlayOneShot(activePackage.Data.placeSound);

        grid.PlacePackage(placedPackage, placedPackage.AllPlacementPoints());

        Destroy(activePackage.gameObject);
        activePackage = null;
    }

    private void TryPickUpPackage()
    {
        int x = grid.GetGridPosition(GetMousePosition()).x;
        int y = grid.GetGridPosition(GetMousePosition()).y;
        if (x > 0 && x <= grid.GridSize && y > 0 && y <= grid.GridSize)
        {
            Debug.Log("Cell " + x + ", " + y + " - " + grid.GridCells[x, y].GetState());
            if (grid.GridCells[x, y].GetState() != CellState.EMPTY)
            {
                GridPackage package = grid.GridCells[x, y].GetTopPackage();
                SelectPackage(package.Data);
                grid.RemovePackage(package, package.AllPlacementPoints());
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
}
