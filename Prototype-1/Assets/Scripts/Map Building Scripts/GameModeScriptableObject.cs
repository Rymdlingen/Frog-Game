using UnityEngine;
using UnityEngine.Events;

public enum GameModes
{
    Explore,
    Map
}

[CreateAssetMenu(fileName = "GameModeScriptableObject", menuName = "ScriptableObjects/GameMode")]
public class GameModeScriptableObject : ScriptableObject
{
    [SerializeField]
    private GameModes currentGameMode;

    public UnityEvent<GameModes> modeChangeEvent;

    private void Awake()
    {
        currentGameMode = GameModes.Explore;
    }

    private void OnEnable()
    {
        if (modeChangeEvent == null)
        {
            modeChangeEvent = new UnityEvent<GameModes>();
        }
    }

    public void ChangeGameMode()
    {
        if (currentGameMode == GameModes.Explore)
        {
            currentGameMode = GameModes.Map;
        }
        else
        {
            currentGameMode = GameModes.Explore;
        }

        modeChangeEvent.Invoke(currentGameMode);
    }
}
