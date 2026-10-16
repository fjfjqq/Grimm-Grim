using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sceenblackout : MonoBehaviour
{
    [SerializeField] private Image fadeout;
    [SerializeField] private float fadespeed = 0.002f;

    private void Start()
    {
        StartCoroutine(Fadein());
    }

    public void Gosceen(string sceenname)
    {
        StartCoroutine(Fadeoutandload(sceenname));
    }

    IEnumerator Fadein()
    {
        float endthefade = 1f;
        while (endthefade > 0f)
        {
            endthefade -= fadespeed;
            fadeout.color = new Color(0, 0, 0, endthefade);
            yield return null;
        }
        fadeout.raycastTarget = false; //이거 안꺼주면 클릭 잡아먹음
    }
    IEnumerator Fadeoutandload(string sceenname)
    {
        fadeout.raycastTarget = true; //켜는 이유는 씬 전환중에 버튼 누르면 에러터질테니깐
        float endthein = 0f;
        while (endthein < 1f)
        {
            endthein += fadespeed;
            fadeout.color = new Color(0, 0, 0, endthein);
            yield return null;
        }
        SceneManager.LoadScene(sceenname);
    }
}
