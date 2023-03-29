using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 25.0f;
    [SerializeField] private float duringTime = 2f;
    [SerializeField] private SoundManager gameManager;

    bool isDrive = false;
    bool driveAble = true;
    bool isHandle = false;
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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            driveAble = true;
        }
        PlayDrive();
        if (isDrive)
        {
            driveTime += Time.deltaTime;
            Drive_Straight();
            if(duringTime <= driveTime)
            {
                isDrive = false;
                gameManager.DontPlay();

            }
        }
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
    }

    public void setIsHandle(bool isHandle)
    {
        this.isHandle = isHandle;
    }

    //get���� ������ rotate�� ������ -90�� �Ѱų� 90�� ������ ���� ���������� ����
}
