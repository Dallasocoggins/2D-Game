using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public int Damage;

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
        } else
        {
            Destroy(gameObject, 10f);
        }
        
    }
}
