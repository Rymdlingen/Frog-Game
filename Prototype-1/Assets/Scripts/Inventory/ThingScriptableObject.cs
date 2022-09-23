using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ThingScriptableObject", menuName = "ScriptableObjects/Thing")]
public class ThingScriptableObject : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private GameObject model;

    public string Name
    {
        get { return name; }

        private set { name = value; }
    }

}
