using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalDrive : Drive
{
    public float DuringTime { get; set; } //고정된 값
    public float DriveTime { get; set; } //0~3
    public bool isDrive { get; set; } //false
    public bool driveAble { get; set; } //true

    private bool isLeft = false;
    public HorizontalDrive()
    {
        DuringTime = 3f;
        isDrive = false;
        driveAble = true;
    }

    public bool StartDrive(bool isHandle, float rotateY)
    {
        if (!isDrive && driveAble && isHandle && (rotateY <= -0.69 || 0.69 <= rotateY))
        {
            isDrive = true;
            driveAble = false;

            //핸들을 잡은 상태 + -90 / 90 범주를 넘어서면rotate [90 => 70으로 바꿔야함]
            if (rotateY <= -0.69) {
                isLeft = true;
            }
            else {
                isLeft = false;
            }

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

    public bool GetIsLeft()
    {
        return isLeft;
    }
    

}
