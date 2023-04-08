using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientController : MonoBehaviour
{
    private bool isStickTop = false;        //스틱의 꼭대기에서 들어온 음식인가 판단.
    Rigidbody rigid;
    Collider coll;
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
        if (this.gameObject.tag == "Shrimp")
        {
            coll = gameObject.GetComponent<CapsuleCollider>();
        }
        else
        {
            coll = gameObject.GetComponent<MeshCollider>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "StickTop")
        {
            Debug.Log("접촉!! Stick Top coll !!!!!!!");
            this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
            Debug.Log(this.gameObject.transform.parent +"는 부모" + "\n" + gameObject +"는 자식");
        
            return;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "StickTop")
        {
            isStickTop = true;
        }

        if(other.gameObject.tag == "STICK" && isStickTop)
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
            this.rigid.useGravity = false;
            this.coll.isTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "STICK" || other.gameObject.tag=="StickTop")
        {
            other.gameObject.GetComponent<MenuController>().pop();

            this.rigid.useGravity = true;
            this.coll.isTrigger = false;
            this.gameObject.transform.parent = null;
            isStickTop = false;
        }
    }
}