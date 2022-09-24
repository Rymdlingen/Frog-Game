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

    private void DisplayInfoForThisArea(Area areaScript)
    {
        AreaScriptableObject areaInfo = areaScript.AreaInfo;

        // Set position of background.
        Vector3 backgroundPosition = mapCamera.WorldToScreenPoint(areaScript.transform.position);
        backgroundPosition = new Vector3(backgroundPosition.x, backgroundPosition.y, 0);
        backgroundTransform.SetPositionAndRotation(backgroundPosition, backgroundTransform.rotation);

        // Set name text.
        nameText.SetText(areaInfo.Name);

        // Set text for needed things.
        needText.SetText(GetNeededThingsAndAmount(areaInfo.thingsRequiredForUnlocking, areaInfo.nrOfThingsRequired));



        // Parse data about needed thing from inventory.
        // Set status for button.
    }

    private string GetNeededThingsAndAmount(List<ThingScriptableObject> things, int[] nrOfThings)
    {
        string text = "Needed: \n";

        for (int i = 0; i < things.Count; i++)
        {
            text += nrOfThings[i] + " " + things[i].Name + "\n";
        }

        return text;
    }

    private void GetNrOfThingsNeeded()
    {

    }
}
