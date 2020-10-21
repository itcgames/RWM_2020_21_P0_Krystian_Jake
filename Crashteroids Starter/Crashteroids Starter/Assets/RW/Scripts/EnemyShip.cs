using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    public float speed = 1;
    public bool canShoot = false;
    public bool moveLeft = false;

    [SerializeField]
    private MeshRenderer mesh;

    private float maxY = -5;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
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
    }

    public void SetMoveLeft(bool t_moveLeft)
    {
        moveLeft = t_moveLeft;
    }
}
