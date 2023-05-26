using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sender : MonoBehaviour
{
    GameObject customerManager;
    GameObject gameManager;
    private GameObject now_menu;
    public bool ordering;
    // Start is called before the first frame update
    void Start()
    {
        customerManager = GameObject.Find("CustomerManager");
        gameManager = GameObject.Find("GameManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "STICK") { 
            string[] menus = other.GetComponent<MenuController>().getMenu();
            string[] checking = now_menu.GetComponent<MenuSetting>().getMenu();
            bool res = checkMenu(menus, checking);
            customerManager.GetComponent<CustomerManager>().Take(other.gameObject, res);
        }
    }

    public void setMenu(GameObject menu)
    {
        this.now_menu = menu;
    }

    private bool checkMenu(string[] menu, string[] checking)
    {
        for (int i = 0; i < 4; i++)
        {
            if (menu[i] != checking[i])
            {
                return false;
            }
        }
        //�ݺ����� ���Դٴ� ���� 4���� ��Ұ� ��� ���ٴ� ���� �ǹ��Ѵ�.
        return true;
    }
}
