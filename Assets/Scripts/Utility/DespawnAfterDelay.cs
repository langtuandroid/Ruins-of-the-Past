using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnAfterDelay : MonoBehaviour
{
    [SerializeField][Range(100f,10000f)] private float DespawnDelayMilliSec = 5000f;
    void Start()
    {
        Invoke("DestroySelf", DespawnDelayMilliSec);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
