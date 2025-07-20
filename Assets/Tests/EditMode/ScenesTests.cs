using System.Linq;
using NUnit.Framework;
using UnityEditor;

namespace Tests.EditMode
{
    public class ScenesTests
    {
        private readonly string[] _requiredScenes =
        {
            "Assets/Scenes/MainMenu.unity",
            "Assets/Scenes/Level_1.unity",
        };
        
        [Test]
        public void RequiredScenes_ExistInSceneFolder()
        {
            foreach (var scenePath in _requiredScenes)
            {
                var sceneExists = System.IO.File.Exists(scenePath);
                Assert.IsTrue(sceneExists, $"Scene not found {scenePath}");
            }
        }

        [Test]
        public void RequiredScenes_AreInBuildSettings()
        {
            var scenesInBuild = EditorBuildSettings.scenes.Select(scene => scene.path).ToArray();
            foreach (var requiredScene in _requiredScenes)
            {
                var isInBuildSettings = scenesInBuild.Contains(requiredScene);
                Assert.IsTrue(isInBuildSettings, $"Scene not in build settings: {requiredScene}");
            }
        }
    }
}
