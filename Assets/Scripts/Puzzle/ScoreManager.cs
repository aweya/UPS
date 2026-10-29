using System.Collections.Generic;
using FMODUnity;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private float balanceCrashTreshhold = 3.5f;
    [SerializeField] private EventReference moneyGainEvent;
    [SerializeField] private EventReference moneyLossEvent;

    public float timeGracePercent = 0.2f;
    private float timeGracePeriod;
    public float smuggleBusse = 100;

    public float CalculateMoneyLevel(List<GridPackage> placedPackages, BalanceManager balanceManager, CountdownTimer countdownTimer, PuzzleGrid grid)
    {
        float timer = countdownTimer.TimeRemaining;
        float totalTimer = countdownTimer.startTime;
        float totalBalance = balanceManager.totalBalance;
        //score per level
        // all packages added * balance * time left
        float moneySumLevel = 0;

        foreach (var p in placedPackages)
        {
            float value = p.Data.value;
            moneySumLevel += value;
        }

        // check if rocket is off balance and crashes
        if (totalBalance > balanceCrashTreshhold)
        {
            return -moneySumLevel;
        }

        // successful delivery

        // calc balance rating
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
        // Full time rating during the grace period, then decrease to 0.
        timeGracePeriod = totalTimer * timeGracePercent;
        float timeRating = Mathf.Lerp(0.2f, 1f, Mathf.Clamp01(timer / (totalTimer - timeGracePeriod)));

        // calc total cash
        float finalCash = moneySumLevel * balanceRating * timeRating;

        // smuggle package
        //still have to do this!!!
        /*
        if (smuggle = true)
            finalCash -= smuggleBusse;
        */

        if (finalCash > 0)
        {
            RuntimeManager.PlayOneShot(moneyGainEvent);
        }
        else if (finalCash < 0)
        {
            RuntimeManager.PlayOneShot(moneyLossEvent);
        }

        return finalCash;
    }

}
