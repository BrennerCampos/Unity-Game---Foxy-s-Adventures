using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{


    public float moveSpeed, moveTime, waitTime;
    private float moveCount, waitCount;

    public Transform leftPoint, rightPoint;

    private bool movingRight;

    private Rigidbody2D rigidbody;

    public SpriteRenderer spriteRenderer;

    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        leftPoint.parent = null;
        rightPoint.parent = null;

        movingRight = true;

        moveCount = moveTime;
    }

    // Update is called once per frame
    void Update()
    {

        if (moveCount > 0)
        {

            moveCount -= Time.deltaTime;

            if (movingRight)
            {
                rigidbody.velocity = new Vector2(moveSpeed, rigidbody.velocity.y);

                spriteRenderer.flipX = true;

                if (transform.position.x > rightPoint.position.x)
                {
                    movingRight = false;
                }
            }
            else
            {
                rigidbody.velocity = new Vector2(-moveSpeed, rigidbody.velocity.y);

                spriteRenderer.flipX = false;

                if (transform.position.x < leftPoint.position.x)
                {
                    movingRight = true;
                }

            }

            if (moveCount <= 0)
            {
                // choose random time between 3/4th of our wait time and 1 1/4 of our wait time
                waitCount = Random.Range(waitTime * 0.75f,  waitTime * 1.25f);
            }

            anim.SetBool("isMoving", true);
        }
        else if (waitCount > 0)
        {

            waitCount -= Time.deltaTime;
            
            // Telling enemy to stand still
            rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y);

            if (waitCount <= 0)
            {
                moveCount = Random.Range(moveTime * 0.75f, waitTime * 0.75f);
            }
            
            anim.SetBool("isMoving", false);

        }

        
    }
}
