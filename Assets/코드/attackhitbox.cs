using UnityEngine;
using System.Collections.Generic;
using System;

public class attackhitbox : MonoBehaviour
{
    [SerializeField] private chardata player;
    private BoxCollider2D box;

    void Start() 
    {
        box = GetComponent<BoxCollider2D>(); //자동삽입
    }

    public void Attack()
    {
        Vector2 boxbox = transform.TransformPoint(box.offset); //로컬 좌표를 게임내 월드 좌표로 변경 아래 있던걸 나눠서 끝

        Vector2 boxsize = new Vector2(box.size.x * MathF.Abs(transform.lossyScale.x), box.size.y * MathF.Abs(transform.lossyScale.y)); //원래 방향전환을 로컬 스케일을 음수로 바꿔서 했는데 그거 때문에 박스 크기가 이상해져버린것 그거때문에 양수로 고정해주는 abs로 양수로 바꿔줌
     
        Collider2D[] target = Physics2D.OverlapBoxAll(boxbox,boxsize, 0); // 하다가 설계를 바꾸니까 오류나서 초기화
        foreach (Collider2D list in target) //에너미 배열에 있는 콜라이더 하나씩 실행
        {
            if (list.CompareTag("Enemy") == true) //태그가 에너미일경우
            {
                enemy enemytarget = list.GetComponent<enemy>();
                if(enemytarget != null)
                {
                    enemytarget.nowhp -= (int)player.nowweapon.damage; //타겟이 된 에너미의 hp를 공격력에서 빼기

                    Vector2 knockbackwich = (list.transform.position - player.transform.position).normalized; //적의 위치에서 플레이어의 위치를 뺀다. 그러면 x좌표가 -혹은 +가 나올텐데 그걸로 어디로 넉백될지 정한다. 노말라이즈 붙여서 방향만 남겨주기
                    enemytarget.kncokback(knockbackwich, player.nowweapon.knockback); // 어느 방향으로 얼마나 세게 칠지 얼마나 세게 칠지는 현재 무기 넉백
                }
            }
        }
    }

}
