using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BreakablePlatform : MonoBehaviour
{
    [SerializeField] private bool showParticles = true;
    [SerializeField] private List<Collider> triggeredBy;
    [SerializeField] private GameObject particlePrefab;
    
    private GameObject platform;
    
    public UnityEvent onBreak = new();

    private void Start()
    {
        platform = transform.parent.gameObject;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        foreach (Collider c in triggeredBy)
        {
            if (other.name == c.name)
            {
                TriggerBreak();
            }
        }
    }

    private void TriggerBreak()
    {
        Vector3 pos = transform.position;
        if (showParticles)
        {
            Instantiate(particlePrefab, pos, Quaternion.identity);
        }
        
        onBreak.Invoke();
        Destroy(platform);
    }
}
