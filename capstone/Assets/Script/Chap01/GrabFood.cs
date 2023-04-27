using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class GrabFood : MonoBehaviour
{
    private Rigidbody _rigid;    

    public GameObject boardcheck;
    public GameObject foodcheck;
    public Transform originPosition; // ���� ��ġ
    public TMP_Text checkText; // ȭ�鿡 ���� ����
    public GameObject cart;
    public bool checkIt; // īƮ�� �� ��ᰡ �´��� Ȯ��
    private bool once = true; // īƮ�� �� �ѹ��� üũ

    public GameObject vegetables; // īƮ�� ���� ä�� ��ġ �θ�
    public GameObject meetandfish; // īƮ�� ���� ���� ��ġ �θ�
    public int foodNum;
    public GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        checkText.text = " ";
        once = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UnGrabVegetable()
    {
        // ó�� ��Ҵٰ� ������ �߷� ó���� ��������
        _rigid.useGravity = true;
        _rigid.isKinematic = false;
    }


    private void OnCollisionEnter(Collision collision)
    {

        // īƮ�� ������ ���(īƮ �ٴڿ� ��Ҵ��� Ȯ��)
        if (collision.collider.gameObject.CompareTag("CheckCollision"))
        {

            // ���忡 üũ ǥ��
            boardcheck.SetActive(true);

            if (checkIt) // ��ƾ��� ��Ḧ ���� ���
            {
                // īƮ�� ��ä ���
                //foodcheck.transform.parent = cart.transform;
                if(foodNum < 3)
                    vegetables.gameObject.transform.GetChild(foodNum).gameObject.SetActive(true);
                else if(foodNum < 6)
                    meetandfish.gameObject.transform.GetChild(foodNum-3).gameObject.SetActive(true);
                
                if (once)
                {
                    // īƮ�� �ǰ� �� ������ ���� ����
                    GameObject.Find("GameManager").GetComponent<FoodCheck>().increaseCheckFood();
                    once = false;
                    checkText.text = "���ϼ̽��ϴ�!";
                    Target.SetActive(false);
                    Invoke("HideText", 3f);
                }
            }
            
            else
            {
                checkText.text = "�ٽ� �ѹ�\n�����غ�����.";
                GoBackPosition();
                Invoke("HideText", 3f);
            }
        }

        // �ٴڿ� �������� ���
        else if (collision.collider.gameObject.CompareTag("CheckCollision_Floor") ||
             collision.collider.gameObject.CompareTag("Cart"))
        {
            GoBackPosition();
        }
    }

    private void GoBackPosition()
    {
        _rigid.useGravity = false;
        _rigid.isKinematic = true;
        this.transform.position = originPosition.position;
        this.transform.rotation = originPosition.rotation;
        this.transform.localScale = originPosition.localScale;
        Debug.Log("123");
    }

    private void HideText()
    {
        checkText.text = " ";
    }

}
