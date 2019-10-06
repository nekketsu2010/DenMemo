using System.Collections;
using System.Collections.Generic;
using NCMB;
using UnityEngine;

public class UserData {
    public static string userName;
    public static string userID;
    public static int rank;
    public static int exp;
    public static List<Monster.Param> HaveNames = new List<Monster.Param>(); //持っている教授の名前

    public static void Save()
    {
        SaveData data = new SaveData();
        data = SaveData.Save(data);
        string savedata = FileController.Save(data);
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("SaveData");
        query.WhereEqualTo("userID", userID);
        query.Find((List<NCMBObject> objects, NCMBException e) =>
        {
            if(e != null)
            {
                Debug.Log("データベース取得エラー！");
                return;
            }

            foreach (NCMBObject nCMBObject in objects)
            {
                nCMBObject["savedata"] = savedata;
                nCMBObject.Save();
            }

            if (objects.Count == 0)
            {
                //新規にデータ作成
                NCMBObject newData = new NCMBObject("SaveData");
                newData["userID"] = userID;
                newData["savedata"] = savedata;
                newData.Save();
            }
        });
    }
}
