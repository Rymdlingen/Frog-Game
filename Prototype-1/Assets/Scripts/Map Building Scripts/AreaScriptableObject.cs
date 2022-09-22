using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AreaState
{
    Locked,
    Dirty,
    Clean,
    Thriving
}

[CreateAssetMenu(fileName = "AreaScriptableObject", menuName = "ScriptableObjects/Area")]
public class AreaScriptableObject : ScriptableObject
{
    [SerializeField]
    private string _name;
    public string Name
    {
        get { return _name; }
        private set {; }
    }
    [SerializeField]
    private bool isDiscovered;
    [SerializeField]
    private AreaState currentAreaState;
    [SerializeField]
    private List<ThingScriptableObject> itemsRequiredForUnlocking = new List<ThingScriptableObject>();
}

