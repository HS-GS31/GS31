using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Drive
{
    public float DuringTime { get; set; } //고정된 값
    public bool isDrive { get; set; } //false

    public bool driveAble { get; set; } //true
    public bool StartDrive(bool isHandle, float rotateY);
    public bool Action(float driveTime); //드라이브 액션 [나중에 재정의]
}

/*
 * 드라이브
Drive함수
  - 버튼 디자인패턴?

ㅇ
  - isLeft
  - Handle LEft / Right

직진, 왼쪽오른쪽

true면 브금재생, 대신 isRate가 false면 브금off

근데 만약 이동 함수는 어떻게 수정하지

 */
