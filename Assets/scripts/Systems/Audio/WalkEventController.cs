using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEventController : MonoBehaviour
{
    public GameObject targetObject;

    private GameObject grassSoundEmitter;
    private GameObject stoneSoundEmitter;
    private GameObject woodSoundEmitter;
    private GameObject waterSoundEmitter;
    private string groundType;

    // Start is called before the first frame update
    void Start()
    {
        grassSoundEmitter = this.transform.GetChild(0).gameObject;
        stoneSoundEmitter = this.transform.GetChild(1).gameObject;
        woodSoundEmitter = this.transform.GetChild(2).gameObject;
        waterSoundEmitter = this.transform.GetChild(3).gameObject;

        grassSoundEmitter.SetActive(false);
        stoneSoundEmitter.SetActive(false);
        woodSoundEmitter.SetActive(false);
        waterSoundEmitter.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateState(VelocityZ(targetObject), GroundDistance(targetObject), groundType);
        this.transform.position = targetObject.transform.position;
    }

    private float GroundDistance(GameObject gameObject) //Return distance from ground for object
    {
        //Create raycast
        RaycastHit rcHit;

        //Setup vector
        var objectRB = gameObject.GetComponent<Rigidbody>();
        Vector3 originPos = objectRB.position;
        originPos.y = originPos.y + 0.1f;
        Ray downRay = new Ray(originPos, -Vector3.up);

        //Execute vector
        if (Physics.Raycast(downRay, out rcHit))
        {
            GameObject hitObject = rcHit.collider.gameObject;
            if (hitObject?.GetComponent<GroundSFXController>())
            {
                groundType = hitObject.GetComponent<GroundSFXController>().groundType.ToString();
                return (rcHit.distance);
            }
            return (rcHit.distance);
        }
        else return 0; //If raycast does not hit ground set failsafe
    }

    private float VelocityZ(GameObject gameObject) //Return velocity of object
    {
        var objectRB = gameObject.GetComponent<Rigidbody>();
        return (objectRB.velocity.z);
    }

    private void UpdateState(float zVelocity, float dist, string groundType) //Update state of the sound emitter when certain criteria are met.
    {
        if ((zVelocity <= -0.5 && dist <= 0.2) || (zVelocity >= 0.5 && dist <= 0.2))
        {
            if(groundType == "Grass")
            {
                grassSoundEmitter.SetActive(true);
                stoneSoundEmitter.SetActive(false);
                woodSoundEmitter.SetActive(false);
            }
            if (groundType == "Stone")
            {
                grassSoundEmitter.SetActive(false);
                stoneSoundEmitter.SetActive(true);
                woodSoundEmitter.SetActive(false);
            }
            if (groundType == "Wood")
            {
                woodSoundEmitter.SetActive(true);
                stoneSoundEmitter.SetActive(false);
                grassSoundEmitter.SetActive(false);
            }
        }
        else { grassSoundEmitter.SetActive(false); stoneSoundEmitter.SetActive(false); woodSoundEmitter.SetActive(false);}
    }
}
