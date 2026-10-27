using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridCell
{
    private int maxHeight;
    private List<GridPackage> packages = new List<GridPackage>();
    private CellState cellState;

    public void Initialize(int height, CellState startState)
    {
        maxHeight = height;
        cellState = startState;
    }

    public void SetPackage(GridPackage newPackage, PlacementPoint point)
    {
        for (int i = 0; i < point.Height; i++)
        {
            packages.Add(newPackage);
        }
        if (newPackage.Stackable && packages.Count < maxHeight)
        {
            cellState = CellState.STACKABLE;
        }
        else
        {
            cellState = CellState.FULL;
        }
    }

    public void RemovePackage()
    {
        if (packages.Count > 1)
        {
            cellState = CellState.STACKABLE;
            if (packages[0] == packages[1])
            {
                packages.Clear();
            }
            else
            {
                packages.RemoveAt(packages.Count - 1);
            }
        }
        else if (packages.Count > 0)
        {
            cellState = CellState.STACKABLE;
            packages.RemoveAt(packages.Count - 1);
        }

        if (packages.Count == 0)
        {
            cellState = CellState.EMPTY;
        }
    }

    public CellState GetState()
    {
        return cellState;
    }

    public GridPackage GetTopPackage()
    {
        return packages[packages.Count - 1];
    }

    public float GetWeight()
    {
        float totalWeight = 0;
        foreach (var p in packages)
        {
            totalWeight += p.Weight;
        }
        return totalWeight;
    }
}

public enum CellState
{
    EMPTY,
    STACKABLE,
    FULL
}
