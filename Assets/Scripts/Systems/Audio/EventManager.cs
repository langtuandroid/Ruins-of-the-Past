using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class EventManager : MonoBehaviour
{
    //This event manager is for sound that are not related to 3D space, this manager is for easy access to these events when needed without needing a dedicated event emitter.

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
    [field: SerializeField] public EventReference non3dEvent { get; private set; }

}