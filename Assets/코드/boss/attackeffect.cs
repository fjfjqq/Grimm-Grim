using UnityEngine;

public static class attackeffect //굳이 저장해놓을게 없어서 좀 더 간편한 스태틱
{
    public static GameObject Spawn(GameObject prefab, Vector2 start, Vector2 end, float baselength, float life)
    {
        if (prefab == null)
        {
            return null;
        }

        Vector2 center = (start + end) * 0.5f;
        Vector2 dir = end - start;
        float length = dir.magnitude;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        GameObject effect = Object.Instantiate(prefab, center, Quaternion.Euler(0f, 0f, angle)); //내가 이해한게 맞고 내가 의도한대로 내가 코딩을 잘했다면 a랑 b가 있고 그 사이의 가운데 위치와 기울기 각도 필요길이를 계산해서 이펙트를 만드는것

        if (baselength > 0f)
        {
            Vector3 scale = effect.transform.localScale;
            scale.x = scale.x * (length / baselength);
            effect.transform.localScale = scale;
        }

        if (life > 0f)
        {
            Object.Destroy(effect, life);
        }

        return effect;
    }
}
