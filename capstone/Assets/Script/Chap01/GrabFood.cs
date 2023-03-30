using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabFood : MonoBehaviour
{
    private Rigidbody _rigid;
    private bool checkCollision = false;
    public GameObject boardcheck;
    public GameObject foodcheck;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        checkCollision = false; 
    }

    // Update is called once per frame
    void Update()
    {
        // 떨어진 곳이 카트라면
        if (checkCollision == true)
        {
            Debug.Log("checkCollision");
        }

        /*
        // 떨어진 곳이 카트가 아니면 중력 해제하고 원위치로 (특정위치까지 떨어지다가)
        else
        {
            _rigid.useGravity = false;
        }
        */
    }


    public void UnGrabVegetable()
    {
        // 처음 잡았다가 놓으면 중력 처리로 떨어지게
        _rigid.useGravity = true;

        if(checkCollision == true)         
        {
            Debug.Log("카트에 들어감2");
            boardcheck.SetActive(true);
            checkCollision = false;
        }
            
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("CheckCollision"))
        {
            Debug.Log("111111111111111111");
            // 카트에 떨어진거면 checkCollision = true
            checkCollision = true;
        }
    }

}
