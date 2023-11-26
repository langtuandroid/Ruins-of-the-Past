using System;
using UnityEngine;

public class QuickRestart : MonoBehaviour
{
   [SerializeField] KeyCode RestartKey = KeyCode.BackQuote;

   void Update()
   {
      if (Input.GetKeyDown(RestartKey))
      {
         restartGame();
      }
   }

   private void restartGame()
   {
      UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name); 
   }
}
