using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour
{

    public void ContinueGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void NewGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void ShowSettings()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
