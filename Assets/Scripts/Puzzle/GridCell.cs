using Unity.VisualScripting;
using UnityEngine;

public class GridCell
{
    private GridPackage package;

    public void SetPackage(GridPackage newPackage)
    {
        package = newPackage;
    }

    public bool IsEmpty()
    {
        return package == null;
    }
}
