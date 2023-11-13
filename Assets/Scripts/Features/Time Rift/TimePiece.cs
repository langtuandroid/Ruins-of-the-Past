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

        private List<Collider> _colliders = new();

        private List<Rigidbody> _rigidbodies = new();

        private List<MAnimal> _controllers = new();

        private void Start()
        {
            foreach (var player in players)
            {
                _colliders.AddRange(player.GetComponentsInChildren<Collider>());
                _rigidbodies.AddRange(player.GetComponentsInChildren<Rigidbody>());
                _controllers.AddRange(player.GetComponentsInChildren<MAnimal>());
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

            foreach (var capsuleCollider in _colliders)
            {
                capsuleCollider.includeLayers = oldMask;
                capsuleCollider.excludeLayers = newMask;
            }

            foreach (var rigidBody in _rigidbodies)
            {
                rigidBody.includeLayers = oldMask;
                rigidBody.excludeLayers = newMask;
            }

            foreach (var controller in _controllers)
            {
                controller.groundLayer = new LayerReference(LayerMask.GetMask(_isInPast ? "Past" : "Default"));
            }
        }
    }
}