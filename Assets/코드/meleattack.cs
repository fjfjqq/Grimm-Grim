using UnityEngine;

public class meleattack : MonoBehaviour
{
    public Transform turnpoint1;
    public Transform turnpoint2;

    public float nonamlspeed = 2f;

    public float distance = 5f;
    public float attackrange = 1f;
    public LayerMask playerlayer;

    public float chasespeed = 4f;

    public GameObject attackhitbox;
    public int damge = 1;

    private Rigidbody2D rb;
    private int facedir = 1;
    private bool isattack = false;
    private Transform turntarget;
    private Animator animator;

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

            transform.localScale = new Vector3(facedir, 1, 1); //x축 크기 변경

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
        if (Vector2.Distance(transform.position, turntarget.position) < 0.5f)
        {
            if(turntarget == turnpoint2)
            {
                turntarget = turnpoint1;
            }
            else
            {
                turntarget = turnpoint2;
            }
        }
    }

    void Startattack()
    {
        isattack = true;
        rb.linearVelocity = Vector2.zero;
    }


}
