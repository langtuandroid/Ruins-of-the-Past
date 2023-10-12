using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressureButton : MonoBehaviour
{
    [SerializeField] private List<Collider> triggeredBy = new List<Collider>();
    public UnityEvent OnPressureActivate;
    
    
    private void OnTriggerEnter(Collider other)
    {
        foreach (Collider c in triggeredBy)
        {
            if (other.name == c.name)
            {
                OnPressureActivate.Invoke();
            }
        }
    }
}
