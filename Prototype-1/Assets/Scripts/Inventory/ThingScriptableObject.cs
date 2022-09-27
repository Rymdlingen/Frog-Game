using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ThingScriptableObject", menuName = "ScriptableObjects/Thing")]
public class ThingScriptableObject : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private GameObject model;
    [SerializeField] private Sprite icon;

    public string Name
    {
        get { return _name; }

        private set { _name = value; }
    }

}
