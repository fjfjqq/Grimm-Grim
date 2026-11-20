using UnityEngine;
using System.Collections;
using System;

public class rangerenemy : MonoBehaviour
{
    public Transform turnpoint1;
    public Transform turnpoint2;

    public float speed = 2f;
    public float attackrange = 8f;
    public LayerMask playerlayer;
    public LayerMask groundlayer;

    public Transform attackpoint;
    public float attackcooltime = 2f;

    private Rigidbody2D rb;
    private Animator animator;
    private int facedir = 1;
    private bool isattack = false;
    private bool waiting = false;
    private Transform turntarget;

    private float lastattacktime = -10f;
    private Vector2 playerwhich;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        turntarget = turnpoint2;
    }
    private void Update()
    {
        if(isattack == true)
        {
            return;
        }

        bool checkplayer = false;
        Transform foundplayer = null;
        Collider2D found = Physics2D.OverlapCircle(transform.position, attackrange, playerlayer);

        if(found != null)
        {
            Vector2 check = (found.transform.position - transform.position).normalized;
            float longcheck = Vector2.Distance(transform.position, found.transform.position);

            RaycastHit2D wallcheck = Physics2D.Raycast(transform.position, check, longcheck, groundlayer);

            if(wallcheck.collider == null)
            {
                checkplayer = true;
                foundplayer = found.transform;
            }
        }

        if(checkplayer == true && Time.time > lastattacktime + attackcooltime)
        {
            Startattack(foundplayer);
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

        if(Vector2.Distance(transform.position, turntarget.position) < 0.5f)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            animator.SetBool("walk", false);
            StartCoroutine(Waitattackturn());
            return;
        }

        float direction = turntarget.position.x - transform.position.x;

        if(direction > 0)
        {
            facedir = 1;
        }
        else
        {
            facedir = -1;
        }

        transform.localScale = new Vector3(-facedir, 1, 1);
        rb.linearVelocity = new Vector2(facedir * speed, rb.linearVelocity.y);
        animator.SetBool("walk", true);
    }
    void Startattack(Transform target)
    {
        isattack = true;
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("walk", false);

        playerwhich = (target.position - attackpoint.position).normalized;

        if(target.position.x > transform.position.x)
        {
            facedir = 1;
        }
        else
        {
            facedir = -1;
        }
        transform.localScale = new Vector3(-facedir, 1, 1);

        animator.SetTrigger("attack");
    }
    public void shoot()
    {
        float baseangel = Mathf.Atan2(playerwhich.y, playerwhich.x) * Mathf.Rad2Deg; //Atan2로 뭐 왼쪽에 있는지 오른쪽에 있는지 주는데 이게 라디안이라는 단위로 줌 이걸 내가 아는 도로 바꾸려면 rad2deg곱해야한다 해서 곱하기

        for(int i = 0; i < 3; i++)
        {
            float offset = 15f * i;
            float angel = (baseangel - 15f) + offset; //-15도부터 정방향 15도로 쏘게

            float duch = angel * Mathf.Deg2Rad; //다시 라디안으로 바꾸기 삼각함수는 라디안이니깐(내가 이거 때문에 거의 3시간은 씀)
            Vector2 dir = new Vector2(Mathf.Cos(duch), Mathf.Sin(duch)); //cos이랑 sin으로 날아갈 방향 구하기

            GameObject gob = bulletsaver.instance.Reroald(); //쉬었음 총알 데려와
            gob.transform.position = attackpoint.position; //총구까지 옮기고
            gob.SetActive(true); //총알 활성화

            bullet bullet = gob.GetComponent<bullet>(); //날아갈 방향
            bullet.setup(dir);

        }
    }
    public void endattack()
    {
        isattack = false;
        lastattacktime = Time.time;
    }
    public void dead()
    {
        isattack =true;
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("walk", false);
        animator.SetTrigger("dead");
    }

    IEnumerator Waitattackturn()
    {
        waiting = true;
        yield return new WaitForSeconds(2f);

        if(turntarget == turnpoint1)
        {
            turntarget = turnpoint2;
        }
        else
        {
            turntarget = turnpoint1;
        }

        waiting = false;
    }
}
