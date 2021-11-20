using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public int jumpForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /** Player specfic jumppad
          
         Player _player = collision.GetComponent<Player>();
         if (_player != null && _player.getCurHealth() > 0)
         {
             _player.GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
         } 
        **/

        //general jumppad
        
        if (collision.GetComponent<Rigidbody2D>() != null)
        {
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
        }
    }
}
