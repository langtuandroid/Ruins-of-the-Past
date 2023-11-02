using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstance;

    public static AudioManager instance { get; private set;}    //Create singleton instance

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one audio manager is active in the scene.");
        }
        instance = this;
        eventInstance = new List<EventInstance>();
    }

    public void PlayOneShot(EventReference sound, Vector3 soundSourcePos)   //Method calling one-off FMOD events
    {
        RuntimeManager.PlayOneShot(sound, soundSourcePos);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)     //Method instancing FMOD events and running them
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        this.eventInstance.Add(eventInstance);
        return eventInstance;
    }

    private void CleanUp()  //Clean-up events in memory upon scene deactivation
    {
        foreach(EventInstance eventInstance in eventInstance)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }

    private void OnDestroy()
    {
        CleanUp();
    }
}
