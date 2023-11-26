using System.Collections;
using System.Collections.Generic;
using MalbersAnimations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public sealed class Menu : MonoBehaviour
    {
        [SerializeField] private List<GameObject> freezeTargets;

        [SerializeField] private Canvas mainMenu;

        [SerializeField] private Canvas pauseMenu;
        
        public TextMeshProUGUI floatingText;

        private bool _frozen;

        private void Start()
        {
            mainMenu.enabled = true;
            pauseMenu.enabled = false;

            if (!_frozen)
                ToggleFreeze(false);
        }

        private void ToggleFreeze(bool togglePause)
        {
            foreach (var o in freezeTargets)
            {
                // TODO: Not every game object is guaranteed to have all of these components, possibly could cause errors in the future
                var input = o.GetComponent<MInput>();
                var animator = o.GetComponent<Animator>();
                var rigidBody = o.GetComponent<Rigidbody>();

                input.enabled = _frozen;
                if (!togglePause) continue;
                animator.enabled = _frozen;
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
                ToggleFreeze(false);
            
            StartCoroutine(FadeInFloatingText());
        }

        IEnumerator FadeInFloatingText()
        {
            const float duration = 3.0f; 
            const float startAlpha = 0f;
            const float endAlpha = 1f;
            var elapsedTime = 0f;
            
            yield return new WaitForSeconds(2.0f);
            floatingText.gameObject.SetActive(true);
            var startColor = floatingText.color;
            startColor.a = startAlpha;
            floatingText.color = startColor;

            while (elapsedTime < duration)
            {
                var alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
                
                var newColor = floatingText.color;
                newColor.a = alpha;
                floatingText.color = newColor;

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            var finalColor = floatingText.color;
            finalColor.a = endAlpha;
            floatingText.color = finalColor;
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