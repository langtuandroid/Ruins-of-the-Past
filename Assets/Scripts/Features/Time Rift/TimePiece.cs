using System.Collections.Generic;
using MalbersAnimations;
using MalbersAnimations.Controller;
using MalbersAnimations.Scriptables;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Features.Time_Rift
{
    public class TimePiece : MonoBehaviour
    {
        [SerializeField] private GameObject[] players;

        [SerializeField] private Camera activeCamera;

        [SerializeField] private Camera hiddenCamera;

        [SerializeField] private int hiddenSceneIndex;

        [SerializeField] private LayerMask activePhysicsLayerMask;

        [SerializeField] private LayerMask hiddenPhysicsLayerMask;

        private bool _isInPast;

        private static readonly int TimepieceTexture = Shader.PropertyToID("_TimepieceTexture");

        private List<Rigidbody> _capsuleColliders = new();

        private void Start()
        {
            foreach (var player in players)
            {
                _capsuleColliders.Add(player.GetComponent<Rigidbody>());
            }

            SceneManager.LoadScene(hiddenSceneIndex, LoadSceneMode.Additive);

            var texture = new RenderTexture(Screen.width, Screen.height, 24);
            Shader.SetGlobalTexture(TimepieceTexture, texture);
            hiddenCamera.targetTexture = texture;
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.R)) return; // TODO: Convert to new input system

            _isInPast = !_isInPast;

            activeCamera.targetTexture = hiddenCamera.targetTexture;
            hiddenCamera.targetTexture = null;

            // Swap cameras
            (activeCamera, hiddenCamera) = (hiddenCamera, activeCamera);

            var oldMask = !_isInPast ? activePhysicsLayerMask : hiddenPhysicsLayerMask;
            var newMask = _isInPast ? activePhysicsLayerMask : hiddenPhysicsLayerMask;
            foreach (var c in _capsuleColliders)
            {
                c.excludeLayers = newMask;
            }

            foreach (var player in players)
            {
                player.GetComponent<MAnimal>().groundLayer =
                    new LayerReference(LayerMask.GetMask(_isInPast ? "Past" : "Default"));

                var colliders = player.GetComponentsInChildren<Collider>();
                foreach (var capsuleCollider in colliders)
                {
                    capsuleCollider.includeLayers = oldMask;
                    capsuleCollider.excludeLayers = newMask;
                }

                var rigidbodies = player.GetComponentsInChildren<Rigidbody>();
                foreach (var rigidBody in rigidbodies)
                {
                    rigidBody.includeLayers = oldMask;
                    rigidBody.excludeLayers = newMask;
                }
            }
        }
    }
}