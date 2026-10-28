using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDatabase", menuName = "Scriptable Objects/LevelDatabase")]
public class LevelDatabase : ScriptableObject
{
    public List<LevelDataWrapper> allLevelData;
    public List<LevelData> GetLevelsOfTier(int tier)
    {
        if (tier > allLevelData.Count - 1)
        {
            return null;
        }
        else
        {
            return allLevelData[tier - 1].GetLevelData();
        }
    }
}

[Serializable]
public class LevelDataWrapper
{
    public List<LevelData> levelData;
    public List<LevelData> GetLevelData()
    {
        return levelData;
    }
}
