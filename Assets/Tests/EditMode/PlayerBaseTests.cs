using NUnit.Framework;
using Player;
using UnityEngine;

namespace Tests.EditMode
{
    public class PlayerBaseTests
    {
        [Test]
        public void AddExp_AddsExperienceCorrectly()
        {
            var playerGameObject = new GameObject();
            var player = playerGameObject.AddComponent<PlayerBase>();

            const int currentExp = 10;
            const int amountToAdd = 5;

            var newExp = PlayerBase.AddExp(currentExp, amountToAdd);
            Assert.AreEqual(15, newExp);
        }
    }
}
