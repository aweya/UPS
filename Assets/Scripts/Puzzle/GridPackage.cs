using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridPackage : MonoBehaviour
{
    private List<PlacementPoint> placementPoints;
    private float weight;
    private bool stackable;
    public float Weight => weight;
    public bool Stackable => stackable;

    void Start()
    {
        //placementPoints = GetComponentsInChildren<PlacementPoint>().ToList();
    }

    public void Initialize(PackageData data)
    {
        placementPoints = GetComponentsInChildren<PlacementPoint>().ToList();
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

    public void MovePackage(Vector3 position)
    {
        transform.position = position;
    }

    public void RotatePackage(float amount)
    {
        transform.Rotate(0, amount, 0);
    }
}
