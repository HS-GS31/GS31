using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MenuManager : MonoBehaviour
{
    public GameObject[] menus;
    private GameObject sender;
    private GameObject gameManager;
    private void Start()
    {
        sender = GameObject.Find("SenderPlate");
        gameManager = GameObject.Find("GameManager");
    }
    public GameObject getRandomFood()
    {
        int rnd = Random.Range(0, 5);               //1~5 ������ ����.
        GameObject order = Instantiate(menus[rnd]);
        sender.GetComponent<Sender>().setMenu(order);

        //���Ӹ޴������� ���� �� ������
        gameManager.GetComponent<GameManager>().addCount(rnd);
        return order;
    }
}