using MalbersAnimations;
using UnityEngine;

namespace Systems
{
    public class PauseManager : MonoBehaviour
    {
        [SerializeField] private GameObject fox;
        [SerializeField] private GameObject wolf;

        [SerializeField] private GameObject pauseMenu;

        [SerializeField] private KeyCode pauseButton;

        private bool _isPaused;

        void Update()
        {
            if (!Input.GetKeyDown(pauseButton))
                return;

            if (_isPaused)
                ResumeGame();
            else
                PauseGame();

            _isPaused = !_isPaused;
        }

        [ContextMenu("Pause")]
        public void PauseGame()
        {
            fox.GetComponent<MInput>().enabled = false;
            fox.GetComponent<Animator>().enabled = false;
            fox.GetComponent<Rigidbody>().isKinematic = true;

            wolf.GetComponent<MInput>().enabled = false;
            wolf.GetComponent<Animator>().enabled = false;
            wolf.GetComponent<Rigidbody>().isKinematic = true;

            pauseMenu.SetActive(true);
        }

        [ContextMenu("Resume")]
        public void ResumeGame()
        {
            fox.GetComponent<MInput>().enabled = true;
            fox.GetComponent<Animator>().enabled = true;
            fox.GetComponent<Rigidbody>().isKinematic = false;

            wolf.GetComponent<MInput>().enabled = true;
            wolf.GetComponent<Animator>().enabled = true;
            wolf.GetComponent<Rigidbody>().isKinematic = false;

            pauseMenu.SetActive(false);
        }
    }
}