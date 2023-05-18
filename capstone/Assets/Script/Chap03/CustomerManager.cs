using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomerManager : MonoBehaviour
{
    int cnt = 0;        //3���� �Ǹ� é�� ����
    public GameObject[] spawnPoint;
    Transform now_spawnPoint;
    public GameObject now_customer;     //���� �ֹ����� ���.
    public GameObject[] customers;
    private GameObject gameManager;
    bool ordering;
    // Update is called once per frame
    private void Start()
    {
        spawnPoint[0].transform.position = new Vector3(-5, 0, -3.5f);
        spawnPoint[1].transform.position = new Vector3(-5, 0, 3);
        ordering = false;
        gameManager = GameObject.Find("GameManager");
        SpawnCustomer();        //�����ϸ��� �մ� �Ѻ� ��ȯ.
    }

    public void Take(GameObject menu, bool res)
    {        
        //�ѱ�� �̵�.
        if(now_customer.GetComponent<CustomerController>().getStat() == 2)
        {
            now_customer.GetComponent<CustomerController>().setEmoji(res);
            if (res)            //�ùٸ� ������ �������.
            {
                Destroy(menu);

                now_customer.GetComponent<CustomerController>().setStat(3);

                now_customer = null;                //���� �մ��� ���� ���·� ����.
                cnt++;                              //�մ� ���� ī��Ʈ ����.
                if (now_customer == null && cnt < 3)
                {
                    SpawnCustomer();
                    Debug.Log("�մ� ���� �� : " + cnt);
                }
                if(cnt == 3)
                {
                    cnt++;      //�ߺ� �߻� ������.
                    gameManager.GetComponent<GameManager>().ChapOut();
                    Invoke("GoNextScene", 5f);
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
    }
    private void GoNextScene()
    {
        SceneManager.LoadScene("Chap04");
    }
}