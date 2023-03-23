using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sender : MonoBehaviour
{
    GameObject customerManager;
    public bool ordering;
    // Start is called before the first frame update
    void Start()
    {
        customerManager = GameObject.Find("CustomerManager");
        ordering = false;
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "STICK")
        {
            customerManager.GetComponent<CustomerManager>().Take(other.gameObject.transform.parent.gameObject);
            Debug.Log("Á¢½Ã¶û Ãæµ¹!!");
        }
    }
}
