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

    [SerializeField] private Transform[] before_transforms;

    bool isHandle = false;

    bool isDrive = false;
    bool isRotate = false;

    bool driveAble = true;
    bool rotateAble = true;

    bool isLeft = false;

    float driveTime = 0;
    Queue<Route> roadQueue;

    Drive[] drives = new Drive[2];

    //왼쪽은 false, 오른쪽은 true

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
            //Debug.Log("아아 : " + i);

        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug_Handle();
        StartDrive();
        Driving();
    }

    void FixedUpdate()
    {
        //Debug.Log(handle.localRotation.y);
    }

    private void StartDrive()
    {
        foreach (Drive drive in drives)
        {
            bool isCheck = drive.StartDrive(isHandle, handle.localRotation.y);
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

                //완료 되었는가
                if(drive.Action(driveTime))
                {
                    gameManager.DontPlay();

                    //완료 되면 바로 큐 비교
                    //맞으면 스택 제거 + 아래 for문
                    //틀리면 스택 맨 앞 되돌리기

                    for (int j = 0; j < drives.Length; j++)
                    {
                        if(i != j)
                        {
                            drives[j].driveAble = true;
                        }
                    }
                }
            }
        }


    }


    void Drive_Straight()
    {
        //Debug.Log("직진");
        transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
    }


    //
    public void setIsHandle(bool isHandle)
    {
        this.isHandle = isHandle;
    }

    //get으로 가져온 rotate의 값에서 -90을 넘거나 90을 넘으면 왼쪽 오른쪽으로 가짐

    void Handle_Left()
    {
        //회전
        transform.Rotate(0, Time.deltaTime * -30, 0);
        transform.Translate(0,0, 25 * Time.deltaTime);

        //이동
    }
    void Handle_Right()
    {
        transform.Rotate(0, Time.deltaTime * 30, 0);
        transform.Translate(0, 0, 15 * Time.deltaTime);
    }

    //핸들 안잡고 디버깅 하는 용도
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
