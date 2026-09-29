using UnityEngine;

public class stagewarppoint : MonoBehaviour
{
    [SerializeField] private Transform[] warppoint;
    [SerializeField] private Transform player;

    private void Start()
    {
        string warpwich = PlayerPrefs.GetString("warppoint");

        foreach(Transform warpwarpwich in warppoint)
        {
            if(warpwarpwich.name  == warpwich)
            {
                player.position = warpwarpwich.position;
                break;
            }
        }
    }
}
