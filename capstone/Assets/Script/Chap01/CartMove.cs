using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class CartMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float moveTime;
    public bool isGrab;
    public GameObject player;
    public GameObject cart;
    public TMP_Text reTryText; // 다시 고르라는 문구
    private int checkMove = 0; // 몇번 움직였는지 확인

    // Start is called before the first frame update
    void Start()
    {
        isGrab = false;
        checkMove = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MoveCoroutine()
    {
        float timer = 0f;
        while(timer < moveTime)
        {
            cart.transform.Translate(Vector3.right * speed * Time.deltaTime);
            player.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public void GrabHandle()
    {
        if (!isGrab)
        {
            Debug.Log("Grab Handle");
            isGrab = true;

            if (checkCartMove()) // 담긴 재료가 3개거나 6개면
            {
                if(checkMove == 0)
                {
                    // 야채 코너가 끝났을 경우 카트 앞으로
                    checkMove++;
                    StartCoroutine(MoveCoroutine());
                }

                else if(checkMove == 1)
                {
                    // 고기 & 생선 코너가 끝났을 경우 다음 씬으로 가는 UI 등장

                }
            }

            else
            {
                reTryText.text = "마저 골라주세요";
                Invoke("HideText", 3f);
            }
            
        }
    }

    public void NotGrabHandle()
    {
        Debug.Log("Not Grab Handle");
        isGrab = false;
    }

    private void HideText()
    {
        reTryText.text = " ";
    }

    private bool checkCartMove()
    {
        int checkFood = GameObject.Find("GameManager").GetComponent<FoodCheck>().getCheckFood();
        if (checkFood == 3 || checkFood == 6)
        {
            return true;
        }
        else return false;
    }
}
