using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 25.0f;

    [SerializeField] private float duringTime = 2f;
    [SerializeField] private float rotateTime = 3f;

    [SerializeField] private SoundManager gameManager;
    [SerializeField] private Transform handle;

    bool isHandle = false;

    bool isDrive = false;
    bool isRotate = false;

    bool driveAble = true;
    bool rotateAble = true;

    bool isLeft = false;

    float driveTime = 0;
    bool[] roadRight = new bool[] { false, true, false}; //����

    Drive[] drives = new Drive[2];

    //������ false, �������� true

    // Start is called before the first frame update
    void Start()
    {
        StraightDrive straightDrive = new StraightDrive();
        HorizontalDrive horizontalDrive = new HorizontalDrive();
        drives[0] = straightDrive;
        drives[1] = horizontalDrive;
    }

    // Update is called once per frame
    void Update()
    {
        StartDrive();
        Driving();
    }

    void FixedUpdate()
    {
        //if(handle.rotation.y <= -0.69 || 0.69 <= handle.rotation.y)
            //Debug.Log(handle.rotation.y);
    }

    private void StartDrive()
    {
        foreach (Drive drive in drives)
        {
            bool isCheck = drive.StartDrive(isHandle, handle.rotation.y);
            if (isCheck)
            {
                driveTime = 0;
                gameManager.Play();
            }
            if (drive.isDrive)
            {
                break;
            }
        }
    }
    void Driving()
    {
        driveTime += Time.deltaTime;
        for(int i = 0; i < drives.Length; i++)
        {
            Drive drive = drives[i];
            if(drive.isDrive)
            {
                if(i == 1)
                {
                    HorizontalDrive hd = drive as HorizontalDrive;
                    if (hd.GetIsLeft())
                    {
                        Handle_Left();
                    }
                    else
                    {
                        Handle_Right();
                    }
                }
                else
                {
                    Drive_Straight();
                }

                if(drive.Action(driveTime))
                {
                    gameManager.DontPlay();
                }
                //�ϴ� if�� 0, 1�̸� isLeft�� Ʈ�� �̵� �Լ� �Ǻ�
                //�Ű������� driveTime, ���� bool Ÿ��
                //DontPlay
            }
        }


    }


    void Drive_Straight()
    {
        //Debug.Log("����");
        transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
    }


    //
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
