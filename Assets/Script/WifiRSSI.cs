using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WifiRSSI : MonoBehaviour {
    public static BSSID bssid = Resources.Load<BSSID>("BSSID");
    public static ClassRoom room = Resources.Load<ClassRoom>("Room");

    //LibSVMに使うリストを作成
    public static string SVMList(wifiInfo wifiInfo)
    {
        //部屋番号をクラス名にする
        string roomName = "51018";

        int[] rssis = new int[bssid.param.Count];

        for (int i = 0; i < wifiInfo.Length(); i++)
        {
            for (int j = 0; j < bssid.param.Count; j++)
            {
                if(wifiInfo.getBSSID(i).Equals(bssid.param[j].bssid))
                {
                    rssis[j] = wifiInfo.getRSSI(i);
                    break;
                }
            }
        }

        for (int i = 0; i < rssis.Length; i++)
        {
            if (rssis[i] == 0)
            {
                //ないのでRSSIを-999として登録(どの端末でもあり得ないので)
                rssis[i] = -999;
            }
        }

        string result = roomName + " ";
        for (int i = 0; i < rssis.Length; i++)
        {
            if(rssis[i]==-999)
            {
                continue;
            }
            result += (i + 1) + ":" + rssis[i] + " ";
        }
        return result;
    }

    public static int isRoomNumber(string roomNum)
    {
        int roomNumber = ((int)double.Parse(roomNum));
        return roomNumber;
    }

    public static string isRoom(string roomNum)
    {
        roomNum = ((int)double.Parse(roomNum)).ToString();

        for (int i = 0; i < room.param.Count; i++)
        {
            if (roomNum == room.param[i].num.ToString())
            {
                roomNum = room.param[i].roomName;
                break;
            }
        }

        return roomNum;
    }

    public static string isRoomNum(string roomNum)
    {
        for (int i = 0; i < room.param.Count; i++)
        {
            if (roomNum == room.param[i].roomName)
            {
                roomNum = room.param[i].num.ToString();
                break;
            }
            //合致するのがない場合
            if (i == room.param.Count - 1)
            {
                return ""; //わかりやすいように空文字を返す
            }
        }

        return roomNum;
    }
}
