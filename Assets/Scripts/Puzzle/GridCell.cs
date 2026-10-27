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

    public CellState GetState()
    {
        return cellState;
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
