using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    public float speed;
    public int damage;

    private Transform player;
    private Vector2 target;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        //change target to player.position to make the shots homing shots.
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Commented out part is supposed to make the object die when it hits the ground but it doesn't work for some reason.
        if (other.CompareTag("Player") /**|| **other.CompareTag("Ground") **/ )
        {
            DestroyProjectile();
        }

        Player _player = other.GetComponent<Player>();
        if (_player != null && _player.getCurHealth() > 0)
        {
            _player.DamagePlayer(damage);
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }


}
