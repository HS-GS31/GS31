using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    int state;     //0: 대기, 11: walk, 22: order, 33은 음식대기(idle), 44 : take away, 55 : 인사후 퇴장.
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
        //메뉴를 보여줄 말풍선 설정
        this.TextBubble = gameObject.transform.GetChild(7).gameObject;  //7번 인덱스의 자식오브젝트가 textBubble임.
        this.order = this.customerManager.GetComponent<MenuManager>().getRandomFood();
        this.order.SetActive(false);       //처음은 안보이게
        this.order.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        this.state = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //음식 말풍선 위치 수시로 조정
        this.order.transform.position = TextBubble.transform.position;
        this.order.transform.Rotate(new Vector3(0, 15 * Time.deltaTime, 0));
        Vector3 dir = orderPos - transform.position;
        Vector3 back_dir = desPos - transform.position;
        
        if (state == 1)  //음식 주문하러 가기
        {
            this.transform.position = Vector3.MoveTowards(transform.position, orderPos, speed * Time.deltaTime);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * speed);
        }
        if ((transform.position == orderPos) && (state != 3)) {
            state = 2;        //이동 완료시 state 1로 갱신하여 주문하기
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(truckPos - orderPos), Time.deltaTime * 1.5f);
            //도착하면 말풍선 보이기
            this.order.SetActive(true);
        }
        if (state == 3)     //음식 받고 퇴장.
        {
            this.order.SetActive(false);
            this.transform.position = Vector3.MoveTowards(transform.position, desPos, speed * Time.deltaTime);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(back_dir), Time.deltaTime * 2);
        }

        //desPos에 도착시 Destroy...
        if(this.transform.position == desPos)
        {
            Destroy(this.gameObject);
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