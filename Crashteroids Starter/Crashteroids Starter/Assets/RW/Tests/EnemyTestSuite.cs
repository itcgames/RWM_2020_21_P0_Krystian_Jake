using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class EnemyTestSuite
{
    private Game game;

    [SetUp]
    public void Setup()
    {
        GameObject gameGameObject =
            MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
        game = gameGameObject.GetComponent<Game>();
    }

    // 1
    [UnityTest]
    public IEnumerator EnemyShipMove()
    {
        GameObject enemyShip = game.GetSpawner().SpawnShip();

        //Move Down-Left
        enemyShip.transform.GetChild(0).GetComponent<EnemyShip>().SetMoveLeft(true);
        float initialYPos = enemyShip.transform.GetChild(0).transform.position.y;
        float initialXPos = enemyShip.transform.GetChild(0).transform.position.x;

        yield return new WaitForSeconds(0.2f);

        Assert.Less(enemyShip.transform.GetChild(0).transform.position.y, initialYPos);
        Assert.Less(enemyShip.transform.GetChild(0).transform.position.x, initialXPos);

        //Move Move-Right
        enemyShip.transform.GetChild(0).GetComponent<EnemyShip>().SetMoveLeft(false);
        initialYPos = enemyShip.transform.GetChild(0).transform.position.y;
        initialXPos = enemyShip.transform.GetChild(0).transform.position.x;

        yield return new WaitForSeconds(0.2f);

        Assert.Less(enemyShip.transform.GetChild(0).transform.position.y, initialYPos);
        Assert.Greater(enemyShip.transform.GetChild(0).transform.position.x, initialXPos);
    }
}
