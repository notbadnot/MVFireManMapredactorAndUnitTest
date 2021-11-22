using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Game;
using Input;



public class TestForBot
{
    [UnityTest]
    public IEnumerator TestForBotGoesRightIfPointToGoOnTheStraightRightOfHim()
    {
        GameObject levelForTest = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/LevelForTest"));
        GameObject zombieForTest = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/ZombieForTest"));

        levelForTest.transform.position = new Vector3(0, 0, 0);
        zombieForTest.transform.position = new Vector3(0, 0, 1);

        LevelMap levelMapTest = levelForTest.GetComponent<LevelMap>();
        ZombieBotfromAstarBot zombieBot = zombieForTest.GetComponent<ZombieBotfromAstarBot>();

        zombieBot._levelMap = levelMapTest;
        zombieBot.currenPointToGo = new Vector3(3, 0, 1);


        yield return new WaitForFixedUpdate();

        var (moveDirection, viewDirection, shoot) = zombieBot.CurrentInput();

        yield return new WaitForSeconds(0.05f);

        Assert.AreEqual(moveDirection.normalized, Vector3.right);
        Object.Destroy(levelForTest);
        Object.Destroy(zombieForTest);
    }


    [UnityTest]
    public IEnumerator TestForBotGoesLeftIfPointInFrontOfHimButHeCanGoToThatPointThroughLeft()
    {
        GameObject levelForTest = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/LevelForTest"));
        GameObject zombieForTest = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/ZombieForTest"));

        levelForTest.transform.position = new Vector3(0, 0, 0);
        zombieForTest.transform.position = new Vector3(0, 0, 1);

        LevelMap levelMapTest = levelForTest.GetComponent<LevelMap>();
        ZombieBotfromAstarBot zombieBot = zombieForTest.GetComponent<ZombieBotfromAstarBot>();

        zombieBot._levelMap = levelMapTest;
        zombieBot.currenPointToGo = new Vector3(0, 0, 6);

        yield return new WaitForFixedUpdate();

        var (moveDirection, viewDirection, shoot) = zombieBot.CurrentInput();

        yield return new WaitForSeconds(0.05f);

        Assert.AreEqual(moveDirection.normalized, Vector3.left);
        Object.Destroy(levelForTest);
        Object.Destroy(zombieForTest);
    }
}
