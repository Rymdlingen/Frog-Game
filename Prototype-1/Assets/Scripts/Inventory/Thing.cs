using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing : MonoBehaviour
{
    [SerializeField] private ThingScriptableObject thingSO;
    public string thingName;

    // Start is called before the first frame update
    void Start()
    {
        thingName = thingSO.Name;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
