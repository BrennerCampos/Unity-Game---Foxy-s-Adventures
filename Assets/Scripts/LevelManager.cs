using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;
    public float waitToRespawn;
    public int gemsCollected;


    // Object Constructor
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void RespawnPlayer()
    {
        // Coroutines must be started with "StartCoroutine"
        StartCoroutine(RespawnCo());
    }

    // CoRoutine
    private IEnumerator RespawnCo()
    {
        // Deactivates our player
        PlayerController.instance.gameObject.SetActive(false);
        
        // Plays 'Death' SFX
        AudioManager.instance.PlaySFX(8);

        // Pauses the further execution of the script until our waitToRespawn timer is complete
        yield return new WaitForSeconds(waitToRespawn);

        // Reactivates our player
        PlayerController.instance.gameObject.SetActive(true);
        // Spawns our player's position at our last checkpoint
        PlayerController.instance.transform.position = CheckpointController.instance.spawnPoint;
        // Resets our player health to be full...
        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
        
        // And updates our UI accordingly
        UIController.instance.UpdateHealthDisplay();
    }
}



