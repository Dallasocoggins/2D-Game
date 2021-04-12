using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;
    private Animator anim;
    Transform playerGraphics;

    private bool isFacingRight = true;

    private bool isGrounded;
    private bool isWalking;
    private bool canMove;
    private bool canFlip;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsValue;
    private int facingDirection = 1;

    public float dashCoolDown;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction = 0;
    public float distanceBetweenImages;
    private float lastImageXpos;
    private float lastDash = -100f;
    private float dashTimeLeft;

    private bool isDashing;
    private bool canDash = true;

    IEnumerator dashCoroutine;

    private void Start()
    {
        extraJumps = extraJumpsValue;
        playerGraphics = transform.Find("Graphics");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        //Right key press moveInput = 1, Left key press moveInput = -1
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (isFacingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (isFacingRight == true && moveInput < 0)
        {
            Flip();
        }


        if (Mathf.Abs(rb.velocity.x) >= 0.01f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;

        }

       if (isDashing)
       {
           Debug.Log("Should Dash");
            if (isFacingRight)
            {
                rb.velocity = Vector2.right * dashSpeed;
            }
            else
            {
                rb.velocity = Vector2.left * dashSpeed;
            }
            
        }

    }

    private void Update()
    {
        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetButtonDown("Jump") && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }




        /**if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Time.time >= (lastDash + dashCoolDown))
                AttemptToDash();
        }
        CheckDash(); **/

        //Dash();

        /**if (Input.GetKeyDown(KeyCode.LeftShift) && canDash == true)
        {
            Debug.Log("Pressed shift");
            if (dashCoroutine != null)
            {
                Debug.Log("Ending Coroutine");
                StopCoroutine(dashCoroutine);
            } 
            dashCoroutine = Dash(dashTime, dashCoolDown);
            StartCoroutine(dashCoroutine);
        } **/

        Dash();

        UpdateAnimations();
    }

    /**
    private void AttemptToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        PlayerAfterImagePool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;
    }

    private void CheckDash()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                canMove = false;
                canFlip = false;
                rb.velocity = new Vector2(dashSpeed * facingDirection, 0.0f);
                dashTimeLeft -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    PlayerAfterImagePool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
            }

            if (dashTimeLeft <= 0)
            {
                isDashing = false;
                canMove = true;
                canFlip = true;
            }

        }
    } **/


    private void UpdateAnimations()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }


    private void Flip()
    {
        Debug.Log("Flip");
        facingDirection *= -1;
        isFacingRight = !isFacingRight;
        Vector3 theScale = playerGraphics.localScale;
        theScale.x *= -1;
        playerGraphics.localScale = theScale;

    }


     private void Dash()
    {
        if (direction == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {

                Debug.Log("PRESSED SHIFT");
                if (Time.time >= (lastDash + dashCoolDown))
                {
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        //Dash up
                        direction = 1;
                    }
                    else if (Input.GetKeyDown(KeyCode.S))
                    {
                        //Dash DOWN
                        direction = 2;
                    }
                    else if (!isFacingRight)
                    {
                        Debug.Log("SHOULD DASH LEFT 1");
                        direction = 3;
                    }
                    else 
                    {
                        Debug.Log("SHOULD DASH RIGHT 1");
                        direction = 4;
                    }
                }
            }

        }
        else
        {
            if (dashTime <= 0)
            {
                Debug.Log("SHOULD End DASH");
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
                rb.gravityScale = 2;
                canMove = true;
                isDashing = false;
            }
            else
            {
                dashTime -= Time.deltaTime;

                Debug.Log("SHOULD DASH ");
                canMove = false;
                isDashing = true;
                lastDash = Time.time;

                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    PlayerAfterImagePool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }

            }
        } 
    }

   /** IEnumerator Dash(float dashTime, float dashCooldown)
    {
        Debug.Log("IsDashing true");
        isDashing = true;
        canDash = false;
        Debug.Log("canDash false");
        yield return new WaitForSeconds(dashTime);
        Debug.Log("IsDashing false");
        isDashing = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(dashCoolDown);
        Debug.Log("canDash true");
        canDash = true;
    } 
   **/
}


