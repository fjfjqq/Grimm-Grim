using UnityEngine;
using System.Collections.Generic;

public class bulletsaver : MonoBehaviour
{
    public static bulletsaver instance; //다른 스크립트에서 바로 여기로 접근하게 해주는 프리패스 카드키라 생각하면 편할듯?

    public GameObject freeset; //프리셋 연결
    public int bulletsavebon = 10;
    private List<GameObject> saver; //리스트는 뭐 창고

    private void Awake()
    {
        instance = this; //컴포넌트를 instance에 연결, 이거 안넣었다가 NullReferenceException 떠서 3시간동안 머리박음 쉽게 생각해보면 공용 게시판
        saver = new List<GameObject>();

        for(int i = 0; i < bulletsavebon; i++)
        {
            GameObject gob = Instantiate(freeset); //gameobject의 약자 내일 봤을때 또 gob가 뭐지??! 하고 코드 다시 읽는 참사를 방지로 적어둠 즉 메모용
            gob.SetActive(false);
            saver.Add(gob);
        }
    }

    public GameObject Reroald()
    {
        for(int i = 0; i < saver.Count; i++) //장탄수 확인
        {
            if(saver[i].activeInHierarchy == false) //쉬었음 총알이냐 안쉬었음 총알이냐로 나누기
            {
                return saver[i]; //쉬었음 총알 있으면 반환 
            }
        }

        //여기까지 올 일이 없지만 총알 쏘는 얘들이 많아 10개의 쉬었음 총알로 부족할때

        GameObject bulletreroald = Instantiate(freeset); //총알 한명 강제 징집
        bulletreroald.SetActive(false); //일단은 쉬었음 총알으로 두기
        saver.Add(bulletreroald); //쉬었음 총알 수가 늘었으니 탄창 용량 +1
        return bulletreroald; 
    }

    public void Returnbullet(GameObject gob)
    {
        gob.SetActive(false); //비활성화하고 다시 반환 계속 생성하는거 메모리 너무 많이먹어
    }
}
