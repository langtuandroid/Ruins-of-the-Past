using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderSoundScript : MonoBehaviour
{
    //This is all placeholder stuff to make sure FMOD is working.
    //This scriopt will be removed when porper systems are implemented.

    private void PlaySound()
    {
        AudioManager.instance.PlayOneShot(EventManager.instance.testSFX, this.transform.position);
    }
}
