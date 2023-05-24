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
        //35~180 || 180~325
        if (!isDrive && driveAble && isHandle && (35 <= rotateY && rotateY <= 325))
        {
            isDrive = true;
            driveAble = false;
            //Debug.Log("�߾� : " + rotateY);

            //�ڵ��� ���� ���� + - 35 / 35 ���ָ� �Ѿ��rotate
            if (180 <= rotateY) {
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
