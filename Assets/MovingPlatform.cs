using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float timeComplete;
    public bool moveRight;
    public bool moveUp;
    public float xDistance;
    public float yDistance;

    private float xStart;
    private float xFinal;

    private float yStart;
    private float yFinal;

    private float moveSpeed;

    private void Start()
    {
        xStart = transform.position.x + xDistance;
        xFinal = transform.position.x - xDistance;

        yStart = transform.position.y + yDistance;
        yFinal = transform.position.y - yDistance;

        moveSpeed = Mathf.Sqrt(xDistance * xDistance + yDistance * yDistance)/timeComplete;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > xStart)
            moveRight = false;
        if (transform.position.x < xFinal)
            moveRight = true;

        if (transform.position.y > yStart)
            moveUp = false;
        if (transform.position.y < yFinal)
            moveUp = true;

        if (moveRight)
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
        else
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);

        if (moveUp)
            transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
        else
            transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player _player = collision.collider.GetComponent<Player>();
        if (_player != null)
        {
            collision.collider.transform.SetParent(transform);
            collision.collider.GetComponent<PlayerController2D>().refreshJump();
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Player _player = collision.collider.GetComponent<Player>();
        if (_player != null)
        {
            collision.collider.transform.SetParent(null);
            collision.collider.GetComponent<PlayerController2D>().refreshJump();
        }

    }
}
