using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;


public class GrassWalkingEvent : MonoBehaviour
{
    //FMOD Event instances
    private EventInstance foxFootstepsGrass;
    private EventInstance wolfFootstepsGrass;

    //Public variables
    public bool enable;
    public GameObject foxObject;
    public GameObject wolfObject;

    private void Start()
    {
        //Initialize events
        foxFootstepsGrass = AudioManager.instance.CreateEventInstance(EventManager.instance.foxWalk);
        wolfFootstepsGrass = AudioManager.instance.CreateEventInstance(EventManager.instance.wolfWalk);
    }

    private void FixedUpdate()
    {
        //Update sound events
        UpdateSound(foxFootstepsGrass, VelocityZ(foxObject), GroundDistance(foxObject));
        UpdateSound(wolfFootstepsGrass, VelocityZ(wolfObject), GroundDistance(wolfObject));
    }

    private float VelocityZ(GameObject gameObject) //Return velocity of object
    {
        var objectRB = gameObject.GetComponent<Rigidbody>();
        return (objectRB.velocity.z);
    }

    private float GroundDistance(GameObject gameObject) //Return distance from ground for object
    {
        //Create raycast
        RaycastHit rcHit;

        //Setup vector
        var objectRB = gameObject.GetComponent<Rigidbody>();
        Vector3 originPos = objectRB.position;
        Ray downRay = new Ray(originPos, -Vector3.up);

        //Execute vector
        if (Physics.Raycast(downRay, out rcHit))
        {
            return (rcHit.distance);
        }
        else return 0; //If raycast does not hit ground set failsafe
    }

    private void UpdateSound(EventInstance eventInstance, float zVelocity, float dist) //Update sound events according to object state
    {
        //Check if proper conditions are met
        if ((enable == true && zVelocity <= -0.5 && dist <= 0.1) || (enable == true && zVelocity >= 0.5 && dist <= 0.1))
        {
            //Check current playback state
            PLAYBACK_STATE currentSoundState;
            eventInstance.getPlaybackState(out currentSoundState);
            if (currentSoundState == PLAYBACK_STATE.STOPPED)
            {
                //Start event
                eventInstance.start();
            }
        }
        else
        {
            //Stop event
            eventInstance.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
}
