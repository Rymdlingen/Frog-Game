using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<string, int> thingsInInventory { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        thingsInInventory = new Dictionary<string, int>();
    }

    // Update is called once per frame
    void Update()
    {

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
            Debug.Log("picked up: " + collision.gameObject.GetComponent<Thing>().thingName);
            Destroy(collision.gameObject);
        }
    }
}
