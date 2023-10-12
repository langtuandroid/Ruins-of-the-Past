using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BreakablePlatform : MonoBehaviour
{
    private enum BreakMode
    {
        Particles = 0,
        Physics = 1,
    }
    
    [Header("Platform Config")]
    [SerializeField] private bool showParticles = true;
    [SerializeField] private List<Collider> triggeredBy;
    [SerializeField][Range(0f, 10f)] private float breakDelay = 0f;
    [SerializeField] [Range(1f, 10f)] private float respawnDelay = 1f;
    [SerializeField] private BreakMode breakMode = BreakMode.Particles;
    public UnityEvent onBreak = new();
    
    
    [Header("Dont touch!")]
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private Renderer respawnRenderer;
    [SerializeField] private Collider respawnCollider;
    [SerializeField] private Collider respawnTrigger;
    
    private Rigidbody _rigidbody;

    private Vector3 _startPosition;
    private Quaternion _startRotation;
    
    

    private void Start()
    {
        Transform t = transform;
        _rigidbody = GetComponent<Rigidbody>();
        _startPosition = t.position;
        _startRotation = t.rotation;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        foreach (Collider c in triggeredBy)
        {
            if (other.name == c.name)
            {
                Debug.Log("Break triggered");
                StartCoroutine(TriggerBreak());
            }
        }
    }

    IEnumerator TriggerBreak()
    {
        yield return new WaitForSeconds(breakDelay);
        switch (breakMode)
        {
            case BreakMode.Particles:
                BreakPlatformParticles();
                DisableComponents(false);
                break;
            case BreakMode.Physics:
                BreakPlatformPhysics();
                DisableComponents(true);
                break;
            default:
                Debug.Log("No breakMode set");
                break;
        }
        
        StartCoroutine(Respawn());
        onBreak.Invoke();
    }

    private void BreakPlatformParticles()
    {
        Vector3 pos = transform.position;
        if (showParticles)
            Instantiate(particlePrefab, pos, Quaternion.identity);
    }

    private void BreakPlatformPhysics()
    {
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnDelay);
        Reset();
    }

    private void Reset()
    {
        transform.rotation = _startRotation;
        transform.position = _startPosition;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
        EnableComponents();
    }

    private void DisableComponents(bool isPhysics)
    {
        respawnTrigger.enabled = false;

        if (!isPhysics)
        {
            respawnCollider.enabled = false;
            respawnRenderer.enabled = false;
        }
    }

    private void EnableComponents()
    {
        respawnCollider.enabled = true;
        respawnTrigger.enabled = true; 
        respawnRenderer.enabled = true;
    }
}
