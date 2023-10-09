using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    [SerializeField] private List<GameObject> triggeredBy;

    private void OnCollisionEnter(Collision other)
    {
        foreach(GameObject go in triggeredBy)
            if (other.gameObject == go)
            {
                TriggerBreak();
            }
    }

    private void TriggerBreak()
    {
        Debug.Log("Break");
    }
}
