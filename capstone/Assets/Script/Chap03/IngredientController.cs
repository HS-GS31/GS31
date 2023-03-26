using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientController : MonoBehaviour
{
    private Vector3 scale;
    private void Start()
    {
        scale = gameObject.transform.localScale;
    }

    private void Update()
    {
     
        //스틱에 꽂혀 있을때는 중력 영향 X
        if(gameObject.transform.parent.gameObject.tag == "STICK")
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "STICk")
        {
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "STICK")
        {
            this.gameObject.transform.parent = other.gameObject.transform;
            this.gameObject.GetComponent<MeshCollider>().isTrigger = true;
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "STICK")
        {
            this.gameObject.transform.parent = null;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            this.gameObject.GetComponent<MeshCollider>().isTrigger = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "STICK")
        {
            Debug.Log("탈출!!!! 트리거 무효");
            this.gameObject.GetComponent<MeshCollider>().isTrigger = false;
        }   
    }
}
