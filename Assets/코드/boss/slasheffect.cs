using UnityEngine;

public class slasheffect : MonoBehaviour
{
    private Vector2 startpoint;
    private Vector2 endpoint;
    private bool slashing = false;

    private GameObject lineobject;
    private SpriteRenderer sprite;

    private float slashthickk = 2f; //두꺼워질 예정임
    private float waringslash = 0.45f; //투명도임

    public void Setup(Vector2 start, Vector2 end, GameObject freeset, float baselength, float thick, float waring)
    {
        startpoint = start;
        endpoint = end;
        slashthickk = thick;
        waringslash = waring;

        lineobject = attackeffect.Spawn(freeset, start, end, baselength, 0f);
        if (lineobject == null)
        {
            return;
        }

        sprite = lineobject.GetComponent<SpriteRenderer>();

        if (sprite != null)
        {
            Color color = sprite.color;
            color.a = waringslash;
            sprite.color = color;
        }

    }

    public void Slash()
    {
        slashing = true;

        if (lineobject == null)
        {
            return;
        }

        Vector3 scale = lineobject.transform.localScale;
        scale.y = scale.y * slashthickk;
        lineobject.transform.localScale = scale;

        if (sprite != null)
        {
            Color color = sprite.color;
            color.a = 1f;
            sprite.color = color;
        }
    }

    public bool Checkhit(Vector2 playerpos, float playerradisu)
    {
        if (slashing == false)
        {
            return false;
        }

        float distance = Distancetoline(playerpos, startpoint, endpoint);

        if (distance < playerradisu + 0.18f)
        {
            return true;
        }
        return false;
    }


    public static float Distancetoline(Vector2 point, Vector2 linstart, Vector2 linned)
    {
        Vector2 linedirection = linned - linstart;
        float linelength = linedirection.magnitude;

        if (linelength < 0.0001f)
        {
            return Vector2.Distance(point, linstart);
        }

        Vector2 linenorm = linedirection / linelength;
        float dot = Vector2.Dot(point - linstart, linenorm);

        if (dot < 0f)
        {
            return dot = Vector2.Distance(point, linstart);
        }
        if(dot > linelength)
        {
            return Vector2.Distance(point, linned);
        }

        Vector2 closest = linstart + linenorm * dot;
        return Vector2.Distance(point, closest);
    }

    public void Fadeout()
    {
        if(lineobject != null)
        {
            Destroy(lineobject);
        }
        Destroy(gameObject);
    }
}
