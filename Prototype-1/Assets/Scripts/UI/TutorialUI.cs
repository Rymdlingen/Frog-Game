using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] float startTimer;
    float counter;

    // Start is called before the first frame update
    void Start()
    {
        counter = startTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (counter > 0)
        {
            counter -= Time.deltaTime;
        }
        else
        {
            // Hide tutorial UI
        }

        // Listen to if pause is hit. Show if pause and never show in game again TODO
    }
}
