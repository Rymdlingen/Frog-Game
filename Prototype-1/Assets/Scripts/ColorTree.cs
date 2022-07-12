using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTree : MonoBehaviour
{

    UI UIScript;

    public GameObject tree;
    public GameObject trunk;
    public GameObject leaf1;
    public GameObject leaf2;
    public GameObject leaf3;
    public GameObject leaf4;
    public GameObject leaf5;

    public Material trunkColor;
    public Material leafColor;

    int collected = 0;

    // Start is called before the first frame update
    void Start()
    {
        UIScript = GameObject.Find("Canvas").GetComponent<UI>();

    }

    // Update is called once per frame
    void Update()
    {
        if (collected != UIScript.collectedTrash)
        {
            collected = UIScript.collectedTrash;

            if (UIScript.collectedTrash == 1)
            {
                tree.SetActive(true);
            }

            if (UIScript.collectedTrash == 2)
            {
                trunk.GetComponent<MeshRenderer>().material = trunkColor;
            }

            if (UIScript.collectedTrash == 3)
            {
                leaf1.GetComponent<MeshRenderer>().material = leafColor;
            }

            if (UIScript.collectedTrash == 4)
            {
                leaf2.GetComponent<MeshRenderer>().material = leafColor;
            }

            if (UIScript.collectedTrash == 5)
            {
                leaf3.GetComponent<MeshRenderer>().material = leafColor;
            }

            if (UIScript.collectedTrash == 6)
            {
                leaf4.GetComponent<MeshRenderer>().material = leafColor;
            }

            if (UIScript.collectedTrash == 7)
            {
                leaf5.GetComponent<MeshRenderer>().material = leafColor;
            }
        }
    }
}
