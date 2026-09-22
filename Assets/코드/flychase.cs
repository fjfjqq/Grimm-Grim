using UnityEngine;

public class flychase : MonoBehaviour
{
    public LayerMask Player;
    public LayerMask Ground;
    public float movespeed;
    public float chaserange;
    private bool chaseplayer = false;
    private bool checkplayer = false;
    private bool surprised = false;
    private Vector2 check = Vector2.zero;
    private Rigidbody2D rb;
    private Animator animator;
    private enemy defalut;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        defalut = GetComponent<enemy>();
    }

    private void Update()
    {
        if(defalut.dead == true)
        {
            return;
        }

        checkplayer = false;

        Collider2D found = Physics2D.OverlapCircle(transform.position, chaserange, Player); // 현재 들어간 enemy를 기준으로 추격범위만큼의 원을 만들고 그 안에 있는 플레이어 콜라이더를 찾고 만약에 플레이어가 있다면 값을 없으면 null값을 반환 


        if (found != null)
        {

            check = (found.transform.position - transform.position).normalized; //플레이어의 위치에서 자신의 위치를 뺌으로써 x와 y좌표 방향을 잡아준다. 노말라이즈는 거리상관없이 이동속도 일정하게 잡아주기 위해서

            float longcheck = Vector2.Distance(transform.position, found.transform.position); //플레이어의 현재 위치를 계산하는것

            RaycastHit2D wallcheck = Physics2D.Raycast(transform.position, check, longcheck, Ground); //체크가 활성화됬을때 플레이어의 위치로 레이캐스트를 쏴서 확인

            if (wallcheck.collider == null)
            {
                checkplayer = true;
            }
        }

        if (checkplayer == true && chaseplayer == false && surprised == false)
        {
            surprised = true;
            animator.SetTrigger("surprised");
        }

        if (chaseplayer == true && checkplayer == true)
        {
            rb.linearVelocity = check * movespeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
    public void Chase()
    {
        surprised = false;

        if (checkplayer)
        {
            chaseplayer = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            chardata player = collision.gameObject.GetComponent<chardata>();
            if(player != null)
            {
                player.damagesystem();
            }
        }
    }
}
