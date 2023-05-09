using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sender : MonoBehaviour
{
    GameObject customerManager;
    private GameObject now_menu;
    public bool ordering;
    // Start is called before the first frame update
    void Start()
    {
        customerManager = GameObject.Find("CustomerManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "STICK") { 
            string[] menus = other.GetComponent<MenuController>().getMenu();
            //Debug.Log("���� �޴� : " + menus);
            int i = 0;
            foreach(string obj in menus)
            {
                Debug.Log(i + " : " + obj);
                i++;
            }
            string[] checking = now_menu.GetComponent<MenuSetting>().getMenu();
            i = 0;
            foreach (string obj in checking)
            {
                Debug.Log(i + " : " + obj);
            }
            bool res = checkMenu(menus, checking);
            Debug.Log("���ö� �浹!!");
            customerManager.GetComponent<CustomerManager>().Take(other.gameObject, res);
        }
    }

    public void setMenu(GameObject menu)
    {
        this.now_menu = menu;
        Debug.Log("���� �޴���??? : " + now_menu);
    }

    private bool checkMenu(string[] menu, string[] checking)
    {

        for (int i = 0; i < 4; i++)
        {
            if (menu[i] != checking[i])
            {
                Debug.Log("����....");
                return false;
            }
        }
        //�ݺ����� ���Դٴ� ���� 4���� ��Ұ� ��� ���ٴ� ���� �ǹ��Ѵ�.
        Debug.Log("����!!");
        return true;
    }
}
