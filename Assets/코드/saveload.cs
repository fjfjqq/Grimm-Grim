using UnityEngine;
using System.IO;

public class saveload : MonoBehaviour
{
    [SerializeField] private chardata player; //chardata에 있는 플레이어 데이터 가져오는거

    public void Gamesave(int mapnumber) //맵 숫자 받아서 로드할 위치 
    {
        int slot = PlayerPrefs.GetInt("slotnumber", 1); //타이틀에서 골랐던 슬롯 번호 없으면 1 반환

        savefile data = new savefile(); //쉽게 생각해서 저장을 할 상자 만들어두기 여기에 플레이어 데이터를 집어넣어서 박스채로 운반
        data.maxhp = player.maxhp;
        data.nowhp = player.nowhp;
        data.nowmoney = player.nowmoney;
        data.posx = player.transform.position.x;
        data.posy = player.transform.position.y; 

        data.slotofweapon = new string[player.weaponeslot.Length];
        for(int i = 0; i < player.weaponeslot.Length; i++) //무기 슬롯 3개를 새로 만든 배열에다가 순서대로 저장
        {
            if (player.weaponeslot[i] != null)
            {
                data.slotofweapon[i] = player.weaponeslot[i].weaponename; //이게 중요함 클래스 자체를 내가 저장할수 없음 그래서 생각해낸 방법이 
            }
            else
            {
                data.slotofweapon[i] = "";
            }
        }

        mapsavefile mapdata = new mapsavefile();
        mapdata.mapnumber = mapnumber;

        string json = JsonUtility.ToJson(data); //플레이어 데이터를 json(파일)로 변환[아직 이해를 못한쪽이라서 더 파봐야함]
        string saveroute = Path.Combine(Application.persistentDataPath, "save" + slot + ".json"); //원래 path 콤바인 안 해보니깐 써야지 슬래쉬가 잘못들어가는 버그를 해결할수 있다해서 그렇게 바꿈 저장경로
        File.WriteAllText(saveroute, json); //텍스트 파일로 최종 저장 *오버라이팅 주의할것*

        string mapjson = JsonUtility.ToJson(mapdata); //맵 데이터를 json형태의 문자열 string으로 변함
        string mapsaveroute = Path.Combine(Application.persistentDataPath, "savemap" + slot + ".json"); //path 콤바인 사용해서 /자동처리로 경로 생성
        File.WriteAllText(mapsaveroute, mapjson); //텍스트 파일로 최종 저장 

    }

    public void Gameload()
    {
        int slot = PlayerPrefs.GetInt("loadslot", 0); //타이틀에서 골랐던 슬롯 가져오기 없으면 0이라는 뜻
        if (slot <= 0) return; //0이면 안골랐으니깐 리턴 하고 끝내기

        string saveroute = Path.Combine(Application.persistentDataPath, "save" + slot + ".json"); //파일 경로 읽기
        if (!File.Exists(saveroute)) return; //파일이 없으면 새게임이니깐 아무것도 안하고 리턴, 파일이 있으면 넘어가고 파일 읽기 시작

        string json = File.ReadAllText(saveroute); //파일을 문자열로 읽기 그래서 스트링으로
        savefile data = JsonUtility.FromJson<savefile>(json); //json으로 되있는 텍스트를 다시 아까 만들어놨던 savefile로 변환시키기

        player.maxhp = data.maxhp;
        player.nowhp = data.nowhp;
        player.nowmoney = data.nowmoney;
        player.transform.position = new Vector3(data.posx, data.posy, 0);

        if(data.slotofweapon != null) //저장된 무기 데이터 있는지 확인
        {
            for (int i = 0; i < data.slotofweapon.Length; i++)
            {
                if (data.slotofweapon[i] != "") //i번째 슬롯이 빈칸이 아니라는걸 확인
                {
                    player.weaponeslot[i] = Weaponloadout(data.slotofweapon[i]); //지정된 이름으로 웨폰 로드아웃 호출하는거
                }
                else
                {
                    player.weaponeslot[i] = null; //비어있으면 null값 반환
                }
            }
            
        }

        if(data.nowweaponname != null && data.nowweaponname != "")
        {
            player.nowweapon = Weaponloadout(data.nowweaponname);
        }


    }

    private Weapon Weaponloadout(string weaponnamecheck) //찾으면 웨폰 반환
    {
        for(int i = 0; i < player.allweapon.Length; i++) //마찬가지로 올웨폰 배열 끝까지 확인하기
        {
            if (player.allweapon[i] != null && player.allweapon[i].weaponename == weaponnamecheck) //배열의 1번째 칸에 무기가 있는지 확인 후 그 무기와 같은 이름이 있는지 확인후 불러오기
            {
                return player.allweapon[i]; //weapon 함수 반환해주기
            }
        }

        return null; // 안넣었더니 null값일 경우 무한반복하면서 에러
    }

    public static mapsavefile Loadmap(int slot)
    {
        string mappath = Path.Combine(Application.persistentDataPath, "savemap" + slot + ".json");
        if (File.Exists(mappath))
        {
            string json = File.ReadAllText(mappath);
            return JsonUtility.FromJson<mapsavefile>(json);
        }

        return null;
    }
}
