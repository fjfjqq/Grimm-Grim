using UnityEngine;
using System.Collections;

public class saveui : MonoBehaviour 
{
    private SpriteRenderer[] saveslot;
    private mainuI comebackmenu; //메뉴 스크립트 연결
    private float saveorder = 0.2f; //한번에 안나오고 순서대로 나오게 할거라서 쿨타임주기
    private float savegounder = 2f; //얼마나 아래로 갈지
    private float[] originaly; // y값 담는 배열

    void Start()
    {
        originaly = new float[saveslot.Length];
        for(int i = 0; i < saveslot.Length; i++)
        {
            originaly[i] = saveslot[i].transform.position.y;
            saveslot[i].gameObject.SetActive(false);
        }
    }

    void saveuiload()
    {
        StartCoroutine(Savefadein());
    }

    void saveuifalse()
    {
        StartCoroutine(Savefadeout());
    }

    IEnumerator Savefadein()
    {
        for(int i = 0;i < saveslot.Length; i++)
        {
            saveslot[i].gameObject.SetActive(true);
            saveslot[i].color = new Color(1f, 1f, 1f, 0f);

        }
    }

    IEnumerator Savefadeout()
    {

    }

    IEnumerator Slotgounder(int slotcount)
    {

    }

    IEnumerator Slotgoup(int slotcount)
    {

    }
}
