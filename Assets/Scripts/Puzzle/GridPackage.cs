using System.Collections.Generic;
using System.Linq;
using Unity.VectorGraphics;
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
    public bool IsSmugglePackage => isSmugglePackage;
    private int smugglePackageMinimumFacesCovered;

    public void Initialize(PackageData data)
    {
        placementPoints = GetComponentsInChildren<PlacementPoint>().ToList();
        pData = data;
        weight = data.weight;
        value = data.value;
        stackable = data.isStackable;
        isSmugglePackage = data.isSmugglePackage;
        smugglePackageMinimumFacesCovered = data.smugglePackageMinimumFacesCovered;
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
        bool a = CheckSmugglePackage();
    }

    public bool CheckSmugglePackage()
    {
        Debug.Log("Checking Package");
        if (isSmugglePackage)
        {
            GameObject checkObject = new GameObject();
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
                    for (int i = 0; i < p.Height; i++)
                    {
                        GameObject o = Instantiate(checkObject, this.transform.parent);
                        o.transform.localPosition = new Vector3(p.transform.position.x, p.transform.position.y + 0.5f + i, p.transform.position.z);
                        Vector3 raycastPos = o.transform.position;
                        RaycastHit hit;
                        if (Physics.Raycast(raycastPos, dir, out hit, 0.6f))
                        {
                            facesCovered++;
                        }

                        Debug.DrawRay(raycastPos, dir * 0.6f, Color.red, 2f);
                        Destroy(o);
                    }
                }
            }
            Debug.Log("Package checked. Faces Covered: " + facesCovered);
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
