using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalDrive : Drive
{
    public float DuringTime { get; set; } //������ ��
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

            //�ڵ��� ���� ���� + -90 / 90 ���ָ� �Ѿ��rotate [90 => 70���� �ٲ����]
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
