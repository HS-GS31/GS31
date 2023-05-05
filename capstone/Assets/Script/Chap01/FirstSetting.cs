using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSetting : MonoBehaviour
{
    public GameObject vegetables;
    public GameObject meetandfish;


    // Start is called before the first frame update
    void Start()
    {
        InCartFoodFalse();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InCartFoodFalse()
    {
        for(int i = 0; i < vegetables.transform.childCount; i++)
        {
            vegetables.transform.GetChild(i).gameObject.SetActive(false);
        }
        for(int  j = 0; j<meetandfish.transform.childCount; j++)
        {
            meetandfish.transform.GetChild(j).gameObject.SetActive(false);
        }
    }
}
