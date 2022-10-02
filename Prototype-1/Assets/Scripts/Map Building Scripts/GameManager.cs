using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameModeScriptableObject gameModeManager;

    GameModes currentGameMode;

    [SerializeField] private GameObject pauseScreen;

    public UnityEvent<bool> pauseGameEvent;

    bool gameIsPaused = false;

    [SerializeField] private EndGameManager endGameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameModeManager.changeModeEvent.AddListener(SaveCurrentGameMode);

        if (pauseGameEvent == null)
        {
            pauseGameEvent = new UnityEvent<bool>();
        }

        endGameManager.allGoalsMetEvent.AddListener(EndGame);
    }

    // Update is called once per frame
    void Update()
    {
        // Change mode;
        if (Input.GetKeyDown(KeyCode.Tab) && !gameIsPaused)
        {
            gameModeManager.ChangeGameMode();

            // Set cursor visibility.
            if (currentGameMode == GameModes.Explore)
            {
                CursorHiddenAndLocked();
            }
            else if (currentGameMode == GameModes.Map)
            {
                CursorVisibleAndConfined();
            }
        }

        // Pause the game.
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                UnpauseGame();
            }
            else if (!gameIsPaused)
            {
                PauseGame();
            }
        }
    }

    private void EndGame()
    {
        PauseGame();
    }

    public void PauseGame()
    {
        gameIsPaused = true;

        CursorVisibleAndConfined();

        SetPauseScreenVisibility(gameIsPaused);

        pauseGameEvent.Invoke(gameIsPaused);
    }

    public void UnpauseGame()
    {
        gameIsPaused = false;

        if (currentGameMode == GameModes.Explore)
        {
            CursorHiddenAndLocked();
        }

        SetPauseScreenVisibility(gameIsPaused);
        pauseGameEvent.Invoke(gameIsPaused);
    }

    public void SetPauseScreenVisibility(bool showPauseScreen)
    {
        pauseScreen.SetActive(showPauseScreen);
    }

    private void CursorVisibleAndConfined()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void CursorHiddenAndLocked()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void SaveCurrentGameMode(GameModes gameMode)
    {
        currentGameMode = gameMode;
    }
}
