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

        //메뉴를 보여줄 말풍선 설정
        this.TextBubble = gameObject.transform.GetChild(7).gameObject;  //7번 인덱스의 자식오브젝트가 textBubble임.
        this.order = this.customerManager.GetComponent<MenuManager>().getRandomFood();
        this.order.SetActive(false);       //처음은 안보이게
        this.order.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        this.state = 1;

        //이모티콘
        this.emoji_angry = TextBubble.transform.GetChild(0).gameObject;
        this.emoji_smile = TextBubble.transform.GetChild(1).gameObject;
        this.emoji_angry.SetActive(false);
        this.emoji_smile.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 이모티콘 위치 조정
        this.emoji_angry.transform.position = TextBubble.transform.position;
        this.emoji_smile.transform.position = TextBubble.transform.position;

        //음식 말풍선 위치 수시로 조정
        this.order.transform.position = TextBubble.transform.position;
        this.order.transform.Rotate(new Vector3(0, 15 * Time.deltaTime, 0));
        Vector3 dir = orderPos - transform.position;
        Vector3 back_dir = desPos - transform.position;

        //음식 주문하러 가기
        if (state == 1)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, orderPos, speed * Time.deltaTime);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * speed);

            //도착 직전 미세 부분에서 메뉴 띄우기
            if(this.transform.position.x > -2.85f && this.transform.position.x < orderPos.x)
            {
                this.order.SetActive(true);
            }
        }

        //이동 완료시 동작
        if ((transform.position == orderPos) && (state != 3)) {
            state = 2;        //이동 완료시 state 1로 갱신하여 주문하기
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(truckPos - orderPos), Time.deltaTime * 1.5f);
        }

        //음식 받고 퇴장.
        if (state == 3)     
        {
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
        this.order.SetActive(true);                 //메뉴 다시 보이기
        this.emoji_smile.SetActive(false);          //이모지 다시 가리기
        this.emoji_angry.SetActive(false);          //이모지 다시 가리기
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