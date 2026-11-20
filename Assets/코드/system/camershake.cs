using UnityEngine;
using System.Collections;

public class camershake : MonoBehaviour
{
    public static camershake shakeing;

    private Vector3 playerpos;

    private void Awake()
    {
        shakeing = this;
    }
    public void Shake(float shaketime, float shakepower)
    {
        StopAllCoroutines();
        StartCoroutine(Doshake(shaketime, shakepower));
    }

    IEnumerator Doshake(float shaketime, float shakepower)
    {
        playerpos = transform.localPosition;
        float timer = shaketime;

        while (timer > 0f)
        {
            float x = Random.Range(-1f, 1f) * shakepower;
            float y = Random.Range(-1f, 1f) * shakepower;

            transform.localPosition = playerpos + new Vector3(x, y, 0);

            timer -= Time.deltaTime;
            yield return null; 
        }

        transform.localPosition = playerpos;
    }
}
