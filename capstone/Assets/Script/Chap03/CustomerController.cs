using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    int state;     //0: ���, 11: walk, 22: order, 33�� ���Ĵ��(idle), 44 : take away, 55 : �λ��� ����.
    float speed = 0.7f;
    public Vector3 orderPos = new Vector3(-2.8f, 0, -1);
    private Vector3 truckPos;
    private Animator animator;
    private Vector3 desPos;
    GameObject customerManager;
    GameObject TextBubble;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        this.customerManager = GameObject.Find("CustomerManager");
        this.truckPos = new Vector3(0, 0, 0);
        this.desPos = new Vector3(-5, 0, 4);
        this.TextBubble = gameObject.transform.GetChild(7).gameObject;  //7�� �ε����� �ڽĿ�����Ʈ�� textBubble��.
        this.TextBubble.SetActive(false);
        //this.orderPos.transform.localEulerAngles = new Vector3(0, 90, 0);
        //this.orderPos.transform.position = new Vector3(-2.8f, 0, -1);
        this.state = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = orderPos - transform.position;
        Vector3 back_dir = desPos - transform.position;
        
        if (state == 1)  //���� �ֹ��Ϸ� ����
        {
            this.transform.position = Vector3.MoveTowards(transform.position, orderPos, speed * Time.deltaTime);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * speed);
        }
        if ((transform.position == orderPos)&&(state != 3)) {
            state = 2;        //�̵� �Ϸ�� state 1�� �����Ͽ� �ֹ��ϱ�
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(truckPos - orderPos), Time.deltaTime * 1.5f);
            this.customerManager.GetComponent<CustomerManager>().setCustomer(this.gameObject);
            //�����ϸ� ��ǳ�� ���̱�
            this.TextBubble.SetActive(true);
        }
        if (state == 3)     //���� �ް� ����.
        {
            this.TextBubble.SetActive(false);
            this.transform.position = Vector3.MoveTowards(transform.position, desPos, speed * Time.deltaTime);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(back_dir), Time.deltaTime * 2);
        }

        //desPos�� ������ Destroy...
        if(this.transform.position == desPos)
        {
            Destroy(this);
        }
        this.animator.SetInteger("State", state);
    }

    //���� ���ſ� �Լ�
    public void setStat(int stat)
    {
        this.state = stat;
    }
    public int getStat()
    {
        return state;
    }
}