using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    private List<PackageData> levelPackages;
    public void SetUpLevel()
    {
        levelPackages = levelData.levelPackages;
    }
}
