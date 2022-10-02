using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private GameModeScriptableObject gameModeManager;
    [SerializeField]
    private GameManager gameManager;

    [SerializeField] CinemachineVirtualCamera exploreCamera;
    [SerializeField] CinemachineVirtualCamera mapCamera;
    [SerializeField] CinemachineVirtualCamera pauseCamera;

    // Start is called before the first frame update
    void Start()
    {
        gameModeManager.changeModeEvent.AddListener(SetActiveCamera);
        gameManager.pauseGameEvent.AddListener(UsePauseCamera);
    }

    private void UsePauseCamera(bool isPaused)
    {
        if (isPaused)
        {
            // Set Pause camera as active bye setting priority
            pauseCamera.Priority = 10;
        }
        else
        {
            pauseCamera.Priority = 0;
        }
    }

    private void SetActiveCamera(GameModes currentGameMode)
    {
        if (currentGameMode == GameModes.Explore)
        {
            exploreCamera.Priority = mapCamera.Priority + 1;
            mapCamera.Priority = 0;
        }
        else if (currentGameMode == GameModes.Map)
        {
            mapCamera.Priority = exploreCamera.Priority + 1;
            exploreCamera.Priority = 0;
        }
    }
}
