using UnityEngine;
using UnityEngine.SceneManagement;

public class nextstage : MonoBehaviour
{
    [SerializeField] private string stagename; //이거 하나하나 다 넣을수 없고 전 스테이지로 이동할수도 있으니깐 이름으로 받아서
    [SerializeField] private string warppoint;
    [SerializeField] private sceenblackout fade;

    private void OnTriggerEnter2D(Collider2D touch)
    {
        if (touch.CompareTag("Player"))
        {
            chardata Player = touch.GetComponent<chardata>();

            playersavesystem.nowhp = Player.nowhp;
            playersavesystem.maxhp = Player.maxhp;
            playersavesystem.nowmoney = Player.nowmoney;

            if(Player.nowweapon != null)
            {
                playersavesystem.nowweaponname = Player.nowweapon.weaponename;
            }

            playersavesystem.weaponslotname = new string[Player.weaponeslot.Length];

            for(int i = 0; i < Player.weaponeslot.Length; i++)
            {
                if(Player.weaponeslot[i] != null)
                {
                    playersavesystem.weaponslotname[i] = Player.weaponeslot[i].weaponename;
                }
                else
                {
                    playersavesystem.weaponslotname[i] = null;
                }
            }

            PlayerPrefs.SetString("warppoint", warppoint);
            fade.Gosceen(stagename);
        }
    }
}
