using Codice.Client.Common.GameUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteableObject : MonoBehaviour
{
    [SerializeField] GameObject[] ObjectStates;
    private int currentStateIndex = 0;
    private int maxStateIndex;
    [SerializeField] int damageThreshold;

    private int currentDamage = 0;

    private Collider Collider;

    private void OnValidate()
    {
        int childcount = this.transform.childCount;
        ObjectStates = new GameObject[childcount];

        for(int i = 0; i < childcount; i++)
        {
            ObjectStates[i] = transform.GetChild(i).gameObject;
        }
    }

    private void Start()
    {
        maxStateIndex = ObjectStates.Length - 1;
        UpdateStateObjects();
        Collider = GetComponent<Collider>();
    }

    public void ReceiveDamage(int damage)
    {
        currentDamage += damage;

        int nextStateIndex = currentDamage / damageThreshold;
        if (nextStateIndex > currentStateIndex && nextStateIndex <= maxStateIndex)
        {
            currentStateIndex = nextStateIndex;
            UpdateStateObjects();
        }else if(currentDamage >= maxStateIndex)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void UpdateStateObjects()
    {
        for (int i = 0; i < ObjectStates.Length; i++)
        {
            ObjectStates[i].SetActive(i == currentStateIndex);
        }
    }





}
