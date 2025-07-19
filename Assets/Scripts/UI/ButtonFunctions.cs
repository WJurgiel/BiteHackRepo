using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour
{
    [SerializeField]
    private BoneSO bone;
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ContinueGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        bone.boneCurrent = 0;
    }
    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        bone.boneCurrent = 0;
    }
    public void ShowSettings()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
