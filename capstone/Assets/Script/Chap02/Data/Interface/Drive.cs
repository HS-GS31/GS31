using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Drive
{
    public float DuringTime { get; set; } //������ ��
    public bool isDrive { get; set; } //false

    public bool driveAble { get; set; } //true
    public bool StartDrive(bool isHandle, float rotateY);
    public bool Action(float driveTime); //����̺� �׼� [���߿� ������]
}

/*
 * ����̺�
Drive�Լ�
  - ��ư ����������?

��
  - isLeft
  - Handle LEft / Right

����, ���ʿ�����

true�� ������, ��� isRate�� false�� ���off

�ٵ� ���� �̵� �Լ��� ��� ��������

 */
