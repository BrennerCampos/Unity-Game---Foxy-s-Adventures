using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;
    public Rigidbody2D rigidBody;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    public float moveSpeed;
    public float jumpForce;
    public float bounceForce;
    public float knockbackLength, knockbackForce;

    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded;
    private bool canDoubleJump;
    private float knockbackCounter;


    // Creates a PlayerController instance constructor before game starts
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Creates an Animator and Sprite Renderer for the Player
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // If we are not knocked back...
        if (knockbackCounter <= 0)
        {
            // Move our Player's rigid body's x-position based on our set move speed
            rigidBody.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), rigidBody.velocity.y);

            // Checks to see if we are on the ground with a circle overlap underneath Player and creates a bool
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

            // If we are grounded... 
            if (isGrounded)
            {
                // Double jump ability available
                canDoubleJump = true;
            }

            // If "Jump" button is pressed...
            if (Input.GetButtonDown("Jump"))
            {
                // If we are grounded...
                if (isGrounded)
                {
                    // Changes y-position based on our jump force value
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                    // Play "Player Jump" SFX
                    AudioManager.instance.PlaySFX(10);
                }
                else
                {
                    // Otherwise, if not grounded, and Player can still double jump...
                    if (canDoubleJump)
                    {
                        // Changes y-position based on our jump force value
                        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                        // Take away double jump availability
                        canDoubleJump = false;
                        // Play "Player Jump" SFX
                        AudioManager.instance.PlaySFX(10);
                    }
                }
            }

            // Checks which way the Player is headed and flips sprite accordingly
            if (rigidBody.velocity.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (rigidBody.velocity.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
        else    // If Player is knocked back...
        {
            // Continue running down the Player's knockback counter
            knockbackCounter -= Time.deltaTime;
            
            // Knock back our player in the opposite direction that the Player's sprite is facing
            if (!spriteRenderer.flipX)
            {
                rigidBody.velocity = new Vector2(-knockbackForce, rigidBody.velocity.y);
            }
            else
            {
                rigidBody.velocity = new Vector2(knockbackForce, rigidBody.velocity.y);
            }
        }

        // Sets parameters used by our Animator based on current Update loop's variable values
        anim.SetFloat("moveSpeed", Mathf.Abs(rigidBody.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
    }

    public void Knockback()
    {
        // Sets our knockback counter to our predefined knockback length
        knockbackCounter = knockbackLength;
        // Pops the Player up with our predefined knockback force
        rigidBody.velocity = new Vector2(0f, knockbackForce);
        // Change Player's sprite animation to 'Hurt'
        anim.SetTrigger("isHurt");
    }

    public void Bounce()
    {
        // Bounces the Player up with our predefined bounce force
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, bounceForce);
        // Play "Player Jump" SFX
        AudioManager.instance.PlaySFX(10);
    }
}
