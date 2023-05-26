using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSetting : MonoBehaviour
{
    GameObject gameManager;
    private string[] ingredients;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");

        ingredients = new string[4];
        for (int i = 0; i < 4; i++)
        {
            ingredients[i] = transform.GetChild(i + 1).gameObject.tag;   
        }
        int j = 0;
        foreach(string obj in ingredients)
        {
            j++;
        }

        //�޴� ������ manager���� ������
        gameManager.GetComponent<GameManager>().setNowMenu(this.gameObject);
    }

    public string[] getMenu()
    {   
        return ingredients;
    }
}
