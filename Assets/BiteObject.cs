using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteObject : MonoBehaviour
{
    [SerializeField] private int Damage = 1;
    private bool canDealDamage = false;

    public void DamageObject(int value)
    {
        canDealDamage = true;
    }
    
    public void DisableDamage()
    {
        canDealDamage = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("I entered something");
        //If the collider is on a gameobject that has the correct layer
        if (other.gameObject.layer == 29)
        {
           if (canDealDamage)
            {
                BiteableObject biteableobject = other.GetComponent<BiteableObject>();
                if (biteableobject)
                {
                    biteableobject.ReceiveDamage(Damage);
                }
            }
        }
    }
}
