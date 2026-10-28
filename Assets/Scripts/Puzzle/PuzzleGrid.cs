using System.Collections.Generic;
using UnityEngine;

public class PuzzleGrid : MonoBehaviour
{
    public float CellSize = 1f;
    public int GridSize = 3;
    public int GridHeight = 2;
    private GridCell[,] gridCells;
    private List<GridPackage> packagesOnGrid = new List<GridPackage>();
    public GridCell[,] GridCells => gridCells;
    public int PackagesOnGrid => packagesOnGrid.Count;

    private void Awake()
    {
        gridCells = new GridCell[GridSize, GridSize];
    }

    void Start()
    {
        ResetGrid();
    }

    public void PlacePackage(GridPackage package, List<PlacementPoint> placementPoints)
    {
        foreach (var p in placementPoints)
        {
            (int x, int y) = GetGridPosition(p.transform.position);
            gridCells[x, y].SetPackage(package, p);
            Debug.Log("Cell: " + x + ", " + y + "; Weight: " + gridCells[x, y].GetWeight());
        }
        packagesOnGrid.Add(package);
    }


    public void RemovePackage(GridPackage package, List<PlacementPoint> placementPoints)
    {
        foreach (var p in placementPoints)
        {
            (int x, int y) = GetGridPosition(p.transform.position);
            gridCells[x, y].RemovePackage();
            Debug.Log("Cell: " + x + ", " + y + "; Weight: " + gridCells[x, y].GetWeight());
        }
        packagesOnGrid.Remove(package);
    }

    public CellState PlacementState(List<Vector3> placementPoints)
    {
        int stackableCells = 0;
        foreach (var p in placementPoints)
        {
            (int x, int y) = GetGridPosition(p);
            if (x < 0 || x >= GridSize || y < 0 || y >= GridSize)
            {
                return CellState.FULL;
            }

            if (gridCells[x, y].GetState() == CellState.FULL)
            {
                return CellState.FULL;
            }
            else if (gridCells[x, y].GetState() == CellState.STACKABLE)
            {
                stackableCells++;
            }
        }

        if (stackableCells == 0)
        {
            return CellState.EMPTY;
        }
        else if (stackableCells == placementPoints.Count)
        {
            return CellState.STACKABLE;
        }
        else
        {
            return CellState.FULL;
        }
    }

    public (int x, int y) GetGridPosition(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt((worldPos - transform.position).x / CellSize);
        int y = Mathf.FloorToInt((worldPos - transform.position).z / CellSize);
        return (x, y);
    }

    public void ResetGrid()
    {
        for (int x = 0; x < gridCells.GetLength(0); x++)
        {
            for (int y = 0; y < gridCells.GetLength(1); y++)
            {
                gridCells[x, y] = new();
                gridCells[x, y].Initialize(GridHeight, CellState.EMPTY);
            }
        }
        if (packagesOnGrid.Count != 0)
        {
            foreach (var p in packagesOnGrid)
            {
                Destroy(p.gameObject);
            }
        }
        packagesOnGrid.Clear();
    }
}
