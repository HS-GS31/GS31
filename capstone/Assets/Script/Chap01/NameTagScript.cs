using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameTagScript : MonoBehaviour
{
    private string sortlayerName;

    // Start is called before the first frame update
    void Start()
    {
        sortlayerName = "NameTag";
        GetComponent<MeshRenderer>().sortingLayerName = sortlayerName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
