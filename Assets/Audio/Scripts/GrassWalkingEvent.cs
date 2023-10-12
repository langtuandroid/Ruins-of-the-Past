using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using UnityEngine.Networking;


public class GrassWalkingEvent : MonoBehaviour
{
    //FMOD Event instances
    private EventInstance foxFootstepsGrass;
    private EventInstance wolfFootstepsGrass;

    //Public variables
    public bool enable;
    public GameObject foxObject;
    public GameObject wolfObject;
    private Rigidbody _foxRigidBody, _wolfRigidBody;

    private void Start()
    {
        _foxRigidBody = foxObject.GetComponent<Rigidbody>();
        _wolfRigidBody = wolfObject.GetComponent<Rigidbody>();
        foxFootstepsGrass = AudioManager.instance.CreateEventInstance(EventManager.instance.foxWalk);
        wolfFootstepsGrass = AudioManager.instance.CreateEventInstance(EventManager.instance.wolfWalk);
    }

    private void FixedUpdate()
    {
        UpdateSound(foxFootstepsGrass, _foxRigidBody);
        UpdateSound(wolfFootstepsGrass, _wolfRigidBody);
    }

    private void UpdateSound(EventInstance eventInstance, Rigidbody rb)
    {
        var t = rb.transform;
        var result = Physics.Raycast(
            t.position + new Vector3(0.0f, 0.0001f, 0.0f),
            -t.up,
            out var ray
        );

        if (!enable ||
            !result ||
            ray.distance > 0.1 ||
            (rb.velocity.z > -0.5 && rb.velocity.z < 0.5))
        {
            eventInstance.stop(STOP_MODE.ALLOWFADEOUT);
            return;
        }

        PLAYBACK_STATE currentSoundState;
        eventInstance.getPlaybackState(out currentSoundState);
        if (currentSoundState == PLAYBACK_STATE.STOPPED)
        {
            //Start event
            eventInstance.start();
        }
    }
}