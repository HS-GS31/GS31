using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CartMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float moveTime;
    public bool isGrab;
    public GameObject player;
    public GameObject cart;

    // Start is called before the first frame update
    void Start()
    {
        isGrab = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MoveCoroutine()
    {
        float timer = 0f;
        while(timer < moveTime)
        {
            cart.transform.Translate(Vector3.right * speed * Time.deltaTime);
            player.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public void GrabHandle()
    {
        if (!isGrab)
        {
            Debug.Log("Grab Handle");
            isGrab = true;

            StartCoroutine(MoveCoroutine());
        }
    }

    public void NotGrabHandle()
    {
        Debug.Log("Not Grab Handle");
        isGrab = false;
    }
}
