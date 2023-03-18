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
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -1.38f)
        {
            //손님 매니저 오브젝트로 보내기.
            customerManager.GetComponent<CustomerManager>().Take(this.gameObject);
        }
    }
    public void setTaken(bool taken)      //손님이 메뉴를 받은 상태를 확인.
    {
        this.taken = taken;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("닿았다!!1");
    }
}
