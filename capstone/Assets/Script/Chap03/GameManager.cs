using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject WarningText;
    public GameObject ChapIntro;
    private void Start()
    {
        WarningText = this.gameObject.transform.GetChild(0).gameObject;
    }

    public void Warn()
    {
        WarningText.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<WarnText>().setActive();
    }
}
