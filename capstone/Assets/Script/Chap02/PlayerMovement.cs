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


    //������ false, �������� true

    // Start is called before the first frame update
    void Start()
    {

        //���ƿ��� ����Ʈ Queue�� �ֱ�
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

                //�Ϸ� �Ǿ��°�
                if(drive.Action(driveTime))
                {
                    soundManager.DontPlay();

                    if (roadQueue.Count <= 0)
                    {
                        //Debug.Log("�Ϸ�");
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

                            
                            handle.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

                            //Ʋ�Ƚ��ϴ� UI + 2�ʵ����� �ڵ� ��� ��� X
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
        transform.Translate(0,0, speed * 0.65f * Time.deltaTime);

        //�̵�
    }
    void Handle_Right()
    {
        transform.Rotate(0, Time.deltaTime * 30, 0);
        transform.Translate(0, 0, speed*2/5 * Time.deltaTime);
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
    #endregion

    #region UI
    IEnumerator Show_UI_Chapter()
    {
        drives[0].driveAble = false; //�ڵ� ��Ƶ� ��

        //�ڵ� ��Ƶ� �ȵǴ� ���
        yield return new WaitForSeconds(showChapterTime);

        chapterExplanation.SetActive(false);//UI ����
        drives[0].driveAble = true; //�ڵ� ��Ƶ� ��

        //
    }
    IEnumerator Show_UI_Guide()
    {
        //�ڵ� ��Ƶ� �ȵǴ� ���

        //Debug.Log("��");
        guideUI.SetActive(true);
        yield return new WaitForSeconds(showGuideTime);

        //UI ����
        guideUI.SetActive(false);
        //�ڵ� ��Ƶ� ��
        drives[1].driveAble = true; //�ڵ� ��Ƶ� ��

    }

    #endregion
}
