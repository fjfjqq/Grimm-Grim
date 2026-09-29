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
        gameObject.SetActive(false);
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
        if(player.nowmoney >= itemitemlist[i].price)
        {
            player.nowmoney -= itemitemlist[i].price;
        }
        else
        {

        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
