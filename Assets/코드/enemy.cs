using UnityEngine;

public class enemy : MonoBehaviour 
{
    public int maxhp = 3;
    public int nowhp = 3;
    public float movespeed = 2f;

    private Rigidbody2D rb;
    private bool gotoleft = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

}


