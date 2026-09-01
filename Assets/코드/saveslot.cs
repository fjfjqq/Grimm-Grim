using UnityEngine;
using UnityEngine.SceneManagement;

public class NewEmptyCSharpScript : MonoBehaviour
{
    [SerializeField] private int numberofslot; // 슬롯 넘버

    private void OnMouseDown()
    {
        PlayerPrefs.SetInt("loadslot", numberofslot);

        mapsavefile mapdata = saveload.Loadmap(numberofslot);

        if(mapdata != null)
        {
            SceneManager.LoadScene("Chapter" + mapdata.chapter + "-" + mapdata.mapnumber); //데이터가 있으면 저장된 맵으로 이동
        }
        else
        {
            SceneManager.LoadScene("Chapter" + 1 + "-" + 1); //데이터 없으면 새게임이니깐 1에서 시작
        }
    }
}
