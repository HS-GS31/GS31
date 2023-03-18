using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    int state;     //0: 대기, 11: walk, 22: order, 33은 음식대기(idle), 44 : take away, 55 : 인사후 퇴장.
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
        this.TextBubble = gameObject.transform.GetChild(7).gameObject;  //7번 인덱스의 자식오브젝트가 textBubble임.
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
        
        if (state == 1)  //음식 주문하러 가기
        {
            this.transform.position = Vector3.MoveTowards(transform.position, orderPos, speed * Time.deltaTime);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * speed);
        }
        if ((transform.position == orderPos)&&(state != 3)) {
            state = 2;        //이동 완료시 state 1로 갱신하여 주문하기
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(truckPos - orderPos), Time.deltaTime * 1.5f);
            this.customerManager.GetComponent<CustomerManager>().setCustomer(this.gameObject);
            //도착하면 말풍선 보이기
            this.TextBubble.SetActive(true);
        }
        if (state == 3)     //음식 받고 퇴장.
        {
            this.TextBubble.SetActive(false);
            this.transform.position = Vector3.MoveTowards(transform.position, desPos, speed * Time.deltaTime);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(back_dir), Time.deltaTime * 2);
        }

        //desPos에 도착시 Destroy...
        if(this.transform.position == desPos)
        {
            Destroy(this);
        }
        this.animator.SetInteger("State", state);
    }

    //상태 갱신용 함수
    public void setStat(int stat)
    {
        this.state = stat;
    }
    public int getStat()
    {
        return state;
    }
}