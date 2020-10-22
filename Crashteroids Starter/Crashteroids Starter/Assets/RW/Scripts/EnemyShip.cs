using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    public float speed = 1;
    public bool canShoot = true;
    public bool moveLeft = false;

    [SerializeField]
    private MeshRenderer mesh;
    [SerializeField]
    private GameObject enemyLaser;
    [SerializeField]
    private Transform shotSpawn;

    private float maxY = -5;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot)
        {
            StartCoroutine("ShootLaser");
        }
        Move();
    }

    IEnumerator ShootLaser()
    {
        canShoot = false;
        GameObject laserShot = SpawnLaser();
        laserShot.transform.position = shotSpawn.position;
        yield return new WaitForSeconds(1.0f);
        canShoot = true;
    }

    public GameObject SpawnLaser()
    {
        GameObject newLaser = Instantiate(enemyLaser);
        newLaser.SetActive(true);
        return newLaser;
    }

    public void Move()
    {
        transform.Translate(Vector3.back * Time.deltaTime * speed);

        if (moveLeft)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }

        if (transform.position.y < maxY)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    public void SetMoveLeft(bool t_moveLeft)
    {
        moveLeft = t_moveLeft;
    }
}
