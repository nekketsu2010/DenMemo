using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable()]
public class SaveData {
    public string userName;
    public string userID;
    public int rank;
    public int exp;
    public List<Monster.Param> HaveNames = new List<Monster.Param>(); //持っている教授の名前

    public static void Load(SaveData data)
    {
        UserData.userName = data.userName;
        UserData.userID = data.userID;
        UserData.rank = data.rank;
        UserData.exp = data.exp;
        UserData.HaveNames = data.HaveNames;
    }

    public static void Save(SaveData data)
    {
        data.userName = UserData.userName;
        data.userID = UserData.userID;
        data.rank = UserData.rank;
        data.exp = UserData.exp;
        data.HaveNames = UserData.HaveNames;
    }
}
