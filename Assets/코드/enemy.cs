using UnityEngine;

public class enemy : MonoBehaviour 
{
    public int maxhp = 3;
    public int nowhp = 3;
    public int turnpoint1;
    public int turnpoint2;
    public float movespeed = 2f;

    private Rigidbody2D rb;
    private bool gotoleft = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //진짜 수정하고 수정하고 포인터 지정해서 할까했는데 유저랑 충돌하면 이상해지고

        

    }

    void Update()
    {

    }

}


