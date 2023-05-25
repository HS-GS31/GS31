using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CartMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float moveTime;
    //public bool isGrab;

    public GameObject player;
    public GameObject cart;
    public GameObject clipboardCenter;
    public GameObject clipboard;
    public GameObject nextChapUI;

    public TMP_Text reTryText; // �ٽ� ����� ����
    private int checkMove = 0; // ��� ���������� Ȯ��

    private static int checkFood = 0;

    public int hand = 0;

    // Start is called before the first frame update
    void Start()
    {
        reTryText.text = " ";
        //isGrab = false;
        hand = 0;
        checkMove = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MoveCoroutine()
    {
        float timer = 0f;
        while (timer < moveTime)
        {
            cart.transform.Translate(Vector3.right * speed * Time.deltaTime);
            player.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            clipboardCenter.transform.Translate(Vector3.down * speed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public void GrabHandle()
    {
        //if (!isGrab)
        if(hand == 0)
        {
            Debug.Log("Grab Handle" + hand);
            //isGrab = true;
            hand++;
        }

        else if(hand == 1)
        {
            Debug.Log("2 Grab Handle" + hand);
            hand++;
            if (checkCartMove()) // ��� ��ᰡ 3���ų� 6����
            {
                if (checkMove == 0)
                {
                    // ��ä �ڳʰ� ������ ��� īƮ ������
                    checkMove++;
                    //clipboard.GetComponent<MeshCollider>().enabled = false;
                    //clipboard.SetActive(false);
                    StartCoroutine(MoveCoroutine());
                    //clipboard.GetComponent<MeshCollider>().enabled = true;
                    //clipboard.SetActive(true);
                }

                else if (checkMove == 1)
                {
                    if (checkFood != 3)
                    {
                        // ��� & ���� �ڳʰ� ������ ��� ���� ������ ���� UI ����
                        GameObject.Find("GameManager").transform.Find("shopping cart").gameObject.SetActive(false);
                        nextChapUI.transform.GetChild(0).gameObject.SetActive(true);
                        Invoke("GoNextChap02", 5f);
                    }

                    if (checkFood == 3)
                    {
                        reTryText.text = "���� ����ּ���";
                        Invoke("HideText", 3f);
                    }
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
        Debug.Log("Not Grab Handle" + hand);
        //isGrab = false;
        hand--;
    }

    private void HideText()
    {
        reTryText.text = " ";
    }

    private bool checkCartMove()
    {
        checkFood = GameObject.Find("GameManager").GetComponent<FoodCheck>().getCheckFood();
        if (checkFood == 3 || checkFood == 6)
        {
            return true;
        }
        else return false;
    }

    private void GoNextChap02()
    {
        SceneManager.LoadScene("Chap02");
    }
}
