using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    int cnt = 0;        //3���� �Ǹ� é�� ����
    public GameObject[] spawnPoint;
    Transform now_spawnPoint;
    public GameObject now_customer;
    public GameObject[] customers;
    private bool changeCustomer;
    // Update is called once per frame
    private void Start()
    {
        spawnPoint[0].transform.position = new Vector3(-5, 0, -3.5f);
        spawnPoint[1].transform.position = new Vector3(-5, 0, 3);
    }
    void Update()
    {
       
    }

    public void Take(GameObject menu)
    {
        //������ �մ� �տ� �ѱ��
        menu.transform.position = now_customer.transform.GetChild(1).transform.GetChild(0).transform.position;
        menu.GetComponent<MenuController>().setTaken(true);
        cnt++;      //�մ� ���� ī��Ʈ ����.

        //�ѱ�� �̵�.
        if(now_customer.GetComponent<CustomerController>().getStat() == 2)
        {
            now_customer.GetComponent<CustomerController>().setStat(3);
        }
        SpawnCustomer();
    }
    private void SpawnCustomer()
    {
        GameObject customer = Instantiate(customers[0]);
        customer.transform.position = spawnPoint[0].transform.position;
    }

    public void setCustomer(GameObject customer)
    {
        now_customer = customer;
        Debug.Log(customer.name);
    }
}