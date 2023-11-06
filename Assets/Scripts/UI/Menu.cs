using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private Canvas mainMenuPrefab;

    [SerializeField] private Canvas pauseMenuPrefab;

    private Canvas _mainMenu;

    private Canvas _pauseMenu;
    
    private void Start()
    {
        _mainMenu = Instantiate(mainMenuPrefab);
    }

    public void TogglePause()
    {
        if (_pauseMenu == null)
            return;

        _pauseMenu.enabled = true;
    }

    public void OnNewGame()
    {
        _mainMenu.enabled = false;

        if (_pauseMenu == null)
            _pauseMenu = Instantiate(pauseMenuPrefab);
    }
}