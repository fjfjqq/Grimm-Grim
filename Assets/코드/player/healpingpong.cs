using UnityEngine;

public class healpingpong : MonoBehaviour 
{
    public void Healend()
    {
        if (bettaryheal.main != null)
        {
            bettaryheal.main.Healend();
        }
    }
}
