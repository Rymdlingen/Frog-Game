using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField]
    private AreaScriptableObject areaInfo;

    public AreaScriptableObject AreaInfo { get => areaInfo; private set => areaInfo = value; }

    [field: SerializeField]
    public AreaState areaState { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        areaState = areaInfo.StartingSate;

        // add a listener to an event in the map manager. that event -> updateAreaEvent TODO
    }

    // Update is called once per frame
    void Update()
    {

    }
}
