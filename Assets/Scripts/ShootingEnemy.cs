﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    [Header("Optional")]
    [SerializeField]
    private StatusIndicator statusIndicator;


    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;

    private Transform player;

    public int jumpForce;

    public Transform wallCheckRight;
    public Transform wallCheckLeft;
    public float checkRadius;
    private bool isWallNearRight;
    private bool isWallNearLeft;
    public LayerMask whatIsGround;

    private Rigidbody2D rb;

    public Transform groundDetectionRight;
    public Transform groundDetectionLeft;

    // Start is called before the first frame update
    void Start()
    {
        stats.Init();

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }

        if (deathParticles == null)
        {
            Debug.LogError("No enemy Death particles assigned");
        }

        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBtwShots = startTimeBtwShots;

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D groundInfoRight = Physics2D.Raycast(groundDetectionRight.position, Vector2.down, 2f);
        RaycastHit2D groundInfoLeft = Physics2D.Raycast(groundDetectionLeft.position, Vector2.down, 2f);

        if (groundInfoLeft.collider == false && groundInfoRight.collider == false)
        {
            
        }
        else if(isWallNearRight || isWallNearLeft)
        {
            rb.velocity = Vector2.up * jumpForce;

        } else if (groundInfoRight.collider == false)
        {
            rb.velocity = Vector2.left;
        }
        else if (groundInfoLeft.collider == false)
        {
            rb.velocity = Vector2.right;
        }
        else if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        } else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {

            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

        if(timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        } else
        {
            timeBtwShots -= Time.deltaTime;
        }

    }


    //Wall Check doesn't work :(
    void FixedUpdate()
    {
        isWallNearRight = Physics2D.OverlapCircle(wallCheckRight.position, checkRadius, whatIsGround);
        isWallNearLeft = Physics2D.OverlapCircle(wallCheckLeft.position, checkRadius, whatIsGround);

        Debug.Log("Wall Check Right:" + isWallNearRight);
        Debug.Log("Wall Check Left:" + isWallNearLeft);

    }

    public void CheckHealth()
    {
        if (stats.curHealth <= 0)
        {
            if (stats.curHealth <= 0)
            {
                GameMaster.KillEnemy(this);
            }
        }

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }

    private void OnCollisionEnter2D(Collision2D _colInfo)
    {
        //Nothing is here because otherwise it would inheret the exploding from the enemy class
    }

}
