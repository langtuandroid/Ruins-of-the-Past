using System;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CamZone : MonoBehaviour
{
   [SerializeField]
   private CinemachineVirtualCameraBase virtualCamera = null;

   private void Start()
   {
      virtualCamera.enabled = false;
   }

   private void OnTriggerEnter(Collider other)
   {
     if (other.CompareTag("Fox") || other.CompareTag("Wolf"))
        virtualCamera.enabled = true;
   }
   
   private void OnTriggerExit(Collider other)
   {
      if (other.CompareTag("Fox") || other.CompareTag("Wolf"))
         virtualCamera.enabled = false;
   }

   private void OnValidate()
   {
     GetComponent<Collider>().isTrigger = true;
   }
}
