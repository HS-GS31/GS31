using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightDrive : Drive
{
    public float DuringTime { get; set; } //������ ��
    public bool isDrive { get; set; } //false

    public bool driveAble { get; set; } //true


    //������
    public StraightDrive()
    {
        DuringTime = 2f;
        isDrive = false;
        driveAble = true;
    }
    public bool StartDrive(bool isHandle, float rotateY)
    {
        //����̺����� �ƴ�����/ ����̺갡���� ���°�/ �ڵ��� ��Ҵٸ�
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
