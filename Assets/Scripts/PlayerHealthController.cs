using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    // Singleton Creation
    // Version of this script in which only one version can exist

    public static PlayerHealthController instance;

    public int currentHealth, maxHealth;
    
    public float iFrameLength;
    private float iFrameCounter;

    private SpriteRenderer spriteRenderer;

    public GameObject deathEffect;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        // if we are invincibility mode
        if (iFrameCounter > 0)
        {
            // Time.deltaTime is the amount between 2 frames in your FPS, otherwise, = 1 second
            iFrameCounter -= Time.deltaTime;

            if (iFrameCounter <= 0)
            {
                // Changes our alpha back to fully opaque after invincibility frames end
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g,
                    spriteRenderer.color.b, 1f);
            }
        }
    }


    public void DealDamage()
    {

        if(iFrameCounter <=0)
        {
            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                
                // removes player from game

                Instantiate(deathEffect, transform.position, transform.rotation);

                LevelManager.instance.RespawnPlayer();
            }
            else
            {
                iFrameCounter = iFrameLength;
                
                // Fade player by half of the alpha
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g,
                    spriteRenderer.color.b, 0.5f);

                PlayerController.instance.Knockback();

                AudioManager.instance.PlaySFX(9);

            }

            UIController.instance.UpdateHealthDisplay();

        }

    }

    public void HealPlayer()
    {

        currentHealth++;
        
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.UpdateHealthDisplay();

    }


}
