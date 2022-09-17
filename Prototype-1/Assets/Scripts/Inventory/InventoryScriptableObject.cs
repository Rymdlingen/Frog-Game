using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryScriptableObject", menuName = "ScriptableObjects/Inventory")]
public class InventoryScriptableObject : ScriptableObject
{
    Dictionary<ThingScriptableObject, int> thingsInInventory = new Dictionary<ThingScriptableObject, int>();

    public void AddToInventory(ThingScriptableObject thing, int amount = 1)
    {
        if (thingsInInventory.ContainsKey(thing))
        {
            thingsInInventory[thing] += amount;
        }
        else
        {
            thingsInInventory.Add(thing, amount);
        }
    }

    public void RemoveFromInventory(ThingScriptableObject thing, int amount = 1)
    {
        if (thingsInInventory[thing] >= amount)
        {
            thingsInInventory[thing] -= amount;
        }
    }
}
