using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bossslash : bosspatern
{
    public bossai boss;
    public Transform arenacenter;
    public Transform groudpoint;

    public GameObject slashlinefreeset;
    public float slashlinelenght = 5f;
    public float thickmultity = 2f;
    public float waringpoint = 0.45f;

    public float upmotiontime = 0.8f;
    public float downmotiontime = 0.8f;

    public int slashcount = 8;
    public float waringdelay = 0.3f;
    public float slashwait = 0.3f;
    public float showtime = 0.25f;

    public float arenawidght = 20f;
    public float arenaheight = 10f;

    public float playerradius = 0.35f;
    public float safedistance = 1.6f;
    public float savepointup = 1f;

    private List<slasheffect> slashline = new List<slasheffect>();
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (boss == null)
        {
            boss = GetComponent<bossai>();
        }
    }

    public override void Startpattern()
    {
        if(running == true)
        {
            return;
        }
        StartCoroutine(Slashpattern());
    }

    private IEnumerator Slashpattern()
    {
        running = true;
        slashline.Clear();

        if(animator != null)
        {
            animator.SetTrigger("slashup");
        }

        yield return new WaitForSeconds(upmotiontime);

        Vector2 center = Getcenter();

        float safety = center.y;
        if(groudpoint != null)
        {
            safety = groudpoint.position.y + savepointup;
        }

        Vector2 safepoint = new Vector2(center.x + Random.Range(-arenawidght * 0.4f, arenawidght * 0.4f), safety);

        for(int i = 0; i < slashcount; i++)
        {
            slasheffect newslash = Createslashline(center, safepoint);
            slashline.Add(newslash);
            yield return new WaitForSeconds(waringdelay);
        }

        yield return new WaitForSeconds(slashwait);

        if(animator != null)
        {
            animator.SetTrigger("slash");
        }

        for(int i = 0; i < slashline.Count; i++)
        {
            slashline[i].Slash();
        }

        if(camershake.shakeing != null)
        {
            camershake.shakeing.Shake(0.25f, 0.4f);
        }

        Checkplayerhit();

        yield return new WaitForSeconds(showtime);

        for(int i = 0; i < slashline.Count; i++)
        {
            slashline[i].Fadeout();
        }
        slashline.Clear();

        if(animator != null)
        {
            animator.SetTrigger("slashdown");
        }

        yield return new WaitForSeconds(downmotiontime);

        running = false;
    }

    private void Checkplayerhit()
    {
        if(boss == null)
        {
            return;
        }
        if(boss.player == null)
        {
            return;
        }

        Vector2 playerpos = boss.player.position;
        bool hit =false;

        for(int i = 0; i < slashline.Count; i++)
        {
            if (slashline[i].Checkhit(playerpos, playerradius) == true)
            {
                hit = true; 
                break;
            }
        }

        if(hit == false)
        {
            return;
        }

        chardata playerdata = boss.player.GetComponent<chardata>();
        if(playerdata != null)
        {
            playerdata.damagesystem(transform.position);
        }
    }

    private Vector2 Getcenter()
    {
        if(arenacenter != null)
        {
            return arenacenter.position;
        }
        return transform.position;
    }

    private slasheffect Createslashline(Vector2 center, Vector2 safepoint)
    {
        Vector2 start = Vector2.zero;
        Vector2 end = Vector2.zero;

        for(int trycount = 0; trycount < 30; trycount++)
        {
            float angle = Random.Range(0f, 180f);
            float rad = angle * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            Vector2 point = new Vector2(center.x + Random.Range(-arenawidght * 0.5f, arenawidght * 0.5f), center.y + Random.Range(-arenaheight * 0.5f, arenaheight * 0.5f));

            start = point + dir * 30f;
            end = point - dir * 30f;

            float dist = slasheffect.Distancetoline(safepoint, start, end);

            if (dist > safedistance)
            {
                break;
            }
        }

        GameObject obj = new GameObject("slashline");
        slasheffect slash = obj.AddComponent<slasheffect>();
        slash.Setup(start, end, slashlinefreeset, slashlinelenght, thickmultity, waringpoint);

        return slash;
    }
}
