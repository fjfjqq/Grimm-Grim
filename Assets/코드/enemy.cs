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

        rb.gravityScale = 3f;

        rb.linearVelocity = new Vector2(lasthit.x * lastpower, 8f);

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

        knockback = true;
        knockbacktime = Time.time;
        knockbackstop = true;
        knockbackstoptime = Time.time;
        rb.linearVelocity = which * power; //현재 위치 * 무기 파워  
        lasthit = which;
        lastpower = power;
    }

}


