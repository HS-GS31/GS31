using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
    public void InvGoContinue() //이어하기 씬으로 이동
    {
        Invoke("GoContinue", 1.8f);
    }
    public void InvGoCartoon() //만화 씬으로 이동
    {
        Invoke("GoCartoon", 1.8f);
    }
    public void InvGoMain() //메인화면 씬으로 이동
    {
        Invoke("GoMain", 1.8f);
    }


    public void GoContinue() //이어하기 씬으로 이동
    {
        SceneManager.LoadScene("Continue");
    }
    public void GoMain() //메인화면 씬으로 이동
    {
        SceneManager.LoadScene("Start");
    }
    public void GoCartoon() //만화 씬으로 이동
    {
        SceneManager.LoadScene("Cartoon");
    }
    public void GoChap1() //만화 씬으로 이동
    {
        SceneManager.LoadScene("Start");
    }
    public void Quit() //끝내기 누르면 재생 종료(빌드할 때 코드 수정 필요)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
