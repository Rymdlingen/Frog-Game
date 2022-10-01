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

    [Header("Unlock")]
    public List<ThingScriptableObject> thingsRequiredForUnlock;
    public int[] nrOfThingsRequiredUnlock;
    private List<int> savedValuesUnlock = new List<int>();

    [Header("Clean")]
    public List<ThingScriptableObject> thingsRequiredForClean;
    public int[] nrOfThingsRequiredClean;
    private List<int> savedValuesClean = new List<int>();

    [Header("Thriving")]
    public List<ThingScriptableObject> thingsRequiredForThrive;
    public int[] nrOfThingsRequiredThrive;
    private List<int> savedValuesThrive = new List<int>();


    private void OnValidate()
    {
        UpdateRequirements(thingsRequiredForUnlock, nrOfThingsRequiredUnlock, savedValuesUnlock);
        UpdateRequirements(thingsRequiredForClean, nrOfThingsRequiredClean, savedValuesClean);
        UpdateRequirements(thingsRequiredForThrive, nrOfThingsRequiredThrive, savedValuesThrive);
    }

    private void UpdateRequirements(List<ThingScriptableObject> requiredThings, int[] nrOfRequiredThings, List<int> savedValues)
    {
        while (requiredThings.Count > 6)
        {
            requiredThings.RemoveAt(requiredThings.Count - 1);
        }

        if (nrOfRequiredThings.Length != requiredThings.Count)
        {
            while (savedValues.Count < requiredThings.Count)
            {
                savedValues.Add(0);
            }

            for (int i = 0; i < nrOfRequiredThings.Length; i++)
            {
                savedValues[i] = nrOfRequiredThings[i];
            }

            nrOfRequiredThings = new int[requiredThings.Count];

            for (int i = 0; i < nrOfRequiredThings.Length; i++)
            {
                nrOfRequiredThings[i] = savedValues[i];
            }
        }
    }
}

