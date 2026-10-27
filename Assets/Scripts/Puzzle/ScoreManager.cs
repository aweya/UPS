using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    [SerializeField] BalanceManager balanceManager;
    [SerializeField] private PuzzleGrid grid;

    [SerializeField] private float balanceCrashTreshhold = 3.5f;
    public float smuggleBusse = 100;

    public float moneyyyyyy;

    private float moneySumLevel = 0;

    private float totalBalance => balanceManager.totalBalance;

    void Update()
    {
        moneyyyyyy = CalculateMoneyLevel(grid.GridCells);
    }





    float CalculateMoneyLevel(GridCell[,] gridCells)
    {
        //score per level
        // all packages added * balance * time left
        moneySumLevel = 0;
        //read every cell and add money

        for (int x = 0; x < gridCells.GetLength(0); x++)
        {
            for (int z = 0; z < gridCells.GetLength(1); z++)
            {
                //get value
                float value = gridCells[x, z].GetWeight(); //chanche to mooney!!!!!!!!!!!!!!!!!!!!!!
                //add to sum
                moneySumLevel += value;
            }
        }

        // check if rocket is off balance and crashes
        if (totalBalance > balanceCrashTreshhold)
        {
            // payy all packages
            return (-moneySumLevel);
        }

        // sucsessfull delivery

        // calc balancerating
        float balanceRating = 0;
        if (totalBalance > balanceCrashTreshhold - 0.1f)
        {
            balanceRating = 0.1f;
        }
        else
        {
            balanceRating = (balanceCrashTreshhold - totalBalance) / balanceCrashTreshhold;
        }

        // calc timerating
        //-------still have to do this!!!!!
        float timerating = 1;

        // calc total cash
        float finalCash = moneySumLevel * balanceRating * timerating;

        // smuggle package
        //still have to do this tooo!!!
        /*
        if (smuggle = true)
            finalCash -= smuggleBusse;
        */

        return (finalCash);
    }




}
