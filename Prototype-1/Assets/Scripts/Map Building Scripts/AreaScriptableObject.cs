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
    [field: SerializeField] public string Name { get; private set; }
    [SerializeField] private string Description;
    [field: SerializeField] public AreaState StartingSate { get; private set; }
    public List<ThingScriptableObject> thingsRequiredForUnlocking;
    public int[] nrOfThingsRequired;

    private List<int> savedValues = new List<int>();

    private void OnValidate()
    {
        while (thingsRequiredForUnlocking.Count > 6)
        {
            thingsRequiredForUnlocking.RemoveAt(thingsRequiredForUnlocking.Count - 1);
        }

        if (nrOfThingsRequired.Length != thingsRequiredForUnlocking.Count)
        {
            while (savedValues.Count < thingsRequiredForUnlocking.Count)
            {
                savedValues.Add(0);
            }

            for (int i = 0; i < nrOfThingsRequired.Length; i++)
            {
                savedValues[i] = nrOfThingsRequired[i];
            }

            nrOfThingsRequired = new int[thingsRequiredForUnlocking.Count];

            for (int i = 0; i < nrOfThingsRequired.Length; i++)
            {
                nrOfThingsRequired[i] = savedValues[i];
            }
        }
    }
}

