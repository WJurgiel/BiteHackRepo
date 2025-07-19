using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTests
{
    [Test]
    public void AddExp_AddsExperienceCorrectly()
    {
        var PlayerGameObject = new GameObject();
        var player = PlayerGameObject.AddComponent<Player>();

        int currentExp = 10;
        int ammountToAdd = 5;

        int newExp = player.addExp(currentExp, ammountToAdd);
        Assert.AreEqual(17, newExp);
    }
}
