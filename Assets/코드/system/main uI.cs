using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem.Controls;

public class mainuI : MonoBehaviour
{
    [SerializeField] private AudioClip clicksound;
    public saveui saveuiload; //saveui시스템이랑 연결하기
    public string mainbutton;
    public SpriteRenderer[] allbutton;
    SpriteRenderer mainsprtie;
    Vector3 touch;
    bool fade = false;

    private void Start()
    {
        mainsprtie = GetComponent<SpriteRenderer>();
        touch = transform.localScale;
    }

    private void OnMouseEnter()
    {
        mainsprtie.color = new Color(1.2f, 1.2f, 1.2f, 1.2f);
        transform.localScale = touch * 1.07f;
    }

    private void OnMouseExit()
    {
        transform.localScale = touch;
        mainsprtie.color = new Color(1f, 1f, 1f, 1f);
    }
    void OnMouseDown()
    {

        AudioSource.PlayClipAtPoint(clicksound, transform.position);

        if (fade) return;

        if (mainbutton == "start")
        {
            StartCoroutine(Fadeout());
           
        }
        else if (mainbutton == "exit")
        {
            Application.Quit();
        }
    }

    IEnumerator Fadeout()
    {
        fade = true;
        float timecheck = 1f;

        while (timecheck > 0f)
        {
            timecheck -= 0.007f;

            for (int i = 0; i < allbutton.Length; i++)
            {
                allbutton[i].color = new Color(1f, 1f, 1f, timecheck);
            }

            yield return null;
        }

        for (int i = 0; i < allbutton.Length; i++)
        {
            allbutton[i].gameObject.SetActive(false);
        }

        saveuiload.saveuiload(); //세이브 ui에 신호보내기
    }

    public void mainuiload()
    {
        gameObject.SetActive(true);
        StartCoroutine(Fadein());
    }
    IEnumerator Fadein()
    {
        for (int i = 0; i < allbutton.Length; i++)
        {
            allbutton[i].gameObject.SetActive(true);
            allbutton[i].color = new Color(1f, 1f, 1f, 0f);
        }

        float timecheck = 0f;
        while (timecheck < 1f)
        {
            timecheck += 0.007f;
            for (int i = 0; i < allbutton.Length; i++)
            {
                allbutton[i].color = new Color(1f, 1f, 1f, timecheck);
            }

            yield return null;
        }

        fade = false;
    }
}