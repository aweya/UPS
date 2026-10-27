using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    [SerializeField] private TicketManager ticketManager;
    private List<PackageData> levelPackages;
    public void SetUpLevel()
    {
        levelPackages = levelData.levelPackages;
    }
}
