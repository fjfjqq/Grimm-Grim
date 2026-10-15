using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class publicshop : MonoBehaviour
{
    [SerializeField] private GameObject itemfreeset;
    [SerializeField] private Transform itemnpc;
    [SerializeField] private chardata player;

    private shopitem[] itemitemlist;

    public void Open(shopitem[] items)
    {
        Debug.Log("Open 호출됨, 품목 수: " + items.Length);
        gameObject.SetActive(true);
        itemitemlist = items;

        foreach(Transform button in itemnpc)
        {
            Destroy(button.gameObject);
        }

        for(int i = 0; i < items.Length; i++)
        {
            GameObject makebutton = Instantiate(itemfreeset, itemnpc);

            makebutton.GetComponentInChildren<TextMeshProUGUI>().text = items[i].itemname + "-" + items[i].price + "원";
            makebutton.GetComponentInChildren<Image>().sprite = items[i].icon;

            int itemcount = i;
            makebutton.GetComponent<Button>().onClick.AddListener(() => Buy(itemcount));  
        }
    }

    public void Buy(int i)
    {
        if(player.nowmoney < itemitemlist[i].price)
        {
            return;
        }

        Weapon buyweapon = player.Weaponloadout(itemitemlist[i].itemname);

        if(buyweapon == null)
        {
            return;
        }

        for(int ii = 0; ii < player.weaponeslot.Length; ii++)
        {
            if (player.weaponeslot[ii] == buyweapon)
            {
                return;
            }
        }

        for(int ii = 0; ii < player.weaponeslot.Length; ii++)
        {
            if(player.weaponeslot[ii] == buyweapon)
            {
                player.weaponeslot[ii] = buyweapon;
                player.nowmoney -= itemitemlist[i].price;
                return;
            }
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
