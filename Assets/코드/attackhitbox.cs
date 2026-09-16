using UnityEngine;

public class attackhitbox : MonoBehaviour
{
    [SerializeField] private chardata player;

    private void OnTriggerEnter2D(Collider2D collision) //이 히트박스에 다른 콜라이더가 닿았을때 정보 넣기
    {
        if (collision.CompareTag("Enemy")) // 닿은 대상 태그 적인지 확인
        {
            enemy target = collision.GetComponent<enemy>(); 
            if(target != null) // 샌드백
            target.nowhp -= (int)player.nowweapon.damage; //현재 무기만큼 데미지 주기

        }
    }
}
