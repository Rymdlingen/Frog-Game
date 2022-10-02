using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<string, int> thingsInInventory { get; private set; }

    [SerializeField] private MapManager mapManager;

    // Start is called before the first frame update
    void Start()
    {
        thingsInInventory = new Dictionary<string, int>();
        mapManager.updateInventoryEvent.AddListener(UpdateInventory);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateInventory(List<ThingScriptableObject> things, int[] nrOfThings)
    {
        for (int i = 0; i < things.Count; i++)
        {
            RemoveFromInventory(things[i].Name, nrOfThings[i]);
        }
    }

    public void AddToInventory(string thing, int amount = 1)
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

    public void RemoveFromInventory(string thing, int amount = 1)
    {
        if (thingsInInventory[thing] >= amount)
        {
            thingsInInventory[thing] -= amount;
        }
    }

    public int CheckAmountOfThingInInventory(string thing)
    {
        int amount = thingsInInventory[thing];

        return amount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Thing"))
        {
            AddToInventory(collision.gameObject.GetComponent<Thing>().thingName);
            // Debug.Log("picked up: " + collision.gameObject.GetComponent<Thing>().thingName);
            Destroy(collision.gameObject);
        }
    }
}
