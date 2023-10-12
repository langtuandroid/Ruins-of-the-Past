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

    //Private variables
    private float foxZvol;
    private float foxDist;

    private float wolfZvol;
    private float wolfDist;

    private void Start()
    {
        //Initialize events
        foxFootstepsGrass = AudioManager.instance.CreateEventInstance(EventManager.instance.foxWalk);
        wolfFootstepsGrass = AudioManager.instance.CreateEventInstance(EventManager.instance.wolfWalk);
    }

    private void FixedUpdate()
    {
        //Grab velocity of fox and wolf
        Rigidbody foxRB = foxObject.GetComponent<Rigidbody>();
        foxZvol = foxRB.velocity.z;

        Rigidbody wolfRB = wolfObject.GetComponent<Rigidbody>();
        wolfZvol = wolfRB.velocity.z;

        CheckGround();
        UpdateSound();
    }

    private void CheckGround() //Checks if fox and wolf are above ground
    {
        //Create raycasthit
        RaycastHit foxHit;
        RaycastHit wolfHit;

        //Fox raycast
        Rigidbody foxRB = foxObject.GetComponent<Rigidbody>();
        Vector3 foxOrigin = foxRB.position;
        Ray foxDownRay = new Ray(foxOrigin, -Vector3.up);

        //Wolf raycast
        Rigidbody wolfRB = wolfObject.GetComponent<Rigidbody>();
        Vector3 wolfOrigin = wolfRB.position;
        Ray wolfDownRay = new Ray(wolfOrigin, -Vector3.up);

        //Execute raycasts
        if (Physics.Raycast(foxDownRay, out foxHit))
        {
            foxDist = foxHit.distance;
        }
        if (Physics.Raycast(wolfDownRay, out wolfHit))
        {
            wolfDist = wolfHit.distance;
        }
    }

    private void UpdateSound() //Updates sound events according to object state
    {
        //Check if proper conditions are met for fox
        if(enable == true && foxZvol <= -0.5 && foxDist <= 0.1 || enable == true && foxZvol >= 0.5 && foxDist <= 0.1)
        {
            //Check current playback state
            PLAYBACK_STATE foxPlaybackState;
            foxFootstepsGrass.getPlaybackState(out foxPlaybackState);
            if (foxPlaybackState == PLAYBACK_STATE.STOPPED)
            {
                //Start event
                foxFootstepsGrass.start();
            }
        }
        else
        {
            //Stop event
            foxFootstepsGrass.stop(STOP_MODE.ALLOWFADEOUT);
        }

        //Check if proper conditions are met for wolf
        if (enable == true && wolfZvol <= -0.5 && wolfDist <= 0.1 || enable == true && wolfZvol >= 0.5 && wolfDist <= 0.1)
        {
            //Check current playback state
            PLAYBACK_STATE wolfPlaybackState;
            wolfFootstepsGrass.getPlaybackState(out wolfPlaybackState);
            if (wolfPlaybackState == PLAYBACK_STATE.STOPPED)
            {
                //Start event
                wolfFootstepsGrass.start();
            }
        }
        else
        {
            //Stop event
            wolfFootstepsGrass.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
}
