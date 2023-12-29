using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene("SewerLevel");
    }

    public void viewControls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void viewCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
