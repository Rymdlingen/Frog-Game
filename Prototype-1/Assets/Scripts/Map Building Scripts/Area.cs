using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Area : MonoBehaviour
{
    [SerializeField]
    private AreaScriptableObject areaInfo;

    public AreaScriptableObject AreaInfo { get => areaInfo; private set => areaInfo = value; }

    [field: SerializeField]
    public AreaState areaState { get; private set; }

    MapManager mapManager;

    public UnityEvent<Area> thisAreaUpdated;

    // Start is called before the first frame update
    void Start()
    {
        if (thisAreaUpdated == null)
        {
            thisAreaUpdated = new UnityEvent<Area>();
        }

        areaState = areaInfo.StartingSate;
        mapManager = FindObjectOfType<MapManager>();
        mapManager.updateAreaEvent.AddListener(UpdateArea);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateArea(AreaScriptableObject updatedAreasSO)
    {
        if (areaInfo == updatedAreasSO)
        {
            // Send info "I updated to this state". Send area name and new state. 
            // Update this area. TODO

            if (areaState != AreaState.Thriving)
            {
                areaState++;
                thisAreaUpdated.Invoke(this);
            }
        }
    }
}
