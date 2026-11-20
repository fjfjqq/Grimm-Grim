using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 8f;
    public int damage = 1;
    public float lifetime = 5f;
    public LayerMask wall;

    private Vector2 dir;
    private float timer;

    public void setup(Vector2 direc)
    {
        dir = direc.normalized;
        timer = lifetime;

        float angel = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; //이거 대충 삼각함수랑 호도법으로 구한건데 자세히 설명하려면 너무 주석이 길어지니 대충 회전 및 조준 기능
        transform.rotation = Quaternion.Euler(0, 0, angel); //대충 세갈래로 나가는건지라 앵글에 따라서 달라지게

    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            bulletsaver.instance.Returnbullet(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D iwantsleep)
    {
        if (iwantsleep.CompareTag("Player"))
        {
            chardata player = iwantsleep.GetComponent<chardata>();

            if(player != null)
            {
                player.damagesystem(transform.position);
            }   

            bulletsaver.instance.Returnbullet(gameObject);
        }

        if (iwantsleep.CompareTag("Ground"))
        {
            bulletsaver.instance.Returnbullet(gameObject);
        }
    }
}
