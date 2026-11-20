using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.SceneManagement;

public class NewEmptyCSharpScript : MonoBehaviour
{
    Vector3 touch;
    SpriteRenderer mainsprtie;
    [SerializeField] private int numberofslot; // 슬롯 넘버
    [SerializeField] private sceenblackout fade;

    public void Start()
    {
        mainsprtie = GetComponent<SpriteRenderer>();
        touch = transform.localScale;
    }
    private void OnMouseEnter()
    {
        transform.localScale = touch * 1.07f;
    }

    private void OnMouseExit()
    {
        transform.localScale = touch;
    }

    private void OnMouseDown()
    {
        PlayerPrefs.SetInt("loadslot", numberofslot);

        mapsavefile mapdata = saveload.Loadmap(numberofslot);

        if(mapdata != null)
        {
            fade.Gosceen("Chapter" + mapdata.chapter + "-" + mapdata.mapnumber); //데이터가 있으면 저장된 맵으로 이동
        }   
        else
        {
            fade.Gosceen("Chapter" + 1 + "-" + 1); //데이터 없으면 새게임이니깐 1에서 시작
        }
    }
}
