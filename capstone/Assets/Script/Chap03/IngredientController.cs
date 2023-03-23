using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientController : MonoBehaviour
{

    private void Update()
    {
        
        //��ƽ�� ���� �������� �߷� ���� X
        if(gameObject.transform.parent.gameObject.tag == "Menu")
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
        
    }
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "STICK")
        {
            this.gameObject.transform.parent = other.gameObject.transform.parent.transform;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            //Debug.Log("����!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "STICK")
        {
            this.gameObject.transform.parent = null;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            //Debug.Log("Ż��!");
        }
    }
}
