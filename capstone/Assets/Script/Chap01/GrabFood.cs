using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabFood : MonoBehaviour
{
    private Rigidbody _rigid;
    private bool checkCollision = false;
    public GameObject boardcheck;
    public GameObject foodcheck;
    public GameObject cart;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        checkCollision = false; 
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // ������ ���� īƮ�� �ƴϸ� �߷� �����ϰ� ����ġ�� (Ư����ġ���� �������ٰ�)
        else
        {
            _rigid.useGravity = false;
        }
        */
    }


    public void UnGrabVegetable()
    {
        // ó�� ��Ҵٰ� ������ �߷� ó���� ��������
        _rigid.useGravity = true;
        checkCollision = false;
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.gameObject.CompareTag("CheckCollision"))
        {
            // īƮ�� �������Ÿ� checkCollision = true
            checkCollision = true;

            // ���忡 üũ ǥ��
            boardcheck.SetActive(true);

            // īƮ�� ��ä ���
            foodcheck.transform.parent = cart.transform;
        }
    }

}
