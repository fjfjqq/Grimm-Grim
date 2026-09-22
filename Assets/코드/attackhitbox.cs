using UnityEngine;
using System.Collections.Generic;

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
     
        Collider2D[] target = Physics2D.OverlapBoxAll(transform.position + (Vector3)box.offset, box.size * transform.lossyScale, 0); // 좀 계산이 복잡하지만 일단 잊어버릴까봐 써놓자면 범위안에 모든 콜라이더를 탐지한다. 그 범위를 구해야하는데 offset은 히트박스의 중심점이 아니라 오브젝트로부터 얼마나 떨어져있는건지 재주는거라서 현재 위치에서 더해서 중심축을 구한후 현재 박스의 크기 * 실제 월드에 보이는 크기를 구하는 lossyscale을 곱해서 크기를 구한다. 회전은 필요없어서 0으로

        foreach (Collider2D list in target) //에너미 배열에 있는 콜라이더 하나씩 실행
        {
            if (list.CompareTag("Enemy") == true) //태그가 에너미일경우
            {
                enemy enemytarget = list.GetComponent<enemy>();
                if(enemytarget != null)
                {
                    enemytarget.nowhp -= (int)player.nowweapon.damage; //타겟이 된 에너미의 hp를 공격력에서 빼기
                }
            }
        }
    }

}
