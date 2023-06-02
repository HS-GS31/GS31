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
    [SerializeField] private GameObject nextUI;
    [SerializeField] private GameObject navUI;

    [SerializeField] private Transform[] before_transforms;
    [SerializeField] private OneGrabRotateTransformer oneHand;
    [SerializeField] private TwoGrabRotateTransformer twoHand;
    [SerializeField] private Grabbable grabbable;
    [SerializeField] private LoaderScene loaderScene;

    Queue<Route> roadQueue;
    Drive[] drives = new Drive[2];


    bool isHandle = false;
    float driveTime = 0;
    const float showChapterTime = 10.0f;
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
        navUI.SetActive(false);
        StartCoroutine(Show_UI_Chapter());
    }

    // Update is called once per frame
    void Update()
    {
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
            Vector3 vector3 = handle.localRotation.eulerAngles;
            bool isCheck = drive.StartDrive(isHandle, vector3.y);
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
                        StartCoroutine(NextScene());
                        return;
                    }
                    //�Ϸ� �Ǹ� �ٷ� ť �� [���� �ڵ� �����°� ���� ����]
                    if (i == 1)
                    {
                        HorizontalDrive hd = drive as HorizontalDrive;

                        //��� �� ���� �� ��ȯ
                        Route route = roadQueue.Peek();

                        //�߰��� ����[2��]
                        grabbable.enabled = false;

                        //������ �´ٸ�
                        if (hd.GetIsLeft() == route.GetIsLeft())
                        {
                            roadQueue.Dequeue();
                            drives[0].driveAble = true;

                            //�߰��� ����[2��]
                            handle.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                            grabbable.enabled = true;
                        }

                        //������ Ʋ���ٸ�
                        else
                        {
                            transform.position = route.GetPosition();
                            transform.localRotation = Quaternion.Euler(route.GetRotation());

                            //��� able false,true
                            //grabbable.enabled = false;

                            //�ڵ� �ʱ�ȭ
                            handle.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

                            //able true
                            //grabbable.enabled = true;

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


    //�ڵ� ����� ����� �ϴ� �뵵 [�ȳ���]
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

    #endregion

    #region UI
    IEnumerator Show_UI_Chapter()
    {
        drives[0].driveAble = false; //�ڵ� ������ �ȵ�
        drives[1].driveAble = false; //�ڵ� ������ �ȵ�

        //�ڵ� ��Ƶ� �ȵǴ� ���
        yield return new WaitForSeconds(showChapterTime);

        chapterExplanation.SetActive(false);//UI ����
        navUI.SetActive(true);
        drives[0].driveAble = true; //�ڵ� ��Ƶ� ��
        grabbable.enabled = true;
        //
    }
    IEnumerator Show_UI_Guide()
    {
        guideUI.SetActive(true);
        yield return new WaitForSeconds(showGuideTime);

        //UI ����
        guideUI.SetActive(false);
        //�ڵ� ��Ƶ� ��
        grabbable.enabled = true;
        drives[1].driveAble = true; //�ڵ� ��Ƶ� ��

    }

    IEnumerator NextScene()
    {
        //�ڵ� ��Ƶ� �ȵǴ� ���
        drives[1].driveAble = false; //�ڵ� ������ �ȵ�
        drives[0].driveAble = false; //�ڵ� ������ �ȵ�

        navUI.SetActive(false);
        nextUI.SetActive(true);
        yield return new WaitForSeconds(showChapterTime);

        //UI ����
        nextUI.SetActive(false);

        loaderScene.GoScene3();

    }


    #endregion
}
