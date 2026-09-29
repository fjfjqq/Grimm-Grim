using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class npc : MonoBehaviour
{
    private GameObject shop;
    private GameObject talkui;
    private GameObject selectui;
    private GameObject shoui;
    private GameObject questui;
    [SerializeField] private string[] talklist;

    private chardata player;
    private bool playerinrange = false;
    private bool talking = false;
    private int talknumber = 0;

    private shopitem[] shopitme;
    private publicshop publicshop;


    void Update()
    {
        if (playerinrange == true && talking == false && Input.GetKeyDown(KeyCode.W)) //플레이어 인 레인지가 트루고 w를 눌렀을때 실행
        {
            Starttalk();
        }
        else
        {
            nexttalk();
        }
    }

    void Starttalk()
    {
        talking = true;
        talknumber = 0;
        player.enabled = false;
        player.rb.linearVelocity = Vector2.zero;

        shop.SetActive(false);
        talkui.SetActive(true);
        showtalk();
    }
    void showtalk()
    {
        talkui.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = talklist[talknumber];
    }

    void nexttalk()
    {
        talknumber++;

        if (talknumber < talklist.Length)
        {
            showtalk();
        }
        else
        {
            talkui.SetActive(false);
            selectui.SetActive(true);
        }
    }

    public void Openshop()
    {
        selectui.SetActive(false);
        publicshop.Open(shopitme);
    }

    public void Openquest()
    {
        selectui.SetActive(false);
        questui.SetActive(true);
    }

    public void Close()
    {
        selectui.SetActive(false);
        shoui.SetActive(false);
        questui.SetActive(false);
        talkui.SetActive(false);
        talking = false;
        player.enabled = true;
    }
    void OnTriggerEnter2D(Collider2D collider) //트리거 콜라이더 안에 있을때
    {
        if (collider.CompareTag("Player")) //태그로 플레이어 잡혀있는지 감지
        {
            playerinrange = true; // 트루로 변환
            shop.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collider) //트리거 콜라이더 범위 안에 없을때
    {
        if (collider.CompareTag("Player")) //태그로 플레이어 감지
        {
            playerinrange = false; /// 거짓으로 변환
            shop.SetActive(false);
        }
    }
}
