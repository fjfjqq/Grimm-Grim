using UnityEngine;

public class enemybug : MonoBehaviour 
{
    [SerializeField] private Transform flag1;
    [SerializeField] private Transform flag2;
    [SerializeField] private float movespeed = 4f;

    private Transform target;
    private enemy basedata;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        basedata = GetComponent<enemy>();
        target = flag1;
    }

    private void Update()
    {
        if(basedata.dead == true)
        {
            return;
        }
        if(basedata.knockback == true)
        {
            return;
        }

        Vector2 which = (target.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(which.x * movespeed, rb.linearVelocity.y); //방향전환

        if(which.x > 0)
        {
            transform.localScale = new Vector3(-0.6f, 0.6f, 1);
        }
        else if(which.x < 0)
        {
            transform.localScale = new Vector3(0.6f, 0.6f, 1);
        }

        float checkpoint = Vector2.Distance(transform.position, target.position);

        if(checkpoint < 0.3f)
        {
            if (target == flag1)
            {
                target = flag2;
            }
            else if(target == flag2)
            {
                target = flag1;
            }
        }
    }
}
