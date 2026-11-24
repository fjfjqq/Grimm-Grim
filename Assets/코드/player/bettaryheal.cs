using UnityEngine;
using UnityEngine.UI;

public class bettaryheal : MonoBehaviour
{
    public static bettaryheal main;
    public Sprite[] batterysprites;
    public Image batteryimage;
    public int maxcharge = 6;
    public int nowcharge = 0;
    public bool isheal = false;

    private chardata player;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        player = FindAnyObjectByType<chardata>();
        nowcharge = playersavesystem.batterycharge;
        Updateui();
    }
    private void Update()
    {
        if (isheal == true)
        {
            return;
        }

        if (nowcharge < maxcharge)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            isheal = true;
            player.Healanimation();
        }
    }
    public void Healend()
    {
        if (isheal == false)
        {
            return;
        }

        player.nowhp += 1;
        if (player.nowhp > player.maxhp)
        {
            player.nowhp = player.maxhp;
        }

        nowcharge = 0;
        isheal = false;
        player.Healanimationend();
        Updateui();
    }
    public void Cancelheal()
    {
        if (isheal == true)
        {
            isheal = false;
            player.Healanimationend();
            Updateui();
        }
    }
    public void Getpower()
    {
        if (nowcharge < maxcharge)
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