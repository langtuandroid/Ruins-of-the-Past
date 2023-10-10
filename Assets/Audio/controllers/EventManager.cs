using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class EventManager : MonoBehaviour
{
    public static EventManager instance { get; private set; }   //Create singleton instance

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one event manager is active in the scene.");
        }
        instance = this;
    }

    //Below add a reference of each event
    [field: Header("Test SFX")]
    [field: SerializeField] public EventReference testSFX { get; private set; }
}