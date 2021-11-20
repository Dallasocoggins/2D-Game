using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public int Damage;
    public int explosionStrength = 100;

    private void Start()
    {

    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.collider.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.DamageEnemy(Damage);
            Debug.Log("Projectile hit " + " and did " + Damage + " damage.");
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 10f);
        }

        if (collision.collider.gameObject.name == "Bouncy object" || collision.collider.gameObject.name == "Player" || collision.collider.gameObject.name == "enemy")
            collision.rigidbody.AddExplosionForce(explosionStrength, this.transform.position, 5);

        collision.rigidbody.AddExplosionForce(explosionStrength, this.transform.position, 5);

    }
}
