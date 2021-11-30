using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip dirtSound, grassSound;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        grassSound = Resources.Load<AudioClip>("Grass");
        dirtSound = Resources.Load<AudioClip>("Dirt");

        audioSrc = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "Grass":
                audioSrc.PlayOneShot(grassSound);
                    break;
            case "Dirt":
                audioSrc.PlayOneShot(dirtSound);
                break;
        }
    }
}
