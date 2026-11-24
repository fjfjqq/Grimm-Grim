using System.Collections;
using UnityEngine;

public class bossai : MonoBehaviour 
{
    public bosspatern[] patterns;
    public Transform leftpoint;
    public Transform rightpoint;
    public Transform player;

    public bool isleft = true;
    public bool stop = false;

    public float startdelay = 1f;
    public float patterndelay = 1f;

    private Animator animator;
    private int lastpattern = -1;
    private float basescalex;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        basescalex = Mathf.Abs(transform.localScale.x);
    }

    private void Start()
    {
        removeside();
        StartCoroutine(Patternloop());
    }
    private void Update()
    {
        if (stop == true)
        {
            return;
        }
        Lookplayer();
    }

    private void removeside()
    {
        Transform point = Getnowpoint();
        transform.position = new Vector3(point.position.x, transform.position.y, transform.position.z);
    }

    public Transform Getnowpoint()
    {
        if(isleft == true)
        {
            return leftpoint;
        }
        return rightpoint;
    }

    public Transform Gettargetpoint()
    {
        if(isleft == true)
        {
            return rightpoint;
        }
        return leftpoint;
    }

    public void Lookplayer()
    {
        if(player == null)
        {
            return;
        }

        if(player.position.x < transform.position.x)
        {
            Facedirection(-1f);
        }
        else
        {
            Facedirection(1f);
        }
    }

    public void Facedirection(float dirx)
    {
        float scalex = basescalex;
        if(dirx < 0f)
        {
            scalex = -basescalex;
            }
            transform.localScale = new Vector3(scalex, transform.localScale.y, transform.localScale.z);
    }

    private IEnumerator Patternloop()
    {
        yield return new WaitForSeconds(startdelay);

        while (stop == false)
        {
            int pick = Pickpattern();

            if(pick < 0)
            {
                yield break;
            }

            patterns[pick].Startpattern();
            lastpattern = pick;

            while(patterns[pick].running == true)
            {
                yield return null;
            }

            yield return new WaitForSeconds(patterndelay);
        }
    }

    private int Pickpattern()
    {
        if(patterns == null)
        {
            return -1; //말이 안돼는거면 반환
        }

        if(patterns.Length == 0)
        {
            return -1; //말이 안돼는거면 반환
        }

        if(patterns.Length <= 2)
        {
            return Random.Range(0, patterns.Length);
        }

        for(int count = 0; count < 20; count++)
        {
            int pick = Random.Range(0, patterns.Length);

            if(pick != lastpattern)
            {
                return pick;
            }
        }

        return Random.Range(0, patterns.Length);
    }

    public void Stopboss()
    {
        stop = true;
        StopAllCoroutines();

        for(int i = 0; i < patterns.Length; i++)
        {
            patterns[i].StopAllCoroutines();
            patterns[i].running = false;
        }
    }
}
