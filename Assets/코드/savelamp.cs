using UnityEngine;

public class savelamp : MonoBehaviour
{
    [SerializeField] private saveload savethegame;//세이브 로드 스크립트랑 연결
    [SerializeField] private int chapter;
    [SerializeField] private int mapnumber;

    void OnTriggerEnter2D(Collider2D collider) //트리거 콜라이더에 닿았을때
    {
        if (collider.CompareTag("Player"))
        {
            savethegame.Gamesave(chapter, mapnumber); //근데 만들고보니 상호작용이 더 자연스러울거같아서 집가서 수정
        }
    }
}
