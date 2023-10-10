using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderSoundScript : MonoBehaviour
{
    //This is all placeholder stuff to test if FMOD is working.
    //This script will be removed when proper systems are implemented.
    public int placeholder;

    private void FixedUpdate()
    {
        placeholder++;
        if (placeholder == 120)
        {
            placeholder = 0;
            PlaySound();
        }
    }

    private void PlaySound()
    {
        AudioManager.instance.PlayOneShot(EventManager.instance.testSFX, this.transform.position);
    }
}
