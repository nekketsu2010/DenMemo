using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WifiRSSI : MonoBehaviour {
    
    public static int isRoomNumber(string roomNum)
    {
        int roomNumber = ((int)double.Parse(roomNum));
        return roomNumber;
    }

    public static string isRoom(string roomNum)
    {
        roomNum = ((int)double.Parse(roomNum)).ToString();

        for (int i = 0; i < ShareData.rooms.Count; i++)
        {
            if (roomNum == ShareData.rooms[i].getRoomNumber())
            {
                roomNum = ShareData.rooms[i].getRoomName();
                break;
            }
        }

        return roomNum;
    }

    public static string isRoomNum(string roomNum)
    {
        for (int i = 0; i < ShareData.rooms.Count; i++)
        {
            if (roomNum == ShareData.rooms[i].getRoomNumber())
            {
                roomNum = ShareData.rooms[i].getRoomNumber();
                break;
            }
            //合致するのがない場合
            if (i == ShareData.rooms.Count - 1)
            {
                return ""; //わかりやすいように空文字を返す
            }
        }

        return roomNum;
    }
}
