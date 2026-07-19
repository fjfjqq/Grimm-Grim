using UnityEngine;
using UnityEngine.EventSystems;

public class enemy : MonoBehaviour 
{
    public int maxhp = 3;
    public int nowhp = 3;
    public float movespeed = 2f;     
    public float inground = 0.6f;
    public LayerMask groundcheck;

    private Rigidbody2D rb;
    public Vector2 movetheground = Vector2.left;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.linearVelocity = movetheground * movespeed;

        Vector2 movethewall;

        if (movetheground == Vector2.left)
        {
            movethewall = Vector2.down;
        }
        else if(movetheground == Vector2.down)
        {
            movethewall = Vector2.right;
        }   
        else if(movetheground == Vector2.right)
        {
            movethewall = Vector2.up;
        }
        else
        {
            movethewall = Vector2.left;
        }

        RaycastHit2D fronthit = Physics2D.Raycast(transform.position, movetheground, inground, groundcheck);

        RaycastHit2D sidehit = Physics2D.Raycast(transform.position, movethewall, inground, groundcheck);

        if(fronthit.collider != null)
        {
            movetheground = -movethewall;
        }
        else if (sidehit == null)
        {
            movetheground = movethewall;
        }

    }

}


