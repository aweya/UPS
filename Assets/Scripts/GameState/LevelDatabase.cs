using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDatabase", menuName = "Scriptable Objects/LevelDatabase")]
public class LevelDatabase : ScriptableObject
{
    public List<LevelData> tierOneLevels;
    public List<LevelData> tierTwoLevels;
    public List<LevelData> tierThreeLevels;
    public List<LevelData> tierFourLevels;
}
