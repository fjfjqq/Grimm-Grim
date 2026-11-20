using UnityEngine;

public class meleattack : MonoBehaviour
{
    public Transform turnpoint1;
    public Transform turnpoint2;

    public float nonamlspeed = 2f;

    public float distance = 5f;
    public float attackrange = 2f;
    public LayerMask playerlayer;

    public float chasespeed = 4f;

    public GameObject attackhitbox;
    public int damge = 1;
    private float attackcooltime = 0.5f;
    private float lastattacktime = 0f;

    private Rigidbody2D rb;
    private int facedir = 1;
    private bool isattack = false;
    private Transform turntarget;
    private Animator animator;

    private bool waiting = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        turntarget = turnpoint2;
        attackhitbox.SetActive(false);
        
    }

    private void Update()
    {
        if (isattack == true)
        {
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * facedir, distance, playerlayer); //어디를 보고 있는지에 따라 레이케스트 발사 거리를 바꿈

        if (hit.collider != null)
        {
            float dirplayer = hit.transform.position.x - transform.position.x; //얼마나 떨어져있는지 구하고

            if (dirplayer > 0)
            {
                facedir = 1; //플레이어의 현재 위치에 따라서 
            }
            else
            {
                facedir = -1; //왼쪽을 볼지 오른쪽을 볼지 결정
            }

            transform.localScale = new Vector3(-facedir, 1, 1); //x축 크기 변경

            if(hit.distance < attackrange)
            {
                Startattack();
            }
            else
            {
                rb.linearVelocity = new Vector2(facedir * chasespeed, rb.linearVelocity.y);
            }
        }
        else
        {
            Nomal();
        }
    }
    void Nomal()
    {
        if(waiting == true)
        {
            return;
        }

        if (Vector2.Distance(transform.position, turntarget.position) < 0.5f)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            animator.SetBool("walk", false);
            StartCoroutine(Waitandturn());
            return;
        }

        float direcition = turntarget.position.x - transform.position.x;

        if (direcition > 0)
        {
            facedir = 1;
        }
        else
        {
            facedir = -1;
        }

        transform.localScale = new Vector3(-facedir, 1, 1); //보고 있는 방향으로 스프라이트 뒤집기
        rb.linearVelocity = new Vector2(facedir * nonamlspeed, rb.linearVelocity.y); //방향으로 이동
        animator.SetBool("walk", true); //걷기 애니메이션 재생
    }

    void Startattack()
    {
        if(Time.time < lastattacktime + attackcooltime)
        {
            return;
        }

        isattack = true;
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("walk", false);
        animator.SetTrigger("attack");
    }

    public void dead()
    {
        isattack = true;
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("walk", false);
        animator.SetTrigger("dead");
    }

    public void hitboxon()
    {
        attackhitbox.SetActive(true);
    }

    public void hitboxoff()
    {
        attackhitbox.SetActive(false);
    }

    public void endattack()
    {
        isattack = false;
        lastattacktime = Time.time;
    }

    System.Collections.IEnumerator Waitandturn()
    {
        waiting = true;

        yield return new WaitForSeconds(2f); //2초동안 멈추기

        if (turntarget == turnpoint2) //2초후에 실행할것
        {
            turntarget = turnpoint1;
        }
        else
        {
            turntarget = turnpoint2;
        }

        waiting = false;
    }

}
