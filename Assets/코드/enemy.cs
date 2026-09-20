using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.Rendering.DebugUI;

public class enemy : MonoBehaviour
{
    public int maxhp;
    public int nowhp;
    public bool dead;
    public int damage = 1;
    public float movespeed;
    public float chaserange;
    public float fadetime;

    public LayerMask Player;
    public LayerMask Ground;
    private Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer render;
    public Collider2D collider;
    private bool chaseplayer = false;
    private bool checkplayer = false;
    private bool surprised = false;
    private bool fading = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nowhp = maxhp;
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if(dead == true)
        {
            return;
        }

        if (nowhp <= 0)
        {
            Dead();
            return;
        }

        checkplayer = false;

        Collider2D found = Physics2D.OverlapCircle(transform.position, chaserange, Player); // 현재 들어간 enemy를 기준으로 추격범위만큼의 원을 만들고 그 안에 있는 플레이어 콜라이더를 찾고 만약에 플레이어가 있다면 값을 없으면 null값을 반환 


        if (found != null)
        {
            Vector2 check = (found.transform.position - transform.position).normalized; //플레이어의 위치에서 자신의 위치를 뺌으로써 x와 y좌표 방향을 잡아준다. 노말라이즈는 거리상관없이 이동속도 일정하게 잡아주기 위해서

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
            animator.SetTrigger("");
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
            animator.SetBool("nomal", true);
        }
    }

    void Dead()
    {
        dead = true;

        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;
        collider.enabled = false;

        animator.SetBool("nomal", false);
        animator.SetTrigger("dead");
    }

    public void Startfade()
    {
        if (fading == true)
        {
            return;
        }

        fading = true;
        StartCoroutine(Fadeout());
    }
    IEnumerator Fadeout()
    {

        Color fadecolor = render.color;
        float time = 1f;

        while(time > 0f)
        {
            time -= 0.003f;
            
            render.color = new Color(1, 1, 1, time);
            yield return null;

        }

        Destroy(gameObject);
    }

}


