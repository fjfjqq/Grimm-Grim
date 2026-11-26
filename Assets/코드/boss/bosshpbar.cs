using UnityEngine;
using UnityEngine.UI;

public class bosshpbar : MonoBehaviour
{
    public GameObject barrot;
    public Image fill;
    public float fillspeed = 5f;

    private enemy target;
    private float showhp = 1f;

    public void Show(enemy boss)
    {
        target = boss;
        showhp = 1f;

        if(fill != null)
        {
            fill.fillAmount = 1f;
        }

        if(barrot != null)
        {
            barrot.SetActive(true);
        }
    }

    public void Hide()
    {
        target = null;

        if(barrot != null)
        {
            barrot.SetActive(false);
        }
    }

    private void Update()
    {
        if(target == null)
        {
            return;
        }

        if(fill == null)
        {
            return;
        }

        float realhp = (float)target.nowhp / (float)target.maxhp;

        if(realhp < 0f)
        {
            realhp = 0f;
        }

        showhp = Mathf.Lerp(showhp, realhp, Time.deltaTime * fillspeed);
        fill.fillAmount = showhp;
    }
}
