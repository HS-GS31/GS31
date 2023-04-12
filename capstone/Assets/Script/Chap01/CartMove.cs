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
    public TMP_Text reTryText; // �ٽ� ����� ����
    private int checkMove = 0; // ��� ���������� Ȯ��

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

            if (checkCartMove()) // ��� ��ᰡ 3���ų� 6����
            {
                if(checkMove == 0)
                {
                    // ��ä �ڳʰ� ������ ��� īƮ ������
                    checkMove++;
                    StartCoroutine(MoveCoroutine());
                }

                else if(checkMove == 1)
                {
                    // ��� & ���� �ڳʰ� ������ ��� ���� ������ ���� UI ����

                }
            }

            else
            {
                reTryText.text = "���� ����ּ���";
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
