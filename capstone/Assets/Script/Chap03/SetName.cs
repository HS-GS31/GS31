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
        //�հ� �´����� �ؽ�Ʈ ���
        if(other.gameObject.tag == "Hand")
        {
            name.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //�������� �ؽ�Ʈ �����.
        if (other.gameObject.tag == "Hand")
        {
            name.SetActive(false);
        }
    }
}
