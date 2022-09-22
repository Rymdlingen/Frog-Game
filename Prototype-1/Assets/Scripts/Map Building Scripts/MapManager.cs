using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

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


                if (Input.GetMouseButtonDown(0))
                {
                    hit.transform.gameObject.SetActive(false);
                }
            }
        }
    }
}
