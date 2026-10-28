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

    public int currentMoney;

    private bool firstGameStart = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = currentMoney.ToString();
    }

    public void EnterPauseMenu()
    {
        Debug.Log("pause menu on!");
        pauseScreen.SetActive(true);
        countdownTimer.timerRun = false;
        Debug.Log(countdownTimer.timerRun);
        Debug.Log(countdownTimer.timeRemaining);
    }

    public void LeavePauseMenu()
    {
        pauseScreen.SetActive(false);
        countdownTimer.timerRun = true;
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
