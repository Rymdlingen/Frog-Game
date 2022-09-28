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
    [SerializeField] Button unlockButton;
    [SerializeField] Inventory inventory;
    [SerializeField] MapManager mapManagerScript;
    [SerializeField] Camera mapCamera;

    // Start is called before the first frame update
    void Start()
    {
        mapManagerScript.isPointingAtAreaEvent.AddListener(DisplayInfoForThisArea);
        mapManagerScript.isNotPointingAtAreaEvent.AddListener(SetDisplayVisibility);

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

    private void SetDisplayVisibility(bool isVisible)
    {
        requirementsDisplay.SetActive(isVisible);
    }

    private void DisplayInfoForThisArea(Area areaScript)
    {
        // Set display to visible.
        SetDisplayVisibility(true);

        AreaScriptableObject areaInfo = areaScript.AreaInfo;
        int nrOfThingsNeeded = areaInfo.nrOfThingsRequired.Length;
        int nrOfRequirementsMet = 0;

        for (int slot = 0; slot < slots.Count; slot++)
        {
            // Putting a thing in a slot.
            if (slot < nrOfThingsNeeded)
            {
                // The thing that goes in this slot.
                ThingScriptableObject thing = areaInfo.thingsRequiredForUnlocking[slot];

                // Text.
                int have = CheckIfThingIsInInventory(thing.Name);
                int need = areaInfo.nrOfThingsRequired[slot];
                slotsAndText[slot][0].GetComponent<TextMeshProUGUI>().SetText(have.ToString() + " / " + need.ToString());

                // Check if requirement is met.
                if (have >= need)
                {
                    nrOfRequirementsMet++;
                }

                // Icon.
                slotsAndText[slot][1].GetComponent<Image>().sprite = thing.Icon;

                // Show slot.
                slots[slot].SetActive(true);
            }
            else
            {
                // Hide slot.
                slots[slot].SetActive(false);
            }
        }

        // Set position of background.
        Vector3 backgroundPosition = mapCamera.WorldToScreenPoint(areaScript.transform.position);
        backgroundPosition = new Vector3(backgroundPosition.x, backgroundPosition.y, 0);
        backgroundTransform.SetPositionAndRotation(backgroundPosition, backgroundTransform.rotation);

        // Set name text.
        nameText.SetText(areaInfo.Name);

        // Set status for button.
        if (nrOfRequirementsMet == nrOfThingsNeeded)
        {
            unlockButton.interactable = true;
        }
        else
        {
            unlockButton.interactable = false;
        }
    }

    private int CheckIfThingIsInInventory(string thingName)
    {
        int amount;

        if (inventory.thingsInInventory.ContainsKey(thingName))
        {
            amount = inventory.thingsInInventory[thingName];
        }
        else
        {
            amount = 0;
        }

        return amount;
    }
}
