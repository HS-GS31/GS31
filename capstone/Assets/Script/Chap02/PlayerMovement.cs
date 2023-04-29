using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 25.0f;

    [SerializeField] private SoundManager soundManager;
    [SerializeField] private Transform handle;

    [SerializeField] private Transform[] before_transforms;
    [SerializeField] private OneGrabRotateTransformer oneHand;
    [SerializeField] private TwoGrabRotateTransformer twoHand;
    [SerializeField] private LoaderScene loaderScene;


    bool isHandle = false;


    float driveTime = 0;
    Queue<Route> roadQueue;

    Drive[] drives = new Drive[2];

    //������ false, �������� true

    // Start is called before the first frame update
    void Start()
    {
        roadQueue = new Queue<Route>();

        StraightDrive straightDrive = new StraightDrive();
        HorizontalDrive horizontalDrive = new HorizontalDrive();
        drives[0] = straightDrive;
        drives[1] = horizontalDrive;

        for(int i = 0; i < before_transforms.Length; i++)
        {
            bool isCheck;
            if (i % 2 == 0) {
                isCheck = true;
            }
            else {
                isCheck = false;
            }
            //Debug.Log("dd : " + before_transforms[i].position + " " + before_transforms[i].rotation.eulerAngles);
            roadQueue.Enqueue(new Route(isCheck,
                before_transforms[i].position,
                before_transforms[i].rotation.eulerAngles)
            );
            //Debug.Log("�ƾ� : " + i);

        }
    }

    // Update is called once per frame
    void Update()
    {
    #if UNITY_EDITOR
        Debug_Handle();
    #endif
        StartDrive();
        Driving();
    }

    private void StartDrive()
    {
        foreach (Drive drive in drives)
        {
            bool isCheck = drive.StartDrive(isHandle, handle.localRotation.y);
            if (isCheck)
            {
                driveTime = 0;
                soundManager.Play();
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
                    if (hd.GetIsLeft()) {
                        Handle_Left();
                    }
                    else {
                        Handle_Right();
                    }
                }
                else
                {
                    Drive_Straight();
                }

                //�Ϸ� �Ǿ��°�
                if(drive.Action(driveTime))
                {
                    soundManager.DontPlay();

                    if (roadQueue.Count <= 0)
                    {
                        Debug.Log("�Ϸ�");
                        loaderScene.GoScene3();
                        return;
                    }
                    //�Ϸ� �Ǹ� �ٷ� ť �� [���� �ڵ� �����°� ���� ����]
                    if (i == 1)
                    {
                        HorizontalDrive hd = drive as HorizontalDrive;

                        //��� �� ���� �� ��ȯ
                        Route route = roadQueue.Peek();
                        
                        //������ �´ٸ�
                        if (hd.GetIsLeft() == route.GetIsLeft())
                        {
                            roadQueue.Dequeue();
                            drives[0].driveAble = true;
                        }

                        //������ Ʋ���ٸ�
                        else
                        {
                            transform.position = route.GetPosition();
                            transform.localRotation = Quaternion.Euler(route.GetRotation());
                            drives[1].driveAble = true;
                            handle.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

                            
                        }
                    }
                    else
                    {
                        drives[1].driveAble = true;

                    }
                }
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
        if(!isHandle)
        {
            //�ڿ������� ������ ��������
            //handle.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
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

    //�ڵ� ����� ����� �ϴ� �뵵
    #if UNITY_EDITOR
    void Debug_Handle()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            setIsHandle(true);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            handle.localRotation = Quaternion.Euler(0.0f, -90f, 0.0f);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            handle.localRotation = Quaternion.Euler(0.0f, 90f, 0.0f);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            setIsHandle(false);
        }
    }

    #endif
}
