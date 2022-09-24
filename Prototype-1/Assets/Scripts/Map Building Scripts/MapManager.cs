using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapManager : MonoBehaviour
{
    public UnityEvent<Area> isPointingAtArea;

    // Start is called before the first frame update
    void Start()
    {
        if (isPointingAtArea == null)
        {
            isPointingAtArea = new UnityEvent<Area>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 8000))
        {
            if (hit.transform.tag == "Cloud")
            {
                Debug.Log(hit.transform.name);
                isPointingAtArea.Invoke(hit.transform.gameObject.GetComponentInParent<Area>());

                if (Input.GetMouseButtonDown(0))
                {
                    hit.transform.gameObject.SetActive(false);
                }
            }
        }
    }
}
