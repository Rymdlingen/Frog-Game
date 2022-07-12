using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public GameObject trashCounterObject;
    private TextMeshProUGUI trashCounter;
    public int collectedTrash = 0;
    private int totalTrash;

    // Start is called before the first frame update
    void Start()
    {
        totalTrash = GameObject.FindGameObjectsWithTag("Trash").Length;
        trashCounter = trashCounterObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        trashCounter.text = collectedTrash + "/" + totalTrash;
    }
}
