using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    public int tier;
    public PuzzleGrid palette;
    public List<PackageData> levelPackages;
    public float maxWinDeviation;
}
