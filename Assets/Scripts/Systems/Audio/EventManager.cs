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
    [field: Header("Walk events")]
    [field: SerializeField] public EventReference foxWalk { get; private set; }
    [field: SerializeField] public EventReference wolfWalk { get; private set; }

    [field: Header("Ambience events")]
    [field: SerializeField] public EventReference birdEvent { get; private set; }
}