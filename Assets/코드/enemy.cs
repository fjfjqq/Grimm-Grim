using UnityEngine;
using UnityEngine.EventSystems;

public class enemy : MonoBehaviour 
{
    public int maxhp = 3;
    public int nowhp = 3;
    public int turnpoint1;
    public int turnpoint2;
    public float movespeed = 2f;
        
    public Vector2 movethewall = Vector2.left;
    public float inground = 0.6f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        

    }

    void Update()
    {
        rb.linearVelocity = movethewall * movespeed;

        Vector2 sideDirection;



        
    }

}


