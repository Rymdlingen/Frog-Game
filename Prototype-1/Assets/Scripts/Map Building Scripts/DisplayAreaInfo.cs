using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayAreaInfo : MonoBehaviour
{
    [SerializeField] Transform backgroundTransform;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI needText;
    [SerializeField] TextMeshProUGUI haveText;
    [SerializeField] Button unlockButton;

    [SerializeField] InventoryScriptableObject inventory;

    [SerializeField] MapManager mapManagerScript;

    [SerializeField] Camera mapCamera;

    // Start is called before the first frame update
    void Start()
    {
        mapManagerScript.isPointingAtArea.AddListener(DisplayInfoForThisArea);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void DisplayInfoForThisArea(GameObject area)
    {
        Vector3 backgroundPosition = mapCamera.WorldToScreenPoint(area.transform.position);
        backgroundPosition = new Vector3(backgroundPosition.x, backgroundPosition.y, 0);
        backgroundTransform.SetPositionAndRotation(backgroundPosition, backgroundTransform.rotation);
        nameText.SetText(area.name);
        // Parse data from things needed and nr. of things needed.
        // Parse data about needed thing from inventory.
        // Set status for button.
    }
}
