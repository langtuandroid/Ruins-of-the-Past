using MalbersAnimations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteObject : MonoBehaviour
{
    public int Damage { get; private set; }
    public bool canDealDamage = false;
    [SerializeField] int BiteDamage;

    private void Start()
    {
        Damage = BiteDamage;
    }

    public bool CanDealDamage()
    {
        return canDealDamage;
    }

    public void DamageObject(int value)
    {
        canDealDamage = true;
    }

    public void DisableDamage()
    {
        canDealDamage = false;
    }
}
