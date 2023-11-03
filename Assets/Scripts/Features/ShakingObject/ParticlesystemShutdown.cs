using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesystemShutdown : MonoBehaviour
{
    private ParticleSystem PS;
    private void Start()
    {
        PS = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if(PS.particleCount == 0 && GetComponentInChildren<BiteableObject>() == null)
        {
            Destroy(gameObject);
        }
    }
}

