using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
    public GameObject conti;

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
        conti.SetActive(true);
        //SceneManager.LoadScene("Continue");
    }
    public void GoMain() //메인화면 씬으로 이동
    {
        conti.SetActive(false);
        //SceneManager.LoadScene("Start");
    }
    public void RGoMain() //메인화면 씬으로 이동
    {//카툰1에서 처음으로
        SceneManager.LoadScene("Start");
    }
    public void RGoCartoon1() //이어하기의 만화씬으로 이동
    {//반복가능
        SceneManager.LoadScene("Cartoon 1");
    }
    public void GoCartoon() //만화 씬으로 이동
    {
        SceneManager.LoadScene("Cartoon");
    }
    public void GoChap1() //장보기 씬으로 이동
    {
        SceneManager.LoadScene("Chap01");
    }
    public void GoChap2() //운전하기 씬으로 이동
    {
        SceneManager.LoadScene("Chap02");
    }
    public void GoChap3() //요리하기 씬으로 이동
    {
        SceneManager.LoadScene("Chapter03Scene");
    }
    public void GoChap4() //정산(값 정해진) 씬으로 이동
    {
        SceneManager.LoadScene("Chap04 1");
    }
    public void chap4GoMain()
    {
        SceneManager.LoadScene("Start");
    }
    public void Quit() //끝내기 누르면 재생 종료(빌드할 때 코드 수정 필요)
    {
        /*
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        */
        Application.Quit();
    }
}
