using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PuzzleManager puzzleManager;
    [SerializeField] private TicketManager ticketManager;
    [SerializeField] private LevelDatabase levelDatabase;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private CountdownTimer countdownTimer;
    [SerializeField] private UIManager uIManager;

    [Header("Progression")]
    [SerializeField] private int levelsPerTier;
    [SerializeField] private int currentTier = 1;
    [SerializeField] private List<float> startTimePerTier = new List<float>();
    [SerializeField] private List<float> levelTimeDeductionPerTier = new List<float>();
    private float totalMoney;
    public float TotalMoney => totalMoney;
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
        Initialise();
    }

    void OnDisable()
    {
        countdownTimer.finishedAction -= TryFinishLevel;
    }

    private void Start()
    {
        SetUpLevel();
    }

    private void Initialise()
    {
        highestTier = levelDatabase.allLevelData.Count;
        foreach (var l in levelDatabase.GetLevelsOfTier(currentTier))
        {
            availableLevels.Add(l);
        }
        totalMoney = 0;
        countdownTimer.finishedAction += TryFinishLevel;
    }

    private void SetUpLevel()
    {
        float newStartTime = startTimePerTier[currentTier - 1] - (levelTimeDeductionPerTier[currentTier - 1] * levelsPlayedOfTier);
        puzzleManager.ResetPuzzle();
        currentLevelData = GetRandomLevel();
        levelPackages = currentLevelData.levelPackages;
        uIManager.SetNewCharacter(currentLevelData.levelCharacter);
        ticketManager.CreateOrders(levelPackages);
        countdownTimer.ResetTimer(newStartTime);
    }

    private LevelData GetRandomLevel()
    {
        int randomIndex = Random.Range(0, availableLevels.Count);
        Debug.Log(randomIndex);
        LevelData randomLevel = availableLevels[randomIndex];
        availableLevels.Remove(randomLevel);
        return randomLevel;
    }

    public void TryFinishLevel()
    {
        if (puzzleManager.AmountOfPackagesOnGrid() == levelPackages.Count || countdownTimer.timerFinished)
        {
            FinishLevel();
        }
        else
        {
            Debug.Log("Packages left in order!");
        }
    }

    private void FinishLevel()
    {
        totalMoney += scoreManager.CalculateMoneyLevel(puzzleManager.GetPackagesOnGrid(), puzzleManager.BalanceManager, countdownTimer, puzzleManager.Grid);
        Debug.Log("Money: " + totalMoney);
        if (totalMoney <= 0)
        {
            FinishGame(false);
        }
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

    private void FinishGame(bool isWin)
    {
        if (isWin)
        {
            Debug.Log("Congrats, you've finished the game!");
        }
        else
        {
            Debug.Log("Whoops, you got fired");
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
            FinishGame(true);
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
