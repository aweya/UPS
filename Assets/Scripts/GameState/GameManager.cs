using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PuzzleManager puzzleManager;
    [SerializeField] private LevelData levelData;
    [SerializeField] private TicketManager ticketManager;
    private List<PackageData> levelPackages;

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
    }

    private void Start()
    {
        SetUpLevel();
    }

    public void SetUpLevel()
    {
        levelPackages = levelData.levelPackages;
        ticketManager.CreateOrders(levelPackages);
    }

    public void SpawnPackage(PackageData data)
    {
        puzzleManager.SelectPackage(data);
    }
}
