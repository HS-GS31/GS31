using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "STICK")
        {
            this.gameObject.transform.parent = other.gameObject.transform;
            Debug.Log("ºÎÂø!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "STICK")
        {
            this.gameObject.transform.parent = null;
            Debug.Log("Å»Ãâ!");
        }
    }
}