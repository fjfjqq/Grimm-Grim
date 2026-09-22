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


