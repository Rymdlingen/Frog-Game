using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ThingScriptableObject", menuName = "ScriptableObjects/Thing")]
public class ThingScriptableObject : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private bool canBePickedUp;

    public string Name
    {
        get { return name; }

        private set { name = value; }
    }

    public bool CanBePickedUp
    {
        get { return canBePickedUp; }

        private set { canBePickedUp = value; }
    }
}
