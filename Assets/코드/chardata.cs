using UnityEngine;
using static UnityEditor.PlayerSettings;
public class chardata : MonoBehaviour
{
    public Weapon[] weaponeslot = new Weapon[3]; //배열 만들기, 무기의 위치를 따로 정해두지 않았고 여러개의 무기를 임의로 담을 칸인 weapones slot 배열을 만듬
    public Weapon nowweapon; //현재 무기 위에 배열에서 무기를 교체할거기 때문에 지금 무기 번호(1, 2, 3)를 확인 해주는것

    public float dashpeed = 5f;  //일반 이동속도에 더 해줄것 그렇게 대쉬 이동속도를 만듦

    public float lasttime = -99f; //마지막 공격 시간
    public float swaptime = -99f; //마지막으로 무기를 교체한 시간
    public float swapspeed = 1f; //무기 교체 쿨타임

    public float dashcooltime = 1f; //대쉬 쿨타임
    public float lastdash = -99f; //마지막으로 대쉬한 시간
    public float endofdash = -99f; //대쉬 지속중인시간

    public float movespeed = 5; //이동 스피드
    bool dashing = false; //대쉬중 상태
    bool candash = false; //대쉬가 가능한상태인가


    public Rigidbody2D rb; //물리엔진 담아놓을 변수 정하기

    public Transform groundCheck; //그라운드 체크에 위치값 담는 용도
    public float groundchecksize = 0.2f; //그라운드 체크할 원 크기
    public LayerMask groundcheck; // 그라운드만 감지하는 마스크용

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //rigidbody2d 의 컴포넌트를 이 오브젝트에서 가져와서 rb(물리엔진)에 저장

        Weapon parrysword = new Weapon(); //
        parrysword.weaponename = "기본 검"; //무기 이름
        parrysword.damage = 10f; //무기 데미지
        parrysword.attackspeed = 1.0f; //무기 공격속도

        Weapon largesword = new Weapon();
        largesword.weaponename = "대검"; //무기 이름
        largesword.damage = 20f; //무기 데미지
        largesword.attackspeed = 1.5f; //무기 공격속도

        Weapon shortsword = new Weapon();
        shortsword.weaponename = "단검"; //무기 이름
        shortsword.damage = 3f; //무기 데미지
        shortsword.attackspeed = 0.3f; //무기 공격속도

        Weapon bat = new Weapon();
        bat.weaponename = "배트"; //무기 이름
        bat.damage = 12f; //무기 데미지
        bat.attackspeed = 1.4f; //무기 공격속도

        weaponeslot[0] = parrysword; //처음에 기본적인 검은 가지고 있으므로 한칸 채워두기
        weaponeslot[1] = null; //나머지는 나중에 얻으니깐 비워두기
        weaponeslot[2] = null; //나머지는 나중에 얻으니깐 비워두기

        nowweapon = weaponeslot[0]; //현재 시작 할때 무기는 1번에만 들어가있으므로 정해주고 시작
    }

    void weaponeswap()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && nowweapon != weaponeslot[0] && Time.time >= swaptime + swapspeed && weaponeslot[0] != null) // 1번키를 눌렀을때 현재 무기가 1번이 아니고 현재 시간(게임 진행중 시간)이 스왑타임(마지막으로 스왑한 시간) + 스왑 쿨타임보다 크고 웨폰 슬롯이 널값으로 비어있지 않을때 작동
        {
            nowweapon = weaponeslot[0]; //현재 무기를 1번 웨폰칸에 있는걸로 바꾸고
            swaptime = Time.time; //마지막으로 바꾼 시간 갱신
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && nowweapon != weaponeslot[1] && Time.time >= swaptime + swapspeed && weaponeslot[1] != null) // 2번키를 눌렀을때 현재 무기가 1번이 아니고 현재 시간(게임 진행중 시간)이 스왑타임(마지막으로 스왑한 시간) + 스왑 쿨타임보다 크고 웨폰 슬롯이 널값으로 비어있지 않을때 작동
        {
            nowweapon = weaponeslot[1]; //현재 무기를 2번 웨폰칸에 있는걸로 바꾸고
            swaptime = Time.time; //마지막으로 바꾼 시간 갱신
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && nowweapon != weaponeslot[2] && Time.time >= swaptime + swapspeed && weaponeslot[2] != null) // 3번키를 눌렀을때 현재 무기가 1번이 아니고 현재 시간(게임 진행중 시간)이 스왑타임(마지막으로 스왑한 시간) + 스왑 쿨타임보다 크고 웨폰 슬롯이 널값으로 비어있지 않을때 작동
        {
            nowweapon = weaponeslot[2]; //현재 무기를 3번 웨폰칸에 있는걸로 바꾸고
            swaptime = Time.time; //마지막으로 바꾼 시간 갱신
        }
    }

    void Update()
    {
        if(dashing == false)
        {
            if (Input.GetKey(KeyCode.A)) //A키를 누르고 있는동안 작동
            {
                rb.linearVelocity = new Vector2(-movespeed, rb.linearVelocity.y); // X좌표를 현재 speed만큼 이동하고 y좌표는 그대로(왼쪽 이동)
            }
            else if (Input.GetKey(KeyCode.D)) //D키를 누르고 있는동안 작동
            {
                rb.linearVelocity = new Vector2(movespeed, rb.linearVelocity.y); // y좌표를 현재 -speed만큼 이동하고 y좌표는 그대로(오른쪽 이동)
            }
            else //아무것도 안누르고 있을때
            {
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); //속도값 0으로 변경(미끄러지기 방지)
            }
        }
        

        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            movespeed = 9;
        }
        else
        {
            movespeed = 5;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && candash)
        {
            dashing = true;
            endofdash = Time.time + 0.1f;

            if (Input.GetKey(KeyCode.A))
            {
                rb.linearVelocity = new Vector2(-20, rb.linearVelocity.y);
            }
            else if (Input.GetKey(KeyCode.D)){
                rb.linearVelocity = new Vector2(20, rb.linearVelocity.y);
            }

            candash = false;
            lastdash = Time.time;
        }

        if(dashing && Time.time > endofdash)
        {
            dashing = false;
        }
        if (Time.time > dashcooltime + lastdash)
        {
            candash = true;
        }


        weaponeswap(); //눌렀는지 계속 검사해야하기 때문에 호출

        bool stepground = Physics2D.OverlapCircle(groundCheck.position, groundchecksize, groundcheck); // 아까 위에서 만들었던 원에 그라운드 레이어 겹치면 stepground를 true로 아니면 flase로

        if (Input.GetKeyDown(KeyCode.Space) && stepground) //stepground가 true일때 스페이스 키를 누르면 실행
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 14); //y좌표를 14로 바꿈

        }

        if (Input.GetKeyUp(KeyCode.Space) && 0 < rb.linearVelocity.y) // 위로 올라가는지 확인하고 스페이스바를 올라가는중에 뗏을때 
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.6f); //상승속도를 60%로 감소시킴
        }

        if (Input.GetMouseButtonDown(0) && Time.time >= lasttime + nowweapon.attackspeed) //마우스 왼쪽 클릭했을때 현재 시간이 마지막 공격시간이랑 현재무기의 공격시간보다 크면 실행
        {
            
            lasttime = Time.time; //마지막 공격시간 갱신

            if(nowweapon == weaponeslot[0]) //현재 무기가 1번 칸일때 이 안에 있는거 실행
            {
                Debug.Log("1번"); //임의로 들어갈 무기 정보
            }
            else if(nowweapon == weaponeslot[1]) //현재 무기가 2번 칸일때 이 안에 있는거 실행
            {
                Debug.Log("2번"); //임의으로 들어갈 무기 정보
            }
            else if(nowweapon == weaponeslot[2]) //현재 무기가 3번 칸일때 이 안에 있는거 실행
            {
                Debug.Log("3번"); //임의로 들어갈 무기 정보
            }
        }
        

    }
}

public class Weapon
{
    public float attackspeed;
    public float damage;
    public float range;
    public string weaponename;
    
}

