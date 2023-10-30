using UnityEngine;
using FMOD.Studio;

public class AmbienceMaster : MonoBehaviour
{
    private EventInstance birdAmbienceEvent;
    public float eventState;

    // Start is called before the first frame update
    void Start()
    {
        birdAmbienceEvent = AudioManager.instance.CreateEventInstance(EventManager.instance.birdEvent);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        birdAmbienceEvent.setParameterByName("BirdVolume", eventState);

        PLAYBACK_STATE currentSoundState;
        birdAmbienceEvent.getPlaybackState(out currentSoundState);
        if (currentSoundState == PLAYBACK_STATE.STOPPED)
        {
            //Start event
            birdAmbienceEvent.start();
        }
    }
}
