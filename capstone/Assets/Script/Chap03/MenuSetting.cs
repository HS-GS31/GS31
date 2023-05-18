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

        Debug.Log("필요 요소 : ");
        for (int i = 0; i < 4; i++)
        {
            ingredients[i] = transform.GetChild(i + 1).gameObject.tag;   
        }

        Debug.Log("배열 요소 : ");
        int j = 0;
        foreach(string obj in ingredients)
        {
            Debug.Log(j+ ": "+obj);
            j++;
        }

        //메뉴 세팅후 manager에게 보내기
        gameManager.GetComponent<GameManager>().setNowMenu(this.gameObject);
    }

    public string[] getMenu()
    {   
        return ingredients;
    }
}
