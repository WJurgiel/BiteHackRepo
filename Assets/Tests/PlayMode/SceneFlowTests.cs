using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests.PlayMode
{
    public class SceneFlowTests
    {
        /// <summary>
        /// For now it should go to Level_1.unity, change the logic and name accordingly to changes
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator ClickingNewGameButton_LoadLevel1Scene()
        {
            SceneManager.LoadScene("MainMenu");
            yield return null;

            while (SceneManager.GetActiveScene().name != "MainMenu")
                yield return null;

            GameObject newGameButtonObject = GameObject.Find("newGameButton");
            Assert.IsNotNull(newGameButtonObject, "Reason: newGameButton not found in MainMenu");
            
            Button newGameButton = newGameButtonObject.GetComponent<Button>();
            Assert.IsNotNull(newGameButton, "Reason: newGameButton does not have a Button component");

            newGameButton.onClick.Invoke();
            
            float timeout = 5f;
            float timer = 0f;

            while (SceneManager.GetActiveScene().name != "Level_1" && timer < timeout)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            Assert.AreEqual("Level_1", SceneManager.GetActiveScene().name, "Level_1 scene was not loaded");
        }
    }
}
