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
            //�մ� �Ŵ��� ������Ʈ�� ������.
            customerManager.GetComponent<CustomerManager>().Take(this.gameObject);
        }
    }
    public void setTaken(bool taken)      //�մ��� �޴��� ���� ���¸� Ȯ��.
    {
        this.taken = taken;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("��Ҵ�!!1");
    }
}
