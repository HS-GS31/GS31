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
    public Transform originPosition; // 원래 위치
    public TMP_Text checkText; // 화면에 나올 문구
    public GameObject cart;
    public bool checkIt; // 카트에 들어갈 재료가 맞는지 확인

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
        // 처음 잡았다가 놓으면 중력 처리로 떨어지게
        _rigid.useGravity = true;
        _rigid.isKinematic = false;
    }


    private void OnCollisionEnter(Collision collision)
    {

        // 카트에 놓았을 경우
        if (collision.collider.gameObject.CompareTag("CheckCollision"))
        {

            // 보드에 체크 표시
            boardcheck.SetActive(true);

            if (checkIt) // 담아야할 재료를 담은 경우
            {
                // 카트에 야채 상속
                foodcheck.transform.parent = cart.transform;

                checkText.text = "잘하셨습니다!";
                Invoke("HideText", 3f);
            }
            
            else
            {
                checkText.text = "다시 한번\n생각해보세요.";
                GoBackPosition();
                Invoke("HideText", 3f);
            }
        }

        // 바닥에 떨어졌을 경우
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
