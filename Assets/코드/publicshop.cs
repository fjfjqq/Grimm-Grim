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
            Debug.Log("돈 x");
            return;
        }

        Weapon buyweapon = player.Weaponloadout(itemitemlist[i].itemname);

        if(buyweapon == null)
        {
            Debug.Log("무기 x");
            return;
        }

        for(int ii = 0; ii < player.weaponeslot.Length; ii++)
        {
            if (player.weaponeslot[ii] == buyweapon)
            {
                Debug.Log("보유중");
                return;
            }
        }

        for(int ii = 0; ii < player.weaponeslot.Length; ii++)
        {
            if (player.weaponeslot[ii] == null)
            {
                player.weaponeslot[ii] = buyweapon;
                player.nowmoney -= itemitemlist[i].price;

                Debug.Log("구매 완");
                Debug.Log("현재돈" + player.nowmoney);


                return;
            }
        }

        Debug.Log("빈 무기 슬롯이 없자너");
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
