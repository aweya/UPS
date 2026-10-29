using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CountdownTimer countdownTimer;
    public TextMeshProUGUI moneyText;
    public GameObject pauseScreen;
    public GameObject creditsScreen;
    public GameObject mainMenu;
    public GameObject gameScreen;
    public GameObject tutorialScreen;
    public Button Stamp;
    private bool firstGameStart = true;

    public void UpdateMoney(float amount)
    {
        moneyText.text = amount.ToString();
    }

    public void EnterPauseMenu()
    {
        Debug.Log("pause menu on!");
        pauseScreen.SetActive(true);
        countdownTimer.runTimer = false;
        Debug.Log(countdownTimer.runTimer);
        Debug.Log(countdownTimer.TimeRemaining);
    }

    public void LeavePauseMenu()
    {
        pauseScreen.SetActive(false);
        countdownTimer.runTimer = true;
    }

    public void EnterGameScreen()
    {
        if (firstGameStart)
        {
            mainMenu.SetActive(false);
            tutorialScreen.SetActive(true);
            firstGameStart = false;
        }
        else
        {
            mainMenu.SetActive(false);
            gameScreen.SetActive(true);
        }
        Debug.Log("disply Game screen");
    }

    public void EnterCreditsScreen()
    {
        mainMenu.SetActive(false);
        creditsScreen.SetActive(true);
        Debug.Log("disply CREDITS screen");
    }

    public void BackToMenu()
    {
        creditsScreen.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ContinueToGame()
    {
        tutorialScreen.SetActive(false);
        gameScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
