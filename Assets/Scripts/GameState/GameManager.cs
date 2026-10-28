using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PuzzleManager puzzleManager;
    [SerializeField] private TicketManager ticketManager;
    [SerializeField] private LevelDatabase levelDatabase;
    [SerializeField] private int levelsPerTier;
    [SerializeField] private int currentTier = 1;
    private LevelData currentLevelData;
    private List<LevelData> availableLevels = new List<LevelData>();
    private List<PackageData> levelPackages;
    private int highestTier;
    private int levelsPlayedOfTier;
    private OrderEntry activeOrderEntry;
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
        foreach (var l in levelDatabase.GetLevelsOfTier(currentTier))
        {
            availableLevels.Add(l);
        }
    }

    private void Start()
    {
        SetUpLevel();
    }

    private void SetUpLevel()
    {
        currentLevelData = GetRandomLevel();
        levelPackages = currentLevelData.levelPackages;
        ticketManager.CreateOrders(levelPackages);
    }

    private LevelData GetRandomLevel()
    {
        int randomIndex = Random.Range(0, availableLevels.Count);
        Debug.Log(randomIndex);
        LevelData randomLevel = availableLevels[randomIndex];
        availableLevels.Remove(randomLevel);
        return randomLevel;
    }

    private void FinishLevel()
    {
        levelsPlayedOfTier++;
        if (levelsPlayedOfTier >= levelsPerTier)
        {
            SwitchTier();
            if (currentTier <= highestTier)
            {
                SetUpLevel();
            }
        }
        else
        {
            SetUpLevel();
        }
    }

    public void SpawnPackage(PackageData data, OrderEntry entry)
    {
        if (puzzleManager.HasActivePackage())
        {
            ReturnPackageToEntry(activeOrderEntry);
        }
        activeOrderEntry = entry;
        puzzleManager.SelectPackage(data);
    }

    public void ReturnPackageToEntry(OrderEntry entry)
    {
        entry.ReturnPackage();
    }

    private void SwitchTier()
    {
        if (currentTier >= highestTier)
        {

        }
        else
        {
            currentTier++;
            availableLevels.Clear();
            foreach (var l in levelDatabase.GetLevelsOfTier(currentTier))
            {
                availableLevels.Add(l);
            }
        }
    }
}
