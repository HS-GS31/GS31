using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickTop : MonoBehaviour
{
    Collider coll;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //상단을 통해 들어왔을 때,
        other.GetComponent<IngredientController>().setIsSticTop();
        Debug.Log("상단과 충돌");
        //상단을 통해 빠져 나갈때.
    }
}
