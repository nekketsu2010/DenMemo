using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using System.Threading.Tasks;

public class TestClass : MonoBehaviour {

    // Use this for initialization
    void Start () {
        ClassRoom room = Resources.Load<ClassRoom>("Room");
        // クラスのNCMBObjectを作成
        // オブジェクトに値を設定
        for (int i = 0; i < room.param.Count; i++)
        {
            NCMBObject testClass = new NCMBObject("Room");
            testClass["Count"] = i + 1;
            testClass["Number"] = room.param[i].num;
            testClass["DEF"] = 0;
            testClass["RestHP"] = 0;
            testClass["Master"] = "";
            testClass["MasterID"] = "";
            testClass.Save((NCMBException e) =>
            {
                if (e != null)
                {
                    Debug.Log("エラー！" + e.ErrorCode);
                }
                else
                {
                    Debug.Log("できた");
                }
            });
            Debug.Log(i + "回目できた");
        }
        Debug.Log("完了！");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
