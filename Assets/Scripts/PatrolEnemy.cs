using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : Enemy
{


    [Header("Optional")]
    [SerializeField]
    private StatusIndicator statusIndicator;

    private void Start()
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
    }

    public float speed;

    private bool movingRight = true;

    public Transform groundDetection;
    public Transform enemyGraphics;
    


    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f);
        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                enemyGraphics.eulerAngles = new Vector3(0, -180, 0);
                speed *= -1;
                movingRight = false;
            }
            else
            {
                enemyGraphics.eulerAngles = new Vector3(0, 0, 0);
                speed *= -1;
                movingRight = true;
            }
        }
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
        Player _player = _colInfo.collider.GetComponent<Player>();
        if (_player != null && _player.getCurHealth() > 0)
        {
            _player.DamagePlayer(stats.damage);
        }
    }
}
