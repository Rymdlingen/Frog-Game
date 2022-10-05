using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] float startTimer;
    float counter;
    bool pauseCounter;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject tutorialUI;

    // Start is called before the first frame update
    void Start()
    {
        counter = startTimer;
        gameManager.pauseGameEvent.AddListener(ShowInstructions);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.T))
        {
            if (counter > 0)
            {
                counter = 0;
            }
            else
            {
                counter = startTimer;
            }
        }

        if (pauseCounter)
        {
            if (!tutorialUI.activeSelf) tutorialUI.SetActive(true);
        }
        else if (counter > 0)
        {
            if (!tutorialUI.activeSelf) tutorialUI.SetActive(true);
            counter -= Time.deltaTime;
        }
        else
        {
            // Hide tutorial UI
            if (tutorialUI.activeSelf) tutorialUI.SetActive(false);
        }

        // Listen to if pause is hit. Show if pause and never show in game again TODO
    }

    private void ShowInstructions(bool gameIsPaused)
    {
        pauseCounter = gameIsPaused;
    }
}
