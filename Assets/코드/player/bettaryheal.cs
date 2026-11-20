using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class bettaryheal : MonoBehaviour    
{
    public static bettaryheal main;
    public Sprite[] batterysprites;
    public Image batteryimage;

    public int maxcharge = 6;
    public int nowcharge = 0;
    private bool isheal = false;
    private chardata player;
    private Animator playeranimation;
    private void Start()
    {
        player = FindAnyObjectByType<chardata>();
        playeranimation = player.GetComponent<Animator>();
        nowcharge = playersavesystem.batterycharge;
        Updateui();
    }
    private void Awake()
    {
        main = this;
    }

    private void Update()
    {
        if(nowcharge < maxcharge)
        {
            return;
        }

        if (isheal == true)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            isheal = true;
            playeranimation.SetBool("heal", true);
        }
    }
    public void Healend()
    {
        player.nowhp += 1;
        if(player.nowhp > player.maxhp)
        {
            player.nowhp = player.maxhp;
        }
        nowcharge = 0;
        isheal = false;
        playeranimation.SetBool("heal", false);
        Updateui(); 
    }
    public void Cancelheal()
    {
        if (isheal == true)
        {
            isheal = false;
            playeranimation.SetBool("heal", false);
            Updateui();
        }
    }
    public void Getpower()
    {
       if(nowcharge < maxcharge)
        {
            nowcharge += 1;
            Updateui();
        }
    }

    void Updateui()
    {
        batteryimage.sprite = batterysprites[nowcharge];
    }

}
