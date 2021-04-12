using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMove : MonoBehaviour
{
    private float moveInput;

    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction = 0;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        if (direction == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log("PRESSED SHIFT");
                if (moveInput < 0)
                {
                    Debug.Log("SHOULD DASH LEFT 1");
                    direction = 1;
                }
                else if (moveInput > 0)
                {
                    Debug.Log("SHOULD DASH RIGHT 1");
                    direction = 2;
                }
            }

        }
        else
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;

                if (direction == 1)
                {
                    Debug.Log("SHOULD DASH LEFT 2");
                    rb.velocity = Vector2.left * dashSpeed;
                }
                else if (direction == 2)
                {
                    Debug.Log("SHOULD DASH RIGHT 2");
                    rb.velocity = Vector2.right * dashSpeed;
                }
            }
        }
    }
}
