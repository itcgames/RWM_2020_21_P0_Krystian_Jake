﻿using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestSuite
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
    public IEnumerator AsteroidsMoveDown()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        float initialYPos = asteroid.transform.position.y;
        yield return new WaitForSeconds(0.1f);
  
        Assert.Less(asteroid.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator GameOverOccursOnAsteroidCollision()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = game.GetShip().transform.position;
        yield return new WaitForSeconds(0.1f);

        Assert.True(game.isGameOver);
    }
    [UnityTest]
    public IEnumerator NewGameRestartsGame()
    {
        //1
        game.isGameOver = true;
        game.NewGame();
        //2
        Assert.False(game.isGameOver);
        yield return null;
    }
    [UnityTest]
    public IEnumerator LaserMovesUp()
    {
        // 1
        GameObject laser = game.GetShip().SpawnLaser();
        // 2
        float initialYPos = laser.transform.position.y;
        yield return new WaitForSeconds(0.1f);
        // 3
        Assert.Greater(laser.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator LaserDestroysAsteroid()
    {
        // 1
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 2
        UnityEngine.Assertions.Assert.IsNull(asteroid);
    }

    [UnityTest]
    public IEnumerator DestroyedAsteroidRaisesScore()
    {
        // 1
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 2
        Assert.AreEqual(game.score, 1);
    }

    [UnityTest]
    public IEnumerator NewGameScore0()
    {
        Assert.AreEqual(game.score, 0);

        //Set score to 1.
        game.score = 1;

        Assert.AreEqual(game.score, 1);

        //Set game to game over and start a new one.
        game.isGameOver = true;
        game.NewGame();

        yield return new WaitForSeconds(0.1f);

        //Check result.
        Assert.AreEqual(game.score, 0);
    }

    [UnityTest]
    public IEnumerator ShipLeftRight()
    {
        GameObject ship = game.GetShip().gameObject;
        float initialXPos = ship.transform.position.x;

        yield return new WaitForSeconds(0.1f);
        
        //Moves ship Left
        game.GetShip().MoveLeft();
        //Checks if ship is greater the starting pos
        Assert.Less(ship.transform.position.x, initialXPos);

        yield return new WaitForSeconds(0.1f);

        initialXPos = ship.transform.position.x;

        //Moves ship Right
        game.GetShip().MoveRight();
        //Checks to see if its greater then starting pos.
        Assert.Greater(ship.transform.position.x, initialXPos);

    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(game.gameObject);
    }

}
