using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;


    public float moveSpeed;
    public float jumpForce;
    public Rigidbody2D rigidBody;

    private bool isGrounded;
    private bool canDoubleJump;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    private Animator anim;
    private SpriteRenderer spriteRenderer;

    public float knockbackLength, knockbackForce;
    private float knockbackCounter;

    public float bounceForce;


    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (knockbackCounter <= 0)
        {
            rigidBody.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), rigidBody.velocity.y);

            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

            if (isGrounded)
            {
                canDoubleJump = true;
            }

            if (Input.GetButtonDown("Jump"))
            {

                if (isGrounded)
                {
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                    AudioManager.instance.PlaySFX(10);
                }
                else
                {
                    if (canDoubleJump)
                    {
                        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                        canDoubleJump = false;
                        AudioManager.instance.PlaySFX(10);
                    }
                }

            }


            if (rigidBody.velocity.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (rigidBody.velocity.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            knockbackCounter -= Time.deltaTime;
            
            // if we are not facing to the left
            if (!spriteRenderer.flipX)
            {
                rigidBody.velocity = new Vector2(-knockbackForce, rigidBody.velocity.y);
            }
            else
            {
                rigidBody.velocity = new Vector2(knockbackForce, rigidBody.velocity.y);
            }
        }

        

        anim.SetFloat("moveSpeed", Mathf.Abs(rigidBody.velocity.x));
        anim.SetBool("isGrounded", isGrounded);

    }



    public void Knockback()
    {
        knockbackCounter = knockbackLength;
        rigidBody.velocity = new Vector2(0f, knockbackForce);

        anim.SetTrigger("isHurt");
    }

    public void Bounce()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, bounceForce);
        AudioManager.instance.PlaySFX(10);
    }

}
