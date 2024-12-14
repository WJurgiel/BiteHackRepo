using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour
{

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ContinueGame()
    {
        SceneManager.LoadScene("Aneta");
    }
    public void NewGame()
    {
        SceneManager.LoadScene("Aneta");
    }
    public void ShowSettings()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
