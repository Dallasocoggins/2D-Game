using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpRefresh : MonoBehaviour
{
    public bool singleUse;

    private void OnTriggerEnter2D(Collider2D collision) { 
          
         Player _player = collision.GetComponent<Player>();
         if (_player != null && _player.getCurHealth() > 0)
         {
            _player.GetComponent<PlayerController2D>().refreshJump();
         }

        if (singleUse)
        {
            Destroy(gameObject);
        }
    }
}
