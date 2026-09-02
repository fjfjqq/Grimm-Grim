using JetBrains.Annotations;
using System;
using Unity.VisualScripting;
using UnityEngine;
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
    bool isattack = false; //공격중에 멈추기 위한 장치

    public int maxhp = 6;
    public int nowhp = 6;

    public int nowmoney = 0;
    bool nodamage = false;
    public float lasthit = -99f;
    public float nodamagetime = 0.8f;

    public float coyotetime = 0.1f;
    public float coyotetimer = 0f;

    public bool stepground;

    public GameObject Inventory;
    public GameObject Escmain;

    public Animator animator;
    public Rigidbody2D rb; //물리엔진 담아놓을 변수 정하기

    public Transform groundCheck; //그라운드 체크에 위치값 담는 용도
    public float groundchecksize = 0.2f; //그라운드 체크할 원 크기
    public LayerMask groundcheck; // 그라운드만 감지하는 마스크용

    public Weapon[] allweapon = new Weapon[4];
    public void damagesystem()
    {
        if (nodamage)
        {
            return;
        }

        if (isattack)
        {
            return; //공격중이면 애니메이션 못건들이게
        }

        nowhp = nowhp - 1;
        nodamage = true;
        lasthit = Time.time;

        if (nowhp <= 0)
        {
            Debug.Log("죽음");
        }
    }

    void ChangeAnimation(string animationname)
    {
        animator.SetBool("iswalking", false);
        animator.SetBool("isruning", false);
        animator.SetBool("isjumping", false);
        animator.SetBool("isfalling", false);
        animator.SetBool("playerstand", false);

        animator.SetBool(animationname, true);
    }

    void ReturnAnimation()
    {
        if (dashing)
        {
            return;
        }


        if (rb.linearVelocity.y > 0.1f && stepground == false)
        {
            ChangeAnimation("isjumping");
        }
        else if (rb.linearVelocity.y < -0.1f && stepground == false)
        {
            ChangeAnimation("isfalling");
        }
        else if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            ChangeAnimation("isruning");
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            ChangeAnimation("iswalking");
        }
        else
        {
            ChangeAnimation("playerstand");
        }
    }

    public void Attackend()
    {
        isattack = false; //애니메이션에 트리거로 직접 받아올 예정
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //rigidbody2d 의 컴포넌트를 이 오브젝트에서 가져와서 rb(물리엔진)에 저장
        animator = GetComponent<Animator>();

        allweapon[0] = new Weapon(); //
        allweapon[0].weaponename = "기본 검"; //무기 이름 
        allweapon[0].damage = 10f; //무기 데미지
        allweapon[0].attackspeed = 1.0f; //무기 공격속도

        allweapon[1] = new Weapon();
        allweapon[1].weaponename = "대검"; //무기 이름
        allweapon[1].damage = 20f; //무기 데미지
        allweapon[1].attackspeed = 1.5f; //무기 공격속도

        allweapon[2] = new Weapon();
        allweapon[2].weaponename = "단검"; //무기 이름
        allweapon[2].damage = 3f; //무기 데미지
        allweapon[2].attackspeed = 0.3f; //무기 공격속도

        allweapon[3] = new Weapon();
        allweapon[3].weaponename = "배트"; //무기 이름
        allweapon[3].damage = 12f; //무기 데미지
        allweapon[3].attackspeed = 1.4f; //무기 공격속도

        weaponeslot[0] = allweapon[0]; //처음에 기본적인 검은 가지고 있으므로 한칸 채워두기
        weaponeslot[1] = null; //나머지는 나중에 얻으니깐 비워두기
        weaponeslot[2] = null; //나머지는 나중에 얻으니깐 비워두기

        nowweapon = weaponeslot[0]; //현재 시작 할때 무기는 1번에만 들어가있으므로 정해주고 시작

        saveload charload = FindAnyObjectByType<saveload>(); // 씬에서 세이브 로드 붙은거 오브젝트 내가 만든 차데이터에 담아주기
        if (charload != null) //찾았는지 체크
        {
            charload.Gameload(); //찾았음 게임로드에서 호출해서 복원
        }

        
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

    public void leftmove()
    {
        Debug.Log("leftmove");
    }

    void Update()
    {
        if (isattack == true)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return; //공격중엔 아래 전부다 스킵
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Inventory.SetActive(!Inventory.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Escmain.SetActive(!Escmain.activeSelf);
        }

        if(nodamage && Time.time > lasthit + nodamagetime)
        {
            nodamage = false; //
        }

        if (stepground)
        {
            coyotetimer = coyotetime;
        }
        else
        {
            coyotetimer -= Time.deltaTime;
        }



        if(dashing == false)
        {
            if (Input.GetKey(KeyCode.A)) //A키를 누르고 있는동안 작동
            {
                rb.linearVelocity = new Vector2(-movespeed, rb.linearVelocity.y); // X좌표를 현재 speed만큼 이동하고 y좌표는 그대로(왼쪽 이동)
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (Input.GetKey(KeyCode.D)) //D키를 누르고 있는동안 작동
            {
                rb.linearVelocity = new Vector2(movespeed, rb.linearVelocity.y); // y좌표를 현재 -speed만큼 이동하고 y좌표는 그대로(오른쪽 이동)
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else //아무것도 안누르고 있을때
            {
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); //속도값 0으로 변경(미끄러지기 방지)
            }
        }
        
        //서버에서 수정함
        // 또 수정
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

        stepground = Physics2D.OverlapCircle(groundCheck.position, groundchecksize, groundcheck); // 아까 위에서 만들었던 원에 그라운드 레이어 겹치면 stepground를 true로 아니면 flase로

        if (Input.GetKeyDown(KeyCode.Space) && coyotetimer > 0) //stepground가 true일때 스페이스 키를 누르면 실행
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 14); //y좌표를 14로 바꿈

        }

        if (Input.GetKeyUp(KeyCode.Space) && 0 < rb.linearVelocity.y) // 위로 올라가는지 확인하고 스페이스바를 올라가는중에 뗏을때 
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.6f); //상승속도를 60%로 감소시킴
        }

        if (Input.GetMouseButtonDown(0) && Time.time >= lasttime + nowweapon.attackspeed && stepground == true) //마우스 왼쪽 클릭했을때 현재 시간이 마지막 공격시간이랑 현재무기의 공격시간보다 크면 실행
        {
            
            lasttime = Time.time; //마지막 공격시간 갱신
            isattack = true; //공격중이다라는 신호보내기

            if(nowweapon == weaponeslot[0]) //현재 무기가 1번 칸일때 이 안에 있는거 실행
            {
                Debug.Log("1번"); //임의로 들어갈 무기 정보
                animator.SetTrigger("swordattack");
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
        
        ReturnAnimation();
    }
}

[System.Serializable]
public class Weapon
{
    public float attackspeed;
    public float damage;
    public float range;
    public string weaponename;
    
}


