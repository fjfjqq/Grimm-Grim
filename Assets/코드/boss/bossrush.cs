using UnityEngine;
using System.Collections;

public class bossrush : bosspatern
{
    public bossai boss;
    public LayerMask playerlayer;

    public GameObject rusheffect;
    public float rusheffectlenght = 5f;

    public float jumptime = 0.8f;
    public float rushheigh = 2.5f;
    public float effecttime = 0.3f;
    public float endwait = 0.5f;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void Startpattern() //패턴 덮어씌우기 머리아파
    {
        if(running == true)
        {
            return;
        }
        StartCoroutine(Rushpattern());
    }

    private IEnumerator Rushpattern()
    {
        running = true;

        Transform targetpoint = boss.Gettargetpoint();

        float dirx = 1f;
        if(targetpoint.position.x < transform.position.x)
        {
            dirx = -1f;
        }

        boss.Facedirection(dirx); //준비자세 어디를 보고 있냐를 판단해주는거

        if(animator != null)
        {
            animator.SetTrigger("rush");
        }

        Vector2 startpos = transform.position;

        yield return new WaitForSeconds(jumptime);

        Vector2 endpos = new Vector2(targetpoint.position.x, transform.position.y); //대충 y랑 z는 유지하면서 x축만 바꾸는
        transform.position = new Vector3(endpos.x, endpos.y, transform.position.z);
        boss.isleft = !boss.isleft; //보스가 왼쪽 보고있었음 다음은 오른쪽 봐ㅏ라

        if(camershake.shakeing != null)
        {
            camershake.shakeing.Shake(0.25f, 0.45f);
        }

        Vector2 center = (startpos + endpos) *0.5f;
        float wideth = Mathf.Abs(endpos.x - startpos.x);
        Vector2 boxsize = new Vector2(wideth, rushheigh);

        Collider2D hit = Physics2D.OverlapBox(center, boxsize, 0f, playerlayer);

        if(hit != null)
        {
            chardata playerdata = hit.GetComponent<chardata>();
            if(playerdata != null)
            {
                playerdata.damagesystem(startpos);
            }
        }

        attackeffect.Spawn(rusheffect, startpos, endpos, rusheffectlenght, effecttime);

        yield return new WaitForSeconds(effecttime);
        yield return new WaitForSeconds(endwait);

        running = false;
    }
}
