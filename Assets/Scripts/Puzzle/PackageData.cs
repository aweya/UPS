using UnityEngine;

[CreateAssetMenu(fileName = "PackageData", menuName = "Scriptable Objects/PackageData")]
public class PackageData : ScriptableObject
{
    public GridPackage gridPackage;
    public float weight;
}
