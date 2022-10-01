using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatingObject : MonoBehaviour
{
    [field: SerializeField]
    public GameObject UnlockModel { get; private set; }

    [field: SerializeField]
    public GameObject DirtyModel { get; private set; }

    [field: SerializeField]
    public GameObject CleanModel { get; private set; }

    [field: SerializeField]
    public GameObject ThriveModel { get; private set; }

    private MapManager mapManager;
    private Area areaScript;
    private AreaScriptableObject areaSOWhereThingIs;
    private GameObject currentModel;



    // Start is called before the first frame update
    void Start()
    {
        areaScript = GetComponentInParent<Area>();
        areaSOWhereThingIs = areaScript.AreaInfo;
        currentModel = transform.GetChild(0).gameObject;

        areaScript.thisAreaUpdated.AddListener(UpdateThing);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateThing(Area area)
    {
        if (areaScript == area)
        {
            // Update things in this area. TODO

            if (currentModel != null)
            {
                Destroy(currentModel);
            }

            GameObject newModel = null;

            switch (areaScript.areaState)
            {
                case AreaState.Locked:
                    newModel = UnlockModel;
                    break;
                case AreaState.Dirty:
                    newModel = DirtyModel;
                    break;
                case AreaState.Clean:
                    newModel = CleanModel;
                    break;
                case AreaState.Thriving:
                    newModel = ThriveModel;
                    break;
            }

            if (newModel != null)
            {
                currentModel = Instantiate(newModel, transform.position, transform.rotation, transform);
            }
        }
    }
}