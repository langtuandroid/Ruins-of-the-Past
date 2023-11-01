using System;
using System.Collections;
using System.Collections.Generic;
using MalbersAnimations;
using UnityEditor.Search;
using UnityEngine;

public class PauzeManager : MonoBehaviour
{
    [SerializeField] private GameObject fox;
    [SerializeField] private GameObject wolf;

    [SerializeField] private GameObject PauzeMenu;

    [SerializeField] private KeyCode pauzeButton;

    private bool isPauzed;

    void Update()
    {
        if (!Input.GetKeyDown(pauzeButton)) return;

        if (isPauzed)
            UnpauzeGame();
        else
            PauzeGame();
    }
    
    [ContextMenu("Pauze")]
    public void PauzeGame()
    {
        fox.GetComponent<MInput>().enabled = false;
        fox.GetComponent<Animator>().enabled = false;
        fox.GetComponent<Rigidbody>().isKinematic = true;

        wolf.GetComponent<MInput>().enabled = false;
        wolf.GetComponent<Animator>().enabled = false;
        wolf.GetComponent<Rigidbody>().isKinematic = true;
        
        PauzeMenu.SetActive(true);
        isPauzed = true;
    }

    [ContextMenu("Unpauze")]
    public void UnpauzeGame()
    {
        fox.GetComponent<MInput>().enabled = true;
        fox.GetComponent<Animator>().enabled = true;
        fox.GetComponent<Rigidbody>().isKinematic = false;

        wolf.GetComponent<MInput>().enabled = true;
        wolf.GetComponent<Animator>().enabled = true;
        wolf.GetComponent<Rigidbody>().isKinematic = false;
        
        PauzeMenu.SetActive(false);
        isPauzed = false;
    }
}
