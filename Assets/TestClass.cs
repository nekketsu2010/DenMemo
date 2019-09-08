using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using System.Threading.Tasks;
using System.IO;
using NCMB.Tasks;
using System;

public class TestClass : MonoBehaviour {

    // Use this for initialization
    void Start () {
		//RoomDB();
		//AccessDB();
	}

	void RoomDB()
    {
        TextAsset csvFile = Resources.Load("room_db") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);
        // クラスのNCMBObjectを作成
        // オブジェクトに値を設定
        string line;
		int i = 1;
        List<NCMBObject> objects = new List<NCMBObject>();
        while ((line = reader.ReadLine()) != null)
        {
            string[] csv = line.Split(',');
            NCMBObject testClass = new NCMBObject("Room");
			testClass["count"] = i;
            testClass["roomGoukan"] = csv[0];
            testClass["roomFloor"] = csv[1];
            testClass["roomNumber"] = csv[2];
            testClass["roomNane"] = csv[3];
            objects.Add(testClass);
			i++;
        }
        SaveDB(objects);
        Debug.Log("完了！");
    }

    void AccessDB()
	{
		TextAsset csvFile = Resources.Load("room_db") as TextAsset;
		StringReader reader = new StringReader(csvFile.text);
		// クラスのNCMBObjectを作成
		// オブジェクトに値を設定
		string line;
		int i = 1;
		List<NCMBObject> objects = new List<NCMBObject>();
		while ((line = reader.ReadLine()) != null)
		{
			string[] csv = line.Split(',');
			NCMBObject testClass = new NCMBObject("Access");
			testClass["count"] = i;
			testClass["userID"] = "";
			testClass["roomNumber"] = csv[2];
			objects.Add(testClass);
			i++;
		}
		SaveDB(objects);
		Debug.Log("完了！");
	}

	public async void SaveDB(List<NCMBObject> objects)
    {
        try
        {
            List<Task<NCMBObject>> tasks = new List<Task<NCMBObject>>();
            foreach(NCMBObject nCMBObject in objects)
            {
                Task<NCMBObject> task = NCMBObjectTaskExtension.SaveTaskAsync(nCMBObject);
                tasks.Add(task);
            }
            await Task.WhenAll(tasks);
            Debug.Log("全部できた");
        }catch(NCMBException e)
        {
            Debug.LogWarning("エラー！" + e.ErrorCode);
        }
    }

}
