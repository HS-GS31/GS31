using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSetting : MonoBehaviour
{
    private string[] ingredients;
    // Start is called before the first frame update
    void Start()
    {
        ingredients = new string[4];

        Debug.Log("�ʿ� ��� : ");
        for (int i = 0; i < 4; i++)
        {
            ingredients[i] = transform.GetChild(i + 1).gameObject.tag;   
        }

        Debug.Log("�迭 ��� : ");
        int j = 0;
        foreach(string obj in ingredients)
        {
            Debug.Log(j+ ": "+obj);
            j++;
        }
    }

    public string[] getMenu()
    {   
        return ingredients;
    }
}
