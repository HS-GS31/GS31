using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 25.0f;
    [SerializeField] private float duringTime = 2f;

    bool isDrive = false;
    float driveTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && isDrive == false)
        {
            driveTime = 0;
            isDrive = true;
        }
        if(isDrive)
        {
            driveTime += Time.deltaTime;
            Drive_Straight();
            if(duringTime <= driveTime)
            {
                isDrive = false;
            }
            
        }
    }

    void Drive_Straight()
    {
        Debug.Log("Á÷Áø");
        transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
    }
}
