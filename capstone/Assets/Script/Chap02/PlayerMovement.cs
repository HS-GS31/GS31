using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 8.0f;

    [SerializeField] private SoundManager soundManager;
    [SerializeField] private Transform handle;
    [SerializeField] private Transform navCamera;
    [SerializeField] private GameObject chapterExplanation;
    [SerializeField] private GameObject guideUI;

    [SerializeField] private Transform[] before_transforms;
    [SerializeField] private OneGrabRotateTransformer oneHand;
    [SerializeField] private TwoGrabRotateTransformer twoHand;
    [SerializeField] private LoaderScene loaderScene;

    Queue<Route> roadQueue;
    Drive[] drives = new Drive[2];


    bool isHandle = false;
    float driveTime = 0;
    const float showChapterTime = 5.0f;
    const float showGuideTime = 2.0f;


    //왼쪽은 false, 오른쪽은 true

    // Start is called before the first frame update
    void Start()
    {

        //돌아오는 포인트 Queue에 넣기
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
            roadQueue.Enqueue(new Route(isCheck,
                before_transforms[i].position,
                before_transforms[i].rotation.eulerAngles)
            );
        }

        //UI
        chapterExplanation.SetActive(true);
        guideUI.SetActive(false);
        StartCoroutine(Show_UI_Chapter());
    }

    // Update is called once per frame
    void Update()
    {
    #if UNITY_EDITOR
        Debug_Handle();
    #endif
        StartDrive();
        Driving();
        navCamera.transform.position = new Vector3(
            transform.position.x,
            transform.position.y + 12,
            transform.position.z
        );
    }


    #region DriveFunction
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
                else {
                    Drive_Straight();
                }

                //완료 되었는가
                if(drive.Action(driveTime))
                {
                    soundManager.DontPlay();

                    if (roadQueue.Count <= 0)
                    {
                        //Debug.Log("완료");
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

                            
                            handle.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

                            //틀렸습니다 UI + 2초동안은 핸들 잡기 기능 X
                            StartCoroutine(Show_UI_Guide());
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
       
    }

    //get으로 가져온 rotate의 값에서 -90을 넘거나 90을 넘으면 왼쪽 오른쪽으로 가짐

    void Handle_Left()
    {
        //회전
        transform.Rotate(0, Time.deltaTime * -30, 0);
        transform.Translate(0,0, speed * 0.65f * Time.deltaTime);

        //이동
    }
    void Handle_Right()
    {
        transform.Rotate(0, Time.deltaTime * 30, 0);
        transform.Translate(0, 0, speed*2/5 * Time.deltaTime);
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
    #endregion

    #region UI
    IEnumerator Show_UI_Chapter()
    {
        drives[0].driveAble = false; //핸들 잡아도 됨

        //핸들 잡아도 안되는 기능
        yield return new WaitForSeconds(showChapterTime);

        chapterExplanation.SetActive(false);//UI 제거
        drives[0].driveAble = true; //핸들 잡아도 됨

        //
    }
    IEnumerator Show_UI_Guide()
    {
        //핸들 잡아도 안되는 기능

        //Debug.Log("엄");
        guideUI.SetActive(true);
        yield return new WaitForSeconds(showGuideTime);

        //UI 제거
        guideUI.SetActive(false);
        //핸들 잡아도 됨
        drives[1].driveAble = true; //핸들 잡아도 됨

    }

    #endregion
}
