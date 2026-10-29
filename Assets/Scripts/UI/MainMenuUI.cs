using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public GameObject creditsScreen;
    public GameObject gameOverScreen;
    public GameObject contract;
    public GameObject stampMark;
    bool stampRunning;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShowContract()
    {
        contract.SetActive(true);
    }
    public void HideContract()
    {
        contract.SetActive(true);
    }

    public void StartGame()
    {
        if (!stampRunning)
        {
            StartCoroutine(StampCutscene());
        }
        stampRunning = true;
    }
    public void ShowCredits()
    {
        creditsScreen.SetActive(true);
    }
    public void HideCredits()
    {
        creditsScreen.SetActive(false);
    }
    public void CloseGameOver()
    {
        gameOverScreen.SetActive(false);
    }
    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator StampCutscene()
    {
        stampMark.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("AndiScene");
    }
}
