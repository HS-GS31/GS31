using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    int cnt = 0;        //3명이 되면 챕터 종료
    public GameObject[] spawnPoint;
    Transform now_spawnPoint;
    public GameObject now_customer;     //현재 주문중인 사람.
    public GameObject[] customers;
    bool ordering;
    // Update is called once per frame
    private void Start()
    {
        spawnPoint[0].transform.position = new Vector3(-5, 0, -3.5f);
        spawnPoint[1].transform.position = new Vector3(-5, 0, 3);
        ordering = false;
        SpawnCustomer();        //시작하마자 손님 한분 소환.
    }

    public void Take(GameObject menu, bool res)
    {        
        //넘기고 이동.
        if(now_customer.GetComponent<CustomerController>().getStat() == 2)
        {
            now_customer.GetComponent<CustomerController>().setEmoji(res);
            if (res)            //올바른 음식을 받은경우.
            {
                Destroy(menu);
                Debug.Log("음식 받음!!");

                now_customer.GetComponent<CustomerController>().setStat(3);
                Debug.Log("올바른 음식을 받았다!!");

                now_customer = null;                //현재 손님이 없는 상태로 변경.
                cnt++;                              //손님 받은 카운트 증가.
                if (now_customer == null && cnt < 3)
                {
                    SpawnCustomer();
                    Debug.Log("손님 받은 수 : " + cnt);
                }
            }
        }
    }

    private void SpawnCustomer()
    {
        int rndCus = Random.Range(0, 3);
        int rndPos = Random.Range(0, 2);
        GameObject customer = Instantiate(customers[rndCus]);
        customer.transform.position = spawnPoint[rndPos].transform.position;
        now_customer = customer;
        Debug.Log("손님 온다");
    }
}