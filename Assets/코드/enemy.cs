using UnityEngine;
using UnityEngine.EventSystems;

public class enemy : MonoBehaviour
{
    public int maxhp = 3;
    public int nowhp = 3;

    public float movespeed = 2f;
    public Transform[] turnpoint;

    public int nowpoint = 0;
    void Start()
    {

    }

    void Update()
    {
        Transform target = turnpoint[nowpoint];
        Vector2 targetgo = (target.position - transform.position).normalized;

        transform.position += (Vector3)(targetgo * movespeed * Time.deltaTime);

        float targetgogo = Vector2.Distance(transform.position, target.position);
        if(targetgogo < 0.1f)
        {
            nowpoint = (nowpoint + 1) % turnpoint.Length;
        }

        rollthesurface(targetgo);
    }

    void rollthesurface(Vector2 targetgo)
    {
        float anchor = Mathf.Atan2(targetgo.y, targetgo.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, anchor + 90f);
    }

}


