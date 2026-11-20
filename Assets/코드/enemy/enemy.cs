using System.Collections;
using UnityEngine;


public class enemy : MonoBehaviour
{
    public int maxhp;
    public int nowhp;
    public bool dead = false;
    public int damage = 1;
    private Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer render;
    public Collider2D collider;

    private bool knockbackstop;
    private float knockbackstoptime = 0f;
    public float knockbackcooltime = 0.5f;
    private bool fading = false;

    public bool knockback = false;
    private float knockbacktime = 0f;
    private float dotheknockback = 0.3f;

    private Vector2 lasthit = Vector2.zero;
    private float lastpower = 0f;
    public int money = 10;
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
        if (dead == true)
        {
            return;
        }

        if(knockbackstop == true && Time.time > knockbackcooltime + knockbackstoptime) //넉백 쿨타임
        {
            knockbackstop = false;
        }

        if (knockback)
        {
            if(Time.time > knockbacktime + dotheknockback)
            {
                knockback = false;
            }
            return;
        }

        if (nowhp <= 0)
        {
            Dead();
            return;
        }
        
    }
    void Dead()
    {
        dead = true;

        meleattack enemy = GetComponent<meleattack>(); //그니깐 오브젝트에 이제 넉백을 막을 적의 스크립트가 들어있는지 확인
        if (enemy != null)
        {
            enemy.dead();
        }

        chardata player = FindAnyObjectByType<chardata>();
        if(player != null)
        {
            player.nowmoney += money;
        }

        rb.gravityScale = 3f;

        if(enemy == null)
        {
            rb.linearVelocity = new Vector2(lasthit.x * lastpower, 8f);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }

        

        animator.SetBool("nomal", false);
        animator.SetTrigger("dead");

        Invoke("Startfade", 0.5f); //0.5초후에 실행
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

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (dead == true)
        {
            return;
        }


        if (collision.gameObject.CompareTag("Player"))
        {
            chardata player = collision.gameObject.GetComponent<chardata>();
            if (player != null)
            {
                player.damagesystem(transform.position); //여기서 좌표 보내고 
            }
        }
    }

    public void kncokback(Vector2 which, float power)
    {

        if (knockbackstop == true)
        {
            return;
        }

        meleattack enemy = GetComponent<meleattack>(); //그니깐 오브젝트에 이제 넉백을 막을 적의 스크립트가 들어있는지 확인
        if(enemy != null)
        {
            return; //달려있음 넉백안돼게
        }

        knockback = true;
        knockbacktime = Time.time;
        knockbackstop = true;
        knockbackstoptime = Time.time;
        rb.linearVelocity = which * power; //현재 위치 * 무기 파워  
        lasthit = which;
        lastpower = power;

    }

}


