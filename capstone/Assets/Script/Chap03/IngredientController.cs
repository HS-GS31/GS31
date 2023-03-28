using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientController : MonoBehaviour
{
    GameObject parent;
    private void Update()
    {
        //��ƽ�� ���� �������� �߷� ���� X
        if(gameObject.transform.parent.gameObject.tag == "STICK")
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false;
           // gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "STICK")
        {
            this.gameObject.transform.parent = other.gameObject.transform;
            this.parent = other.gameObject;
            this.gameObject.GetComponent<MeshCollider>().isTrigger = true;
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "STICK")
        {
            //parent.GetComponent<MenuController>().pop(gameObject);
            this.gameObject.transform.parent = null;
            this.parent = null;
            this.gameObject.GetComponent<MeshCollider>().isTrigger = false;
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "STICK")
        {
            Debug.Log("����!! Ʈ���� on");
            this.gameObject.GetComponent<MeshCollider>().isTrigger = true;
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;

            //Stick�� ������������...
            //if (parent != null)
            {
                parent.GetComponent<MenuController>().push(gameObject);
            }
        }   
    }
}
