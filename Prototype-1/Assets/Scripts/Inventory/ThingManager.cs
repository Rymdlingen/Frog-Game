using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThingManager : MonoBehaviour
{
    public UnityEvent<ThingScriptableObject> tryToPickThingUpEvent;

    // Start is called before the first frame update
    void Start()
    {
        if (tryToPickThingUpEvent == null)
        {
            tryToPickThingUpEvent = new UnityEvent<ThingScriptableObject>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Thing"))
        {
            tryToPickThingUpEvent.Invoke(collision.gameObject.GetComponent<ThingScriptableObject>());
        }
    }
}
