using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;


public class GrassWalkingEvent : MonoBehaviour
{
    private EventInstance footstepsGrass;
    public bool enable;

    private void Start()
    {
        footstepsGrass = AudioManager.instance.CreateEventInstance(EventManager.instance.testSFX);
    }

    private void FixedUpdate()
    {
        UpdateSound();
    }

    private void UpdateSound()
    {
        if(enable == true)
        {
            PLAYBACK_STATE playbackState;
            footstepsGrass.getPlaybackState(out playbackState);
            if (playbackState == PLAYBACK_STATE.STOPPED)
            {
                footstepsGrass.start();
            }
        }
        else
        {
            footstepsGrass.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
}
