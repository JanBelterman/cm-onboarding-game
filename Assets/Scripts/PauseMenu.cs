using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    private PlayerInputActions _inputActions;

    private void Awake() {
        _inputActions = new PlayerInputActions();
        _inputActions.PlayerControls.Pause.performed += TogglePause;
    }

    public void TogglePause(InputAction.CallbackContext ctx) {
        if (GameIsPaused)
        {
            Resume();
        } else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("MainMenu");
    }
    
    private void OnEnable() {
        _inputActions.Enable();
    }

    private void OnDisable() {
        _inputActions.Disable();
    }
}