using UnityEngine;
using static UnityEditor.PlayerSettings;
public class chardata : MonoBehaviour
{
    public Weapon[] weaponeslot = new Weapon[3];
    public Weapon nowweapon;

    public float moveSpeed = 5f;  
    public float jumpForce = 10f;

    public float lastTime = 0f;
    public float swaptime = 0f;
    public float swapspeed = 1f;
    public int weaponechange = 1;

    private Rigidbody2D rb;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundcheck;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Weapon parrysword = new Weapon();
        parrysword.weaponename = "기본 검";
        parrysword.damage = 10f;
        parrysword.attackspeed = 1.0f;

        Weapon largesword = new Weapon();
        largesword.weaponename = "대검";
        largesword.damage = 20f;
        largesword.attackspeed = 1.5f;

        Weapon shortsword = new Weapon();
        shortsword.weaponename = "단검";
        shortsword.damage = 3f;
        shortsword.attackspeed = 0.3f;

        Weapon bat = new Weapon();
        bat.weaponename = "배트";
        bat.damage = 12f;
        bat.attackspeed = 1.4f;

        weaponeslot[0] = parrysword;
        weaponeslot[1] = null;
        weaponeslot[2] = null;

        nowweapon = weaponeslot[0];
    }

    void weaponeswap()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1) && weaponechange != 1 && Time.time >= swaptime + swapspeed && weaponeslot[0] != null)
        {
            weaponechange = 1;
            nowweapon = weaponeslot[0];
            swaptime = Time.time;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && weaponechange != 2 && Time.time >= swaptime + swapspeed && weaponeslot[1] != null)
        {
            weaponechange = 2;
            nowweapon = weaponeslot[1];
            swaptime = Time.time;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && weaponechange != 3 && Time.time >= swaptime + swapspeed && weaponeslot[2] != null)
        {
            weaponechange = 3;
            nowweapon = weaponeslot[2];
            swaptime = Time.time;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.linearVelocity = new Vector2(-5, rb.linearVelocity.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.linearVelocity = new Vector2(5, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }

        weaponeswap();

        bool stepground = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundcheck);

        if (Input.GetKeyDown(KeyCode.Space) && stepground)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 14);

        }

        if (Input.GetKeyUp(KeyCode.Space) && 0 < rb.linearVelocity.y)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.6f);
        }

        if (Input.GetMouseButtonDown(0) && Time.time >= lastTime + nowweapon.attackspeed)
        {
            
            lastTime = Time.time;

            if(weaponechange == 1)
            {
                Debug.Log("1번");
            }
            else if(weaponechange == 2)
            {
                Debug.Log("2번");
            }
            else if(weaponechange == 3)
            {
                Debug.Log("3번");
            }
        }
        

    }
}

public class Weapon
{
    public float attackspeed;
    public float damage;
    public float range;
    public string weaponename;
    
}

