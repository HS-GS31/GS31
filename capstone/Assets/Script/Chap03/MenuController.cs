using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    GameObject customerManager;
    private void Start()
    {
        customerManager = GameObject.Find("CustomerManager");
    }

    private void OnTriggerStay(Collider other)
    {
        
    }
}
