using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 pos = new Vector3(-0.516f, 1.35f, -1.2f);
    void Start()
    {
        gameObject.transform.position = pos;   
    }
}
