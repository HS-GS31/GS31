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
    //private GameObject sender;
    private GameObject order;
    public GameObject emoji_smile;
    public GameObject emoji_angry;
    GameObject customerManager;
    GameObject TextBubble;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        this.customerManager = GameObject.Find("CustomerManager");
        //this.sender = GameObject.Find("SenderPlate");
        this.truckPos = new Vector3(0, 0, 0);
        this.desPos = new Vector3(-2.8f, 0, 6);
        orderPos = new Vector3(-2.8f, 0, -1);

        //�޴��� ������ ��ǳ�� ����
        this.TextBubble = gameObject.transform.GetChild(7).gameObject;  //7�� �ε����� �ڽĿ�����Ʈ�� textBubble��.
        this.order = this.customerManager.GetComponent<MenuManager>().getRandomFood();
        this.order.SetActive(false);       //ó���� �Ⱥ��̰�
        this.order.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        this.state = 1;

        //�̸�Ƽ��
        this.emoji_angry = TextBubble.transform.GetChild(0).gameObject;
        this.emoji_smile = TextBubble.transform.GetChild(1).gameObject;
        this.emoji_angry.SetActive(false);
        this.emoji_smile.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // �̸�Ƽ�� ��ġ ����
        this.emoji_angry.transform.position = TextBubble.transform.position;
        this.emoji_smile.transform.position = TextBubble.transform.position;

        //���� ��ǳ�� ��ġ ���÷� ����
        this.order.transform.position = TextBubble.transform.position;
        this.order.transform.Rotate(new Vector3(0, 15 * Time.deltaTime, 0));
        Vector3 dir = orderPos - transform.position;
        Vector3 back_dir = desPos - transform.position;

        //���� �ֹ��Ϸ� ����
        if (state == 1)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, orderPos, speed * Time.deltaTime);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * speed);

            //���� ���� �̼� �κп��� �޴� ����
            if(this.transform.position.x > -2.85f && this.transform.position.x < orderPos.x)
            {
                this.order.SetActive(true);
            }
        }

        //�̵� �Ϸ�� ����
        if ((transform.position == orderPos) && (state != 3)) {
            state = 2;        //�̵� �Ϸ�� state 1�� �����Ͽ� �ֹ��ϱ�
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(truckPos - orderPos), Time.deltaTime * 1.5f);
        }

        //���� �ް� ����.
        if (state == 3)     
        {
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

    public void setEmoji(bool res)
    {
        this.order.SetActive(false);
        if (res)
        {
            this.emoji_smile.SetActive(true);
            Invoke("hideEmoji", 1);
        }
        else
        {   
            this.emoji_angry.SetActive(true);
            Invoke("showMenu", 1);
        }
    }
    private void showMenu()
    {
        this.order.SetActive(true);                 //�޴� �ٽ� ���̱�
        this.emoji_smile.SetActive(false);          //�̸��� �ٽ� ������
        this.emoji_angry.SetActive(false);          //�̸��� �ٽ� ������
    }
    private void hidEmoji()
    {
        this.emoji_smile.SetActive(false);
    }
    public GameObject getOrder()
    {
        return order;
    }
}