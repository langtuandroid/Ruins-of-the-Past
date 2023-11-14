using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaterEnterEvent : MonoBehaviour
{
    public GameObject foxObject;
    public GameObject wolfObject;

    public GameObject foxWaterEmitter;
    public GameObject wolfWaterEmitter;

    private int cooldownTimer;

    //Ok voodat jullie gaan flamen wat de fuck dit cooldown timer voor nodig is, malbers heeft fucking 40 verschillende colliders die anders allemaal tegelijk die fucking sound gaan triggeren.
    //Ik trigger hier ook van en daarom heb ik dit gedaan. Als je hier een probleem mee hebt remove malbers maar dan kan ik daadwerkelijk een fatsoenlijk systeem hier voor doen ipv wat dit is.
    //Hetzelfde verhaal met de invoke, fuck malbers. En als je denkt dat dit jank is eerst was het col.transform.parent.parent.parent.name om aan de main object te komen.

    private void OnTriggerEnter(Collider col)
    {
        if (cooldownTimer == 0)
        {
            if (col.transform.root.name == foxObject.name)
            {
                foxWaterEmitter.SetActive(true);
                cooldownTimer = 10;
                Invoke("StopSound", 1);
            }
            if (col.transform.root.name == wolfObject.name)
            {
                wolfWaterEmitter.SetActive(true);
                cooldownTimer = 10;
                Invoke("StopSound", 1);
            }
        }
    }

    private void FixedUpdate()
    {
        if(cooldownTimer > 0)
        {
            cooldownTimer--;
        }
    }

    private void StopSound()
    {
        foxWaterEmitter.SetActive(false);
        wolfWaterEmitter.SetActive(false);
    }
}
