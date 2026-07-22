using UnityEngine;
using UnityEngine.EventSystems;

public class enemy : MonoBehaviour 
{
    public int maxhp = 3;
    public int nowhp = 3;

    public float movespeed = 2f;
    public bool turnpoint = true;

    public Collider2D lastturn = null;

    public Vector2 down = Vector2.down;
    void Start()
    {
        
    }

    void Update()
    {
        Vector2 enemy = Nowmoving();
        transform.position += (Vector3)(enemy * movespeed * Time.deltaTime);
        rollthesurface();
    }

    Vector2 Nowmoving()
    {
        if (turnpoint)
        {
            return new Vector2(down.y, -down.x);
        }
        {
            return new Vector2(-down.y, down.x);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("turnpoint") && collision != lastturn)
        {

            lastturn = collision;

            if (turnpoint)
            {
                down = new Vector2(down.y, -down.x);
            }
            else
            {
                down = new Vector2(-down.y, down.x);
            }
        }
    }

    void rollthesurface()
    {
        transform.rotation = Quaternion.FromToRotation(Vector2.down, down);
    }
}


