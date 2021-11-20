using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserOffOn : MonoBehaviour
{
    public float timeOff;
    public float timeOn;
    public int damage = 1000000;
    SpriteRenderer sprite;
    BoxCollider2D collide;


    // Update is called once per frame
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        collide = GetComponent<BoxCollider2D>();
        StartCoroutine(offOn());
    }

    IEnumerator offOn()
    {
        while (true)
        {
            sprite.enabled = false;
            collide.enabled = false;

            yield return new WaitForSeconds(timeOff);

            sprite.enabled = true;
            collide.enabled = true;

            yield return new WaitForSeconds(timeOn);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Player _player = other.GetComponent<Player>();
        if (_player != null && _player.getCurHealth() > 0)
        {
            _player.DamagePlayer(damage);
        }
    }
}
