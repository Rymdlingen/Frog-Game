using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapManager : MonoBehaviour
{
    public UnityEvent<Area> isPointingAtAreaEvent;
    public UnityEvent<bool> isNotPointingAtAreaEvent;
    public UnityEvent<AreaScriptableObject> updateAreaEvent;

    private bool isPointingAtArea;

    [SerializeField]
    private GameModeScriptableObject gameModeManager;
    private bool inMapMode = false;

    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        if (isPointingAtAreaEvent == null)
        {
            isPointingAtAreaEvent = new UnityEvent<Area>();
        }

        if (isNotPointingAtAreaEvent == null)
        {
            isNotPointingAtAreaEvent = new UnityEvent<bool>();
        }

        if (updateAreaEvent == null)
        {
            updateAreaEvent = new UnityEvent<AreaScriptableObject>();
        }

        gameModeManager.changeModeEvent.AddListener(GetGameMode);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (inMapMode)
        {
            if (Physics.Raycast(ray, out hit, 8000))
            {
                if (hit.transform.tag == "Cloud")
                {
                    isPointingAtArea = true;
                    // Debug.Log(hit.transform.name);
                    isPointingAtAreaEvent.Invoke(hit.transform.gameObject.GetComponentInParent<Area>());

                    /*
                    if (Input.GetMouseButtonDown(0))
                    {
                        hit.transform.gameObject.SetActive(false);
                    }
                    */
                }
                else
                {
                    if (isPointingAtArea)
                    {
                        isPointingAtArea = false;
                        isNotPointingAtAreaEvent.Invoke(isPointingAtArea);
                    }
                }
            }
        }
        else
        {
            if (isPointingAtArea)
            {
                isPointingAtArea = false;
                isNotPointingAtAreaEvent.Invoke(isPointingAtArea);
            }
        }
    }

    public void UpdateArea()
    {
        hit.transform.gameObject.SetActive(false);

        // Send the area that was updated. All areas will listen but only the area that was updated will change. Am I sending the right thing? TODO
        updateAreaEvent.Invoke(hit.transform.parent.GetComponent<Area>().AreaInfo);
    }

    private void GetGameMode(GameModes gameMode)
    {
        if (gameMode == GameModes.Explore)
        {
            inMapMode = false;
        }
        else if (gameMode == GameModes.Map)
        {
            inMapMode = true;
        }
    }
}
