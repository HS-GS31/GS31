using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCheck : MonoBehaviour
{
    public int checkFood = 0; // 카트에 맞는 음식이 몇개 들어왔는지 확인

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
