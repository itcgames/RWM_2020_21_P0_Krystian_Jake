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
        game.NewGame();

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

    // 2
    [UnityTest]
    public IEnumerator EnemyLaserMovesDown()
    {
        game.NewGame();

        GameObject enemyShip = game.GetSpawner().SpawnShip();
        GameObject enemyLaser = enemyShip.transform.GetChild(0).GetComponent<EnemyShip>().SpawnLaser();

        float initialYPos = enemyLaser.transform.position.y;
        yield return new WaitForSeconds(0.1f);

        Assert.Less(enemyLaser.transform.position.y, initialYPos);
    }

    // 3
    [UnityTest]
    public IEnumerator EnemyLaserCollision()
    {
        game.NewGame();

        GameObject enemyShip = game.GetSpawner().SpawnShip();
        GameObject enemyLaser = enemyShip.transform.GetChild(0).GetComponent<EnemyShip>().SpawnLaser();
        enemyLaser.transform.position = Vector3.zero;

        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;

        yield return new WaitForSeconds(0.1f);

        UnityEngine.Assertions.Assert.IsNull(enemyLaser);

        enemyLaser = enemyShip.transform.GetChild(0).GetComponent<EnemyShip>().SpawnLaser();
        enemyLaser.transform.position = Vector3.zero;

        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);

        UnityEngine.Assertions.Assert.IsNull(enemyLaser);
        UnityEngine.Assertions.Assert.IsNull(laser);

        enemyLaser = enemyShip.transform.GetChild(0).GetComponent<EnemyShip>().SpawnLaser();
        enemyLaser.transform.position = game.GetShip().transform.position;

        yield return new WaitForSeconds(0.1f);

        Assert.True(game.isGameOver);
    }

    // 4
    [UnityTest]
    public IEnumerator EnemyLaserOutOfBounds()
    {
        game.NewGame();

        GameObject enemyShip = game.GetSpawner().SpawnShip();
        GameObject enemyLaser = enemyShip.transform.GetChild(0).GetComponent<EnemyShip>().SpawnLaser();
        enemyLaser.transform.position = new Vector3(0,-10,0);

        yield return new WaitForSeconds(0.1f);

        UnityEngine.Assertions.Assert.IsNull(enemyLaser);
    }

    // 5
    [UnityTest]
    public IEnumerator EnemyShipOutOfBounds()
    {
        game.NewGame();

        GameObject enemyShip = game.GetSpawner().SpawnShip();
        enemyShip.transform.GetChild(0).transform.position = new Vector3(0, -5, 0);

        yield return new WaitForSeconds(0.1f);

        UnityEngine.Assertions.Assert.IsNull(enemyShip);
    }

    // 6
    [UnityTest]
    public IEnumerator LaserDestroyedEnemyShip()
    {
        game.NewGame();

        GameObject enemyShip = game.GetSpawner().SpawnShip();
        enemyShip.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        
        UnityEngine.Assertions.Assert.IsNull(enemyShip);
    }

    //7
    [UnityTest]
    public IEnumerator DestroyedEnemyShipRaisesScore()
    {
        game.NewGame();

        GameObject enemyShip = game.GetSpawner().SpawnShip();
        enemyShip.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(game.score, 2);
    }
}
