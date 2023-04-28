using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MenuManager : MonoBehaviour
{
    public GameObject[] menus;
    private GameObject sender;

    private void Start()
    {
        sender = GameObject.Find("SenderPlate");
    }
    public GameObject getRandomFood()
    {
        int rnd = Random.Range(0, 5);               //1~5 ·£´ý°ª »ý¼º.
        GameObject order = Instantiate(menus[rnd]);
        sender.GetComponent<Sender>().setMenu(order);
        return order;
    }
}