using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientController : MonoBehaviour
{
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "STICK")
        {
            Debug.Log("접촉!! 트리거 on");
            collision.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;      //꼬치 트리거 on
            this.gameObject.GetComponent<MeshCollider>().isTrigger = true;              //음식 트리거 on
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
            Debug.Log(this.gameObject.transform.parent +"는 부모" + "\n" + gameObject +"는 자식");
            
            return;
        }

    }
    */
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "STICK")
        {
            this.gameObject.transform.parent = other.gameObject.transform;
            this.gameObject.transform.parent = other.gameObject.transform;
            other.gameObject.GetComponent<MenuController>().push(this.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "STICK")
        {
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
            this.gameObject.GetComponent<MeshCollider>().isTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "STICK")
        {
            other.gameObject.GetComponent<MenuController>().pop();
            this.gameObject.transform.parent = null;
        }
    }
}