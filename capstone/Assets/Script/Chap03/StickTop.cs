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
        //����� ���� ������ ��,
        other.GetComponent<IngredientController>().setIsSticTop();
        Debug.Log("��ܰ� �浹");
        //����� ���� ���� ������.
    }
}
