using System.Collections.Generic;
using MalbersAnimations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public sealed class Menu : MonoBehaviour
    {
        [SerializeField] private List<GameObject> freezeTargets;

        [SerializeField] private Canvas mainMenu;

        [SerializeField] private Canvas pauseMenu;

        private bool _frozen;

        private void Start()
        {
            mainMenu.enabled = true;
            pauseMenu.enabled = false;

            if (!_frozen)
                ToggleFreeze(false);
        }

        private void ToggleFreeze(bool toggleAnimator)
        {
            foreach (var o in freezeTargets)
            {
                // TODO: Not every game object is guaranteed to have all of these components, possibly could cause errors in the future
                var input = o.GetComponent<MInput>();
                var animator = o.GetComponent<Animator>();
                var rigidBody = o.GetComponent<Rigidbody>();

                input.enabled = _frozen;
                animator.enabled = toggleAnimator && _frozen;
                rigidBody.isKinematic = !_frozen;
            }

            _frozen = !_frozen;
        }

        public void TogglePause()
        {
            if (mainMenu.enabled)
                return;

            ToggleFreeze(true);

            pauseMenu.enabled = _frozen;
        }

        public void OnNewGame()
        {
            mainMenu.enabled = false;

            if (_frozen)
                ToggleFreeze(true);
        }

        public void OnQuit()
        {
            Application.Quit();
        }

        public void OnMainMenu()
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}