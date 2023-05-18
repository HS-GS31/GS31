using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSMeasurement : MonoBehaviour
{
    private float deltaTime = 0f;

    [SerializeField]
    private Text fpstext;

    private bool isShow = true;


    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime)*0.1f;

        if(Input.GetKeyDown(KeyCode.F1))
        {
            isShow = !isShow;
        }
    }

    private void OnGUI()
    {
        if(isShow)
        {
            float ms = deltaTime * 1000f;
            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.} FPS ({1:0.0}ms)", fps, ms);

            fpstext.text = text;
        }

    }
}
