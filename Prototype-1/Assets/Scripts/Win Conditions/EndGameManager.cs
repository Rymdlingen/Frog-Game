using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] bool areaThriving = false;
    [SerializeField] bool areaClean = false;
    [SerializeField] bool areaDirty = false;

    [SerializeField] private Area thisAreaShouldBeThriving;
    [SerializeField] private Area thisAreaShouldBeClean;
    [SerializeField] private Area thisAreaShouldBeDirty;

    public UnityEvent allGoalsMetEvent;

    [SerializeField] private bool endTriggeredOnce = false;

    [SerializeField] private GameObject endText;

    // Start is called before the first frame update
    void Start()
    {
        if (allGoalsMetEvent == null)
        {
            allGoalsMetEvent = new UnityEvent();
        }

        thisAreaShouldBeThriving.thisAreaUpdated.AddListener(CheckIfGoalIsMet);
        thisAreaShouldBeClean.thisAreaUpdated.AddListener(CheckIfGoalIsMet);
        thisAreaShouldBeDirty.thisAreaUpdated.AddListener(CheckIfGoalIsMet);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void TurnOnEndText()
    {
        endText.SetActive(true);
    }

    private void CheckIfGoalIsMet(Area area)
    {
        if (endTriggeredOnce)
        {
            return;
        }

        if (area == thisAreaShouldBeThriving)
        {
            if (area.areaState == AreaState.Thriving)
            {
                areaThriving = true;
            }
        }
        else if (area == thisAreaShouldBeClean)
        {
            if (area.areaState == AreaState.Clean)
            {
                areaClean = true;
            }
        }
        else if (area == thisAreaShouldBeDirty)
        {
            if (area.areaState == AreaState.Dirty)
            {
                areaDirty = true;
            }
        }

        if (areaThriving && areaClean && areaDirty)
        {
            endTriggeredOnce = true;
            TurnOnEndText();
            allGoalsMetEvent.Invoke();
        }
    }
}
