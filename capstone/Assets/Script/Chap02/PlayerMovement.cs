using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 25.0f;
    [SerializeField] private float duringTime = 2f;
    [SerializeField] private float rotateTime = 2.2f;
    [SerializeField] private SoundManager gameManager;
    [SerializeField] private Transform leftObject;
    [SerializeField] private Transform rightObject;
    [SerializeField] private Transform handle;

    bool isDrive = false;
    bool driveAble = true;
    bool rotateAble = true;
    bool isRotate = false;
    bool isHandle = false;
    bool isLeft = false;
    float driveTime = 0;
    bool[] roadRight = new bool[] { false, true, false};

    //������ false, �������� true

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        PlayDrive();
        
        if (isDrive)
        {
            driveTime += Time.deltaTime;
            Drive_Straight();

            //����
            if(duringTime <= driveTime)
            {
                isDrive = false;
                gameManager.DontPlay();
            }
        }

        //
        if(isRotate)
        {
            if(isLeft)
            {
                Handle_Left();
            }
            else
            {
                Handle_Right();
            }

            //�ð� ���ϴ� �Լ��� ����ȭ �Ҽ�����������
            driveTime += Time.deltaTime;

            if (rotateTime <= driveTime)
            {
                isRotate = false;
                gameManager.DontPlay();
            }
        }

    }

    void FixedUpdate()
    {
        if(handle.rotation.y <= -0.69 || 0.69 <= handle.rotation.y)
            Debug.Log(handle.rotation.y);
    }

    void Drive_Straight()
    {
        //Debug.Log("����");
        transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
    }


    private void PlayDrive()
    {
        //�����ϰ� �ڵ� ������ �ʴ� �̻� ���� X
        if (isDrive == false && driveAble && isHandle)
        {
            driveTime = 0;
            isDrive = true;
            driveAble = false;
            gameManager.Play();
        }
        //
        else if(!isRotate && rotateAble && !isDrive && isHandle && (handle.rotation.y <= -0.69 || 0.69 <= handle.rotation.y ))
        {
            isRotate = true;
            driveTime = 0;
            rotateAble = false;


            //�ڵ��� ���� ���� + -90 / 90 ���ָ� �Ѿ��rotate
            if (handle.rotation.y <= -0.69)
            {
                isLeft = true;
            }
            else
            {
                isLeft = false;
            }
        }
    }

    public void setIsHandle(bool isHandle)
    {
        this.isHandle = isHandle;
    }

    //get���� ������ rotate�� ������ -90�� �Ѱų� 90�� ������ ���� ���������� ����

    void Handle_Left()
    {
        //ȸ��
        transform.Rotate(0, Time.deltaTime * -30, 0);
        transform.Translate(0,0, 25 * Time.deltaTime);

        //�̵�
    }
    void Handle_Right()
    {
        transform.Rotate(0, Time.deltaTime * 30, 0);
        transform.Translate(0, 0, 15 * Time.deltaTime);
    }
}
