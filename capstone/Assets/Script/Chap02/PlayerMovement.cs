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
    //왼쪽은 false, 오른쪽은 true

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
        //Debug.Log("직진");
        transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
    }


    private void PlayDrive()
    {
        //직진하고 핸들 돌리지 않는 이상 실행 X
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

    //get으로 가져온 rotate의 값에서 -90을 넘거나 90을 넘으면 왼쪽 오른쪽으로 가짐
}
