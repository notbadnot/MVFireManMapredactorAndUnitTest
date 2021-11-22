using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Game;
using Input;

public class Ave
{
    // A Test behaves as an ordinary method
    /*[Test]
    public void AveSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.*/
    /*[UnityTest]
    public IEnumerator AveWithEnumeratorPasses()
    {
        GameObject levelForTest = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/LevelForTest"));
        GameObject zombieForTest = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/ZombieForTest"));


        LevelMap levelMapTest = levelForTest.GetComponent<LevelMap>();
        ZombieBotfromAstarBot zombieBot = zombieForTest.GetComponent<ZombieBotfromAstarBot>();


        yield return new WaitForSeconds(0.1f);

        UnityEngine.Assertions.Assert.IsNull(zombieForTest);
        Object.Destroy(levelForTest);
        Object.Destroy(zombieForTest);
    }*/
}
