using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
    public void InvGoContinue() //�̾��ϱ� ������ �̵�
    {
        Invoke("GoContinue", 1.8f);
    }
    public void InvGoCartoon() //��ȭ ������ �̵�
    {
        Invoke("GoCartoon", 1.8f);
    }
    public void InvGoMain() //����ȭ�� ������ �̵�
    {
        Invoke("GoMain", 1.8f);
    }


    public void GoContinue() //�̾��ϱ� ������ �̵�
    {
        SceneManager.LoadScene("Continue");
    }
    public void GoMain() //����ȭ�� ������ �̵�
    {
        SceneManager.LoadScene("Start");
    }
    public void GoCartoon() //��ȭ ������ �̵�
    {
        SceneManager.LoadScene("Cartoon");
    }
    public void GoChap1() //��ȭ ������ �̵�
    {
        SceneManager.LoadScene("Start");
    }
    public void Quit() //������ ������ ��� ����(������ �� �ڵ� ���� �ʿ�)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
