using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class ButtonFunctions : MonoBehaviour
    {
        [SerializeField]
        private BoneSo bone;

        [SerializeField]
        private GameObject optionsPanel;

        [SerializeField]
        private Slider volumeSlider;

        [SerializeField]
        private AudioSource menuMusic;

        private void Start()
        {
            volumeSlider.value = 1;
            volumeSlider.onValueChanged.AddListener(delegate {SetVolume ();});
        }
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
            optionsPanel.SetActive(true);
        }
        public void CloseSettings()
        {
            optionsPanel.SetActive(false);
        }
        public void QuitGame()
        {
            Application.Quit();
        }
        public void SetVolume()
        {
            menuMusic.volume = volumeSlider.value;
            PlayerPrefs.SetFloat("volume", volumeSlider.value);
            PlayerPrefs.Save();
        }
    }
}
