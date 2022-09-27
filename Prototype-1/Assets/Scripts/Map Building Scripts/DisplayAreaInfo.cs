using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayAreaInfo : MonoBehaviour
{
    [SerializeField] Transform backgroundTransform;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject requirementsDisplay;
    [SerializeField] List<GameObject> slots;
    [SerializeField] GameObject[][] slotsAndText;
    // needed for each slot.
    private TextMeshProUGUI nrHaveAndNeed;
    private Sprite icon;
    [SerializeField] Button unlockButton;

    [SerializeField] Inventory inventory;

    [SerializeField] MapManager mapManagerScript;

    [SerializeField] Camera mapCamera;

    // Start is called before the first frame update
    void Start()
    {
        mapManagerScript.isPointingAtArea.AddListener(DisplayInfoForThisArea);

        slotsAndText = new GameObject[slots.Count][];
        for (int i = 0; i < slots.Count; i++)
        {
            Transform slotTransform = slots[i].transform;
            slotsAndText[i] = new GameObject[] { slotTransform.Find("Amount").gameObject, slotTransform.Find("Icon").gameObject };
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void DisplayInfoForThisArea(Area areaScript)
    {
        AreaScriptableObject areaInfo = areaScript.AreaInfo;
        int nrOfThingsNeeded = areaInfo.nrOfThingsRequired.Length;

        // TODO
        for (int slot = 0; slot < nrOfThingsNeeded; slot++)
        {
            slotsAndText[slot][0].GetComponent<TextMeshProUGUI>().SetText(areaInfo.thingsRequiredForUnlocking[slot].Name);
            //   slot
        }

        // Set position of background.
        Vector3 backgroundPosition = mapCamera.WorldToScreenPoint(areaScript.transform.position);
        backgroundPosition = new Vector3(backgroundPosition.x, backgroundPosition.y, 0);
        backgroundTransform.SetPositionAndRotation(backgroundPosition, backgroundTransform.rotation);

        // Set name text.
        nameText.SetText(areaInfo.Name);

        // Set text for needed things.
        //needText.SetText(GetNeededThingsAndAmount(areaInfo.thingsRequiredForUnlocking, areaInfo.nrOfThingsRequired));



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
