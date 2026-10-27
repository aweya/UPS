using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridPackage : MonoBehaviour
{
    private PackageData pData;
    private List<PlacementPoint> placementPoints;
    private float weight;
    private bool stackable;
    public float Weight => weight;
    public bool Stackable => stackable;
    public PackageData Data => pData;

    public void Initialize(PackageData data)
    {
        placementPoints = GetComponentsInChildren<PlacementPoint>().ToList();
        pData = data;
        weight = data.weight;
        stackable = data.isStackable;
    }

    public List<PlacementPoint> AllPlacementPoints()
    {
        return placementPoints;
    }

    public List<Vector3> PlacementPointCoordinates()
    {
        List<Vector3> allPoints = new List<Vector3>();
        foreach (PlacementPoint p in placementPoints)
        {
            allPoints.Add(p.gameObject.transform.position);
        }
        return allPoints;
    }

    public int GetHeight()
    {
        return 0;
    }

    public void MovePackage(Vector3 position)
    {
        transform.localPosition = position;
    }

    public void RotatePackage(Vector3 rotateAmount)
    {
        transform.Rotate(rotateAmount);
    }
}
