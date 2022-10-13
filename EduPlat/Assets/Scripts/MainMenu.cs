using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("DifficultySelect");
    }

    public void GoToCreditsMenu()
    {
        SceneManager.LoadScene("Credits");
    }

    public void GoToTutorialLevel()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMediumLevel()
    {
        SceneManager.LoadScene("M1");
    }

    public void GoToEasyLevel()
    {
        SceneManager.LoadScene("E1");
    }

    public void GoToHardfLevel()
    {
        SceneManager.LoadScene("H1");
    }
}
