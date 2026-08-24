using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class saveui : MonoBehaviour
{
    [SerializeField] private backbutton back;
    [SerializeField] private SpriteRenderer[] saveslot;
    [SerializeField] private mainuI comebackmenu; //메뉴 스크립트 연결
    private float saveorder = 0.3f; //한번에 안나오고 순서대로 나오게 할거라서 쿨타임주기
    private float savegounder = 2f; //얼마나 아래로 갈지 또는 위로갈지
    private float[] originaly; // y값 담는 배열

    void Start()
    {
        originaly = new float[saveslot.Length];
        for (int i = 0; i < saveslot.Length; i++)
        {
            originaly[i] = saveslot[i].transform.position.y;
            saveslot[i].gameObject.SetActive(false);
        }
    }
    public void saveuiload()
    {
        StartCoroutine(Savefadein());
    }

    public void saveuifalse()
    {
        StartCoroutine(Savefadeout());
    }

    IEnumerator Savefadein()
    {
        for (int i = 0; i < saveslot.Length; i++)
        {
            saveslot[i].gameObject.SetActive(true); // 게임 오브젝트 기능 on
            saveslot[i].color = new Color(1f, 1f, 1f, 0f); // 색깔 바꾸기

            Vector3 where = saveslot[i].transform.position; //i번째 배열에 있는 현재위치 저장    
            where.y = originaly[i] + savegounder; //where에서 저장해놓은 y값 가져와서 내가 지정해놓은 값만큼 올려놓기
            saveslot[i].transform.position = where; //그 위치로 옮기기

            StartCoroutine(Slotgounder(i)); //신호 보내고 시작
            yield return new WaitForSeconds(saveorder); // 내려오는 딜레이
        }

        back.Show();
    }
    IEnumerator Savefadeout()
    {
        back.Hide();

        for (int i = 0;i < saveslot.Length; i++)
        {
            StartCoroutine(Slotgoup(i));
            yield return new WaitForSeconds(saveorder); //다음 슬롯 실행못시키게 내가 지정해둔 시간만큼 딜레이
        }

        yield return new WaitForSeconds(0.5f); // for문 끝나고 0.5초정도 올라가는동안 대기 시간주기(올라가는중에 사라지는거 막기)

        for (int i = 0; i < saveslot.Length; i++)
        {
            saveslot[i].gameObject.SetActive(false); //기능 끄기
        }

        comebackmenu.mainuiload();
    }
    IEnumerator Slotgounder(int slotcount)
    {
        float time = 0f; //진행도
        Vector3 wherestart = saveslot[slotcount].transform.position; //현재 위치를 슬롯에 저장
        float starty = wherestart.y; //시작 y값만 따로 저장해두기
        float undery = originaly[slotcount]; //원래 y값 위치 즉 여기까지 내려와야한다

        while(time < 1f)
        {
            time += 0.003f;
            saveslot[slotcount].transform.position = new Vector3(wherestart.x, Mathf.Lerp(starty, undery, time), wherestart.z); //시작이랑 끝 위치 정해두고 부드럽게 이동하게 하기
            saveslot[slotcount].color = new Color(1f, 1f, 1f, time); //진행도인 time에 비례해서 페이드 인

            yield return null; 
        }
    }
    IEnumerator Slotgoup(int slotcount)
    {
        float time = 1f; //진행도지만 페이드 아웃이니 1부터 시작
        Vector3 wherestart = saveslot[slotcount].transform.position; //현재위치를 슬롯에 저장
        float starty = wherestart.y; //시작 y값 정해놓기
        float ony = originaly[slotcount] + savegounder; // 원래 y값 + 2 올라간 위치로 올라가야한다

        while(time > 0f)
        {
            time -= 0.003f;
            saveslot[slotcount].transform.position = new Vector3(wherestart.x, Mathf.Lerp(ony, starty, time), wherestart.z); //시작이랑 끝 위치 정해두고 lerp로 부드럽게 이동
            saveslot[slotcount].color = new Color(1f, 1f, 1f, time); //진행도인 time에 비례해서 페이드 아웃
            yield return null;
        }
    }
}