using System.Collections.Generic;
using UnityEngine;

public class PuzzleGrid : MonoBehaviour
{
    public float CellSize = 1f;
    public int GridSize = 3;
    private GridCell[,] gridCells;

    void Start()
    {
        gridCells = new GridCell[GridSize, GridSize];
        for (int x = 0; x < gridCells.GetLength(0); x++)
        {
            for (int y = 0; y < gridCells.GetLength(1); y++)
            {
                gridCells[x, y] = new();
            }
        }
    }

    public void PlacePackage(GridPackage package, List<Vector3> placementPoints)
    {
        foreach (var p in placementPoints)
        {
            (int x, int y) = GetGridPosition(p);
            gridCells[x, y].SetPackage(package);
        }
    }

    public bool CanPlacePackage(List<Vector3> placementPoints)
    {
        foreach (var p in placementPoints)
        {
            (int x, int y) = GetGridPosition(p);
            if (x < 0 || x >= GridSize || y < 0 || y >= GridSize)
            {
                return false;
            }

            if (!gridCells[x, y].IsEmpty())
            {
                return false;
            }
        }
        return true;
    }

    private (int x, int y) GetGridPosition(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt((worldPos - transform.position).x / CellSize);
        int y = Mathf.FloorToInt((worldPos - transform.position).z / CellSize);
        return (x, y);
    }
}
