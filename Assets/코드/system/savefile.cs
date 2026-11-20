using UnityEngine;
[System.Serializable]
public class savefile
{
    public int nowhp;
    public int maxhp;
    public int nowmoney;
    public float posx;
    public float posy;
    public string[] slotofweapon; //chardata에 있는거랑 혼돈 안돼게 주의
    public string nowweaponname;
}

[System.Serializable]
public class mapsavefile
{
    public int chapter;
    public int mapnumber;

}
