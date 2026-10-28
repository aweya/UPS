using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PuzzleManager puzzleManager;
    [SerializeField] private TicketManager ticketManager;
    [SerializeField] private LevelDatabase levelDatabase;
    [SerializeField] private int currentTier;
    private LevelData currentLevelData;
    private List<LevelData> availableLevels;
    private List<PackageData> levelPackages;
    private float highestTier;
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        highestTier = levelDatabase.allLevelData.Count;
    }

    private void Start()
    {
        SetUpLevel();
    }

    public void SetUpLevel()
    {
        levelPackages = currentLevelData.levelPackages;
        ticketManager.CreateOrders(levelPackages);
    }

    public void SpawnPackage(PackageData data)
    {
        puzzleManager.SelectPackage(data);
    }

    private void SwitchTier()
    {
        if (currentTier >= highestTier)
        {

        }
        else
        {
            currentTier++;
            availableLevels = levelDatabase.GetLevelsOfTier(currentTier);
        }
    }
}
