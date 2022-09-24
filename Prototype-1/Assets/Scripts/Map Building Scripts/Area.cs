using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField]
    private AreaScriptableObject areaInfo;

    public AreaScriptableObject AreaInfo { get => areaInfo; private set => areaInfo = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
