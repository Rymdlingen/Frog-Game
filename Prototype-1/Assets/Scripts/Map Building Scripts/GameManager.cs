using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameModeScriptableObject gameModeManager;

    GameModes currentGameMode;

    // Start is called before the first frame update
    void Start()
    {
        gameModeManager.changeModeEvent.AddListener(SaveCurrentGameMode);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            gameModeManager.ChangeGameMode();
        }

        if (currentGameMode == GameModes.Explore)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (currentGameMode == GameModes.Map)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    private void SaveCurrentGameMode(GameModes gameMode)
    {
        currentGameMode = gameMode;
    }
}
