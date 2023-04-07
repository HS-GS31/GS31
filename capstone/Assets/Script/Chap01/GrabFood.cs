using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GrabFood : MonoBehaviour
{
    private Rigidbody _rigid;    

    public GameObject boardcheck;
    public GameObject foodcheck;
    public Transform originPosition; // ���� ��ġ
    public TMP_Text checkText; // ȭ�鿡 ���� ����
    public GameObject cart;
    public bool checkIt; // īƮ�� �� ��ᰡ �´��� Ȯ��

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        checkText.text = " ";
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

        // īƮ�� ������ ���
        if (collision.collider.gameObject.CompareTag("CheckCollision"))
        {

            // ���忡 üũ ǥ��
            boardcheck.SetActive(true);

            if (checkIt) // ��ƾ��� ��Ḧ ���� ���
            {
                // īƮ�� ��ä ���
                foodcheck.transform.parent = cart.transform;

                checkText.text = "���ϼ̽��ϴ�!";
                Invoke("HideText", 3f);
            }
            
            else
            {
                checkText.text = "�ٽ� �ѹ�\n�����غ�����.";
                GoBackPosition();
                Invoke("HideText", 3f);
            }
        }

        // �ٴڿ� �������� ���
        if (collision.collider.gameObject.CompareTag("CheckCollision_Floor"))
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
