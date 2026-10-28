using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

[CreateAssetMenu(fileName = "PackageData", menuName = "Scriptable Objects/PackageData")]
public class PackageData : ScriptableObject
{
    public GridPackage gridPackage;
    public float weight;
    public float value;
    public bool isStackable;

    [Header("Sound")]
    public EventReference spawnSound;
    public EventReference gridSound;
    public EventReference placeSound;

    [Header("UI Values")]
    public Sprite[] icons;
    public string packageName;
    public Image background;

}
