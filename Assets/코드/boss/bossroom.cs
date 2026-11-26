using UnityEngine;
using System.Collections;

public class bossroom : MonoBehaviour
{
    public bossai boss;
    public enemy bosshp;
    public GameObject bossobject;
    public GameObject[] doors;
    public bosshpbar hpbar;

    public float appeardelay = 1.5f;

    private bool started = false;
    private bool ended = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(started == true)
        {
            return;
        }
        if(collision.CompareTag("Player") == false)
        {
            return;
        }

        started = true;
        StartCoroutine(Startfight());
    }

    private IEnumerator Startfight()
    {
        for(int i = 0; i < doors.Length; i++)
        {
            doors[i].SetActive(true);
        }

        if(camershake.shakeing != null)
        {
            camershake.shakeing.Shake(0.3f, 0.3f);
        }

        bossobject.SetActive(true);

        yield return new WaitForSeconds(appeardelay);

        if(hpbar != null)
        {
            hpbar.Show(bosshp);
        }

        boss.Startboss();
    }

    private void Update()
    {
        if(started == false)
        {
            return;
        }

        if(ended == true)
        {
            return;
        }

        if(bosshp == null)
        {
            return;
        }

        if(bosshp.dead == true)
        {
            ended = true;
            Endfight();
        }
    }

    private void Endfight()
    {
        boss.Stopboss();

        if(hpbar != null)
        {
            hpbar.Hide();
        }

        for(int i = 0; i < doors.Length; i++)
        {
            doors[i].SetActive(false);
        }
    }
}
