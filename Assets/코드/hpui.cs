using UnityEngine;
using UnityEngine.UI;

public class hpui : MonoBehaviour
{
    public GameObject heart;
    public Transform hpvessel;
    public chardata user;

    public Image[] heartsystem;
    void Start()
    {
        heartsystem = new Image[user.maxhp];

        for(int i = 0; i < user.maxhp; i++)
        {
            GameObject copyhp = Instantiate(heart, hpvessel);

            heartsystem[i] = copyhp.GetComponent<Image>();
        }
    }

    void Update()
    {
        for(int i = 0; i < heartsystem.Length; i++)
        {
            if(i < user.nowhp)
            {
                heartsystem[i].color = Color.red;
            }
            else
            {
                heartsystem[i].color = Color.white;
            }
        }
    }
}
