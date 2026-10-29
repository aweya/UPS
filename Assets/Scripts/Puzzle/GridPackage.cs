using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridPackage : MonoBehaviour
{
    private PackageData pData;
    private List<PlacementPoint> placementPoints;
    private float weight;
    private float value;
    private bool stackable;
    public float Weight => weight;
    public float Value => value;
    public bool Stackable => stackable;
    public PackageData Data => pData;

    private bool isSmugglePackage;
    private int smugglePackageMinimumFacesCovered;

    public void Initialize(PackageData data)
    {
        placementPoints = GetComponentsInChildren<PlacementPoint>().ToList();
        pData = data;
        weight = data.weight;
        value = data.value;
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

    public bool CheckSmugglePackage()
    {
        if (isSmugglePackage)
        {
            int facesCovered = 0;
            foreach (var p in AllPlacementPoints())
            {
                Vector3[] directions = new Vector3[]
                {
                    p.transform.forward,
                    -p.transform.forward,
                    p.transform.right,
                    -p.transform.right,
                    p.transform.up
                };

                foreach (Vector3 dir in directions)
                {
                    Vector3 raycastPos = new Vector3(p.transform.localPosition.x, p.transform.localPosition.y + 0.5f, p.transform.localPosition.z);
                    RaycastHit hit;
                    if (Physics.Raycast(raycastPos, dir, out hit, 0.6f))
                    {
                        facesCovered++;
                    }
                }
            }
            if (facesCovered >= smugglePackageMinimumFacesCovered)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}
