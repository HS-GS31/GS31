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

                //완료 되었는가
                if(drive.Action(driveTime))
                {
                    soundManager.DontPlay();

                    if (roadQueue.Count <= 0)
                    {
                        Debug.Log("완료");
                        loaderScene.GoScene3();
                        return;
                    }
                    //완료 되면 바로 큐 비교 [물론 핸들 돌리는거 끝난 직후]
                    if (i == 1)
                    {
                        HorizontalDrive hd = drive as HorizontalDrive;

                        //경로 다 가면 씬 전환
                        Route route = roadQueue.Peek();
                        
                        //방향이 맞다면
                        if (hd.GetIsLeft() == route.GetIsLeft())
                        {
                            roadQueue.Dequeue();
                            drives[0].driveAble = true;
                        }

                        //방향이 틀리다면
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
        //Debug.Log("직진");
        transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
    }


    //
    public void setIsHandle(bool isHandle)
    {
        this.isHandle = isHandle;
        if(!isHandle)
        {
            //자연스럽게 밑으로 내려가는
            //handle.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
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
