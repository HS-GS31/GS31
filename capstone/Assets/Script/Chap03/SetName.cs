using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetName : MonoBehaviour
{
    public GameObject name;
    // Start is called before the first frame update
    void Start()
    {
        name.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        //손과 맞닿으면 텍스트 출력
        if(other.gameObject.tag == "Hand")
        {
            name.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //떨어지면 텍스트 미출력.
        if (other.gameObject.tag == "Hand")
        {
            name.SetActive(false);
        }
    }
}
