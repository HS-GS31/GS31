using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCheck : MonoBehaviour
{
    public int checkFood = 0; // īƮ�� �´� ������ � ���Դ��� Ȯ��

    // Start is called before the first frame update
    void Start()
    {
        checkFood = 0;
    }

    public void increaseCheckFood()
    {
        Debug.Log("increase Check Food");
        checkFood++;
    }

    public int getCheckFood()
    {
        return checkFood;
    }
}
