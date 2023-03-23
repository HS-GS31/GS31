using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    GameObject customerManager;
    private bool taken = false;
    private void Start()
    {
        customerManager = GameObject.Find("CustomerManager");
    }
}
