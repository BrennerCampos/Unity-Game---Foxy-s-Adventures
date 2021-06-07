using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] soundEffects;

    public AudioSource BGM, levelEndMusic;

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


    public void PlaySFX(int soundToPlay)
    {
        // Stop any other already playing instance of this sound effect
        soundEffects[soundToPlay].Stop();

        soundEffects[soundToPlay].pitch = Random.Range(0.9f, 1.1f);

        // Then play the sound effect from the array
        soundEffects[soundToPlay].Play();

    }

}
