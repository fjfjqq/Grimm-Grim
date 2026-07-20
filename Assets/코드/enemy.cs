using UnityEngine;
using UnityEngine.EventSystems;

public class enemy : MonoBehaviour 
{
    public int maxhp = 3;
    public int nowhp = 3;

    public float movespeed = 2f;
    public bool turnclock = true;

    public float groundcheck = 0.6f;
    public float sidecheck = 0.3f;
    public LayerMask ground;

    public Vector2 down = Vector2.down;
    void Start()
    {

    }

    void Update()
    {
        Vector2 laycast = Nowmoving();

        Debug.Log("laycast" + laycast + "down" + down);

        Debug.DrawRay(transform.position, down * groundcheck, Color.red);

        if (Frontup(laycast))
        {
            turncorner();
            laycast = Nowmoving();
        }

        transform.position += (Vector3)(laycast * movespeed * Time.deltaTime);
        shootlaycast();
        rollthesurface();


    }

    Vector2 Nowmoving()
    {
        if (turnclock)
        {
            return new Vector2(down.y, -down.x);
        }
        else
        {
            return new Vector2(-down.y, down.x);
        }
    }

    void turncorner()
    {
        if (turnclock)
        {
            down = new Vector2(down.y, -down.x);
        }
        else
        {
            down = new Vector2(-down.y, down.x);
        }
    }

    bool Frontup(Vector2 laycast)
    {
        Vector2 start = (Vector2)transform.position + laycast * sidecheck;

        RaycastHit2D hit = Physics2D.Raycast(start, down, groundcheck, ground);

        return hit.collider == null;
    } 

    void shootlaycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, down, groundcheck, ground);

        if(hit.collider != null)
        {
            float off = 0.1f;
            transform.position = hit.point - down * off;
        }
    }

    void rollthesurface()
    {
        transform.rotation = Quaternion.FromToRotation(Vector2.down, down);
    }

}


