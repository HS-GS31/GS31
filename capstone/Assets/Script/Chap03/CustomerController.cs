using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    int state;     //0: ���, 11: walk, 22: order, 33�� ���Ĵ��(idle), 44 : take away, 55 : �λ��� ����.
    float speed = 0.7f;
    Vector3 orderPos;
    private Vector3 truckPos;
    private Animator animator;
    private Vector3 desPos;
    private GameObject order;
    GameObject customerManager;
    GameObject TextBubble;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        this.customerManager = GameObject.Find("CustomerManager");
        this.truckPos = new Vector3(0, 0, 0);
        this.desPos = new Vector3(-2.8f, 0, 4);
        orderPos = new Vector3(-2.8f, 0, -1);
        //�޴��� ������ ��ǳ�� ����
        this.TextBubble = gameObject.transform.GetChild(7).gameObject;  //7�� �ε����� �ڽĿ�����Ʈ�� textBubble��.
        this.order = this.customerManager.GetComponent<MenuManager>().getRandomFood();
        this.order.SetActive(false);       //ó���� �Ⱥ��̰�
        this.order.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        this.state = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //���� ��ǳ�� ��ġ ���÷� ����
        this.order.transform.position = TextBubble.transform.position;
        this.order.transform.Rotate(new Vector3(0, 15 * Time.deltaTime, 0));
        Vector3 dir = orderPos - transform.position;
        Vector3 back_dir = desPos - transform.position;
        
        if (state == 1)  //���� �ֹ��Ϸ� ����
        {
            this.transform.position = Vector3.MoveTowards(transform.position, orderPos, speed * Time.deltaTime);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * speed);
        }
        if ((transform.position == orderPos) && (state != 3)) {
            state = 2;        //�̵� �Ϸ�� state 1�� �����Ͽ� �ֹ��ϱ�
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(truckPos - orderPos), Time.deltaTime * 1.5f);
            //�����ϸ� ��ǳ�� ���̱�
            this.order.SetActive(true);
        }
        if (state == 3)     //���� �ް� ����.
        {
            this.order.SetActive(false);
            this.transform.position = Vector3.MoveTowards(transform.position, desPos, speed * Time.deltaTime);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(back_dir), Time.deltaTime * 2);
        }

        //desPos�� ������ Destroy...
        if(this.transform.position == desPos)
        {
            Destroy(this.gameObject);
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