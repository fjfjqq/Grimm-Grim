using UnityEngine;

public class savelamp : MonoBehaviour
{
    [SerializeField] private saveload savethegame;//세이브 로드 스크립트랑 연결
    [SerializeField] private int chapter;
    [SerializeField] private int mapnumber;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject tutorial;

    private bool playerinrange = false;
    private bool saveing;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerinrange == true && saveing == false && Input.GetKeyDown(KeyCode.W)) //플레이어 인 레인지가 트루고 w를 눌렀을때 실행
        {
            saveing = true;
            animator.SetTrigger("save");
        }
    }
    void OnTriggerEnter2D(Collider2D collider) //트리거 콜라이더 안에 있을때
    {
        if (collider.CompareTag("Player")) //태그로 플레이어 잡혀있는지 감지
        {
             playerinrange = true; // 트루로 변환
             tutorial.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collider) //트리거 콜라이더 범위 안에 없을때
    {
        if (collider.CompareTag("Player")) //태그로 플레이어 감지
        {
            playerinrange = false; /// 거짓으로 변환
            tutorial.SetActive(false);
        }
    }

    public void Endsave()
    {
        savethegame.Gamesave(chapter, mapnumber);
        saveing = false;

    }
}
