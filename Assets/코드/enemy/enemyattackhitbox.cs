using UnityEngine;

public class enemyattackhitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            chardata player = collision.GetComponent<chardata>();
            if(player != null)
            {
                player.damagesystem(transform.position);
            }
        }
    }
}
