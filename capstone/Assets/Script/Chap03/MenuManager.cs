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
        int rnd = Random.Range(0, 5);               //1~5 랜덤값 생성.
        GameObject order = Instantiate(menus[rnd]);
        sender.GetComponent<Sender>().setMenu(order);

        //게임메니저에게 랜덤 값 보내기
        gameManager.GetComponent<GameManager>().addCount(rnd);
        return order;
    }
}