using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightDrive : Drive
{
    public float DuringTime { get; set; } //고정된 값
    public bool isDrive { get; set; } //false

    public bool driveAble { get; set; } //true


    //생성자
    public StraightDrive()
    {
        DuringTime = 2f;
        isDrive = false;
        driveAble = true;
    }
    public bool StartDrive(bool isHandle, float rotateY)
    {
        //드라이브중이 아니지만/ 드라이브가능한 상태고/ 핸들을 잡았다면
        if (!isDrive && driveAble && isHandle)
        {
            isDrive = true;
            driveAble = false;
            return true;
        }
        return false;

    }

    public bool Action(float driveTime)
    {
        if (DuringTime <= driveTime)
        {
            isDrive = false;
            return true;
        }
        return false;
    }
}
