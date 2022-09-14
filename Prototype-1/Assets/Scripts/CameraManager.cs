using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private GameModeScriptableObject gameModeManager;

    [SerializeField] CinemachineVirtualCamera exploreCamera;
    [SerializeField] CinemachineVirtualCamera mapCamera;

    // Start is called before the first frame update
    void Start()
    {
        gameModeManager.modeChangeEvent.AddListener(SetActiveCamera);
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
