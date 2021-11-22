using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Game;
using Input;
public class NewTestScript
{
    [UnityTest]
    public IEnumerator OnStraightLineLeftBotGoesLeft()
    {
        GameObject levelForTest = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/LevelForTest"));
        GameObject zombieForTest = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/ZombieForTest"));


        LevelMap levelMapTest = levelForTest.GetComponent<LevelMap>();
        ZombieBotfromAstarBot zombieBot = zombieForTest.GetComponent<ZombieBotfromAstarBot>();


        yield return new WaitForSeconds(0.1f);

        UnityEngine.Assertions.Assert.IsNull(zombieForTest);
        Object.Destroy(levelForTest);
        Object.Destroy(zombieForTest);
    }
}
