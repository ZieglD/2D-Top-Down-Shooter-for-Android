using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Kontrolliert die Szenen und setzt fest, wann welche Szene geladen wird
public class MainMenu : MonoBehaviour
{
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


    public void NewRun()
    {
        SceneManager.LoadScene("BasementMain");
    }


    public void NextButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void BackButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }


    public void BackToMenu()
    {
       SceneManager.LoadScene("MainMenu");
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
