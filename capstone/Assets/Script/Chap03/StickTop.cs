using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickTop : MonoBehaviour
{
    Collider coll;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shrimp")
        {
            other.GetComponent<BoxCollider>().isTrigger = true;
            other.GetComponent<Rigidbody>().useGravity = false;
            Debug.Log("새우가 충돌!! 통과!!");
        }
    }
}
