using System.Collections;
using UnityEngine;

public class backbutton : MonoBehaviour 
{
    [SerializeField] private saveui savesystem;
    Vector3 touch;
    SpriteRenderer mainsprtie;

    private void Start()
    {
        mainsprtie = GetComponent<SpriteRenderer>();
        touch = transform.localScale;
        gameObject.SetActive(false);
    }

    public void Hide()
    {
        StartCoroutine(Fadeout());
    }

    public void Show()
    {
        gameObject.SetActive(true);
        StartCoroutine(Fadein());
    }

    private void OnMouseDown()
    {
        savesystem.saveuifalse();
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

    IEnumerator Fadein()
    {
        gameObject.SetActive(true);
        float time = 0f;

        while(time < 1f)
        {
            time += 0.02f;
            mainsprtie.color = new Color(1f, 1f, 1f, time);
            yield return null;
        }
    }

    IEnumerator Fadeout()
    {
        float time = 1f;

        while (time > 0f)
        {
            time -= 0.02f;
            mainsprtie.color = new Color(1f, 1f, 1f, time);
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
