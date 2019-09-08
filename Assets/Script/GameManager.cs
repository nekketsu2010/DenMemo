using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using NCMB;

public class GameManager : MonoBehaviour {
    private wifiInfo wifiInfo;

    public Button accessButton;
    public Text userName;
    public Text myAccessButton;
    public Text accessStation;
    public Image professorImage;
    public Image progressBar;
    public GameObject accessPanel;
    public GameObject contents; //originPanelの親
    public RectTransform originPanel; //画面の前面に出す履歴のやつのオリジナルパネル
    public GameObject DBcontents;
    public RectTransform originDBTexts;
    public GameObject myDBcontents;
    public RectTransform originMyDBTexts;

    private Monster monster;
    private Monster.Param param;
    private string statusText;
    private int userCount = 0; //ユーザがマスターの部屋の数

    // Use this for initialization
    void Start () {
        param = UserData.HaveNames[0];
        professorImage.sprite = Resources.Load<Sprite>("jewel/image/card/" + param.id);

        userName.text = UserData.userName;
        //renewDB();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void AccessTapped()
    {
        StartCoroutine(wifiGet());
        accessButton.enabled = false;
    }

    IEnumerator wifiGet()
    {
        //wifi情報を取得
        wifiInfo = new wifiInfo();

        yield return new WaitForSeconds(0.5f);

        //サーバーに予測させる
        progressBar.gameObject.SetActive(true);
        WWWForm form = new WWWForm();
        string result = wifiInfo.getWifi();
        form.AddField("rssi", result);
        WWW www = new WWW(ShareData.masterURL  + "predict", form);
        while (!www.isDone)
        { // ダウンロードの進捗を表示
            progressBar.fillAmount = www.progress;
            yield return null;
        }
        progressBar.fillAmount = 0;
        progressBar.gameObject.SetActive(false);

        if (www.error == null)
        {
            Debug.Log(www.text);
            result = www.text;
            //部屋番号変換の前にデータベースにアクセスして陣地取り処理を行う
            //部屋番号が返却されていれば文字数は少ないので
            if (result.Length <= 10)
            {
                //データベースから部屋番号で検索
                //QueryTestを検索するクラスを作成
                NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Room");
                //Scoreの値が7と一致するオブジェクト検索
                query.WhereEqualTo("Number", WifiRSSI.isRoomNumber(result));
                query.FindAsync((List<NCMBObject> objList, NCMBException e) => {
                    if (e != null)
                    {
                        //検索失敗時の処理
                        Debug.Log("失敗した！");
                    }
                    else
                    {
                        //Numberがresultのオブジェクトを出力（当然一個しか出ない＜被りがないからね＞）
                        foreach (NCMBObject obj in objList)
                        {
                            //データを取り出す
                            string masterName = "";
                            //誰も入っていないとき
                            if (obj["Master"] != null)
                            {
                                masterName = obj["Master"].ToString();
                            }
                            int restHp = System.Convert.ToInt32(obj["RestHP"]);
                            int defense = System.Convert.ToInt32(obj["DEF"]);
                            Debug.Log("objectId:" + obj["Number"]);

                            //ダメージ計算を行う
                            //DEF/2を自分のATKから引いたダメージだけ与えることにする（とりあえず）
                            int ATK = param.Attack - defense / 2;
                            restHp -= ATK;
                            //現マスターの残りHPが0以下になったら、もしくはそもそもマスター居ないならマスター交代
                            if (restHp <= 0 || string.IsNullOrEmpty(masterName))
                            {
                                obj["Master"] = UserData.userName;
                                obj["MasterID"] = UserData.userID;
                                obj["RestHP"] = param.Hp;
                                obj["DEF"] = param.Defense;
                                statusText = "新しくあなたがマスターになりました！";
                            }
                            else if (obj["MasterID"].ToString() != UserData.userID)
                            {
                                obj["RestHP"] = restHp;
                                statusText = "マスターに " + ATK + " のダメージを与えました！";
                            }
                            else
                            {
                                statusText = "すでにあなたがマスターです！";
                            }
                            obj.Save();
                        }
                    }
                });
            }

            result = WifiRSSI.isRoom(result);
        }
        else
        {
            Debug.LogError(www.error);
            Debug.Log("うんち");
        }
        accessButton.enabled = true;
        Debug.Log(result);

        accessStation.text = result;
        accessPanel.SetActive(true);
    }

    //アクセスパネルを消したとき
    public void DeleteAccessPanel()
    {
        //アクセスパネルのテキストを写してInstantiate
        Text[] texts = originPanel.GetComponentsInChildren<Text>();
        foreach(Text text in texts)
        {
            if (text.gameObject.name == "StatusText")
            {
                text.text = statusText;
            }
            if (text.gameObject.name == "StationText")
            {
                text.text = accessStation.text;
            }
        }
        RectTransform panel = GameObject.Instantiate<RectTransform>(originPanel);
        panel.transform.parent = contents.transform;
        panel.SetAsFirstSibling();
        panel.gameObject.SetActive(true);
        renewDB();
    }

    //現在のマスターの一覧を更新
    public void renewDB()
    {
        //contentsの子供を消す処理（元だけは消さない）
        for (int i = 0; i < DBcontents.transform.childCount; i++)
        {
            GameObject child = DBcontents.transform.GetChild(i).gameObject;
            if (child.name != originDBTexts.name)
            {
                Destroy(child.gameObject);
            }
        }
        for (int i = 0; i < myDBcontents.transform.childCount; i++)
        {
            GameObject child = myDBcontents.transform.GetChild(i).gameObject;
            if (child.name == originMyDBTexts.name)
            {
                Destroy(child.gameObject);
            }
        }
        userCount = 0;
        //DBにアクセスして上から下までデータいただく
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Room");
        query.OrderByAscending("count");
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e == null)
            {
                //データ取得成功時
                foreach (NCMBObject obj in objList)
                {
                    //マスターが自分だった場合
                    //if (obj["MasterID"].ToString() == UserData.userID)
                    //{
                    //    userCount++;
                    //    foreach (Text text in originMyDBTexts.GetComponentsInChildren<Text>())
                    //    {
                    //        if (text.gameObject.name == "RoomNameText")
                    //        {
                    //            text.text = WifiRSSI.isRoom(obj["Number"].ToString());
                    //        }
                    //        if (text.gameObject.name == "MasterText")
                    //        {
                    //            if (string.IsNullOrEmpty(obj["Master"].ToString()))
                    //            {
                    //                text.text = "現在空いています！！！";
                    //            }
                    //            else
                    //            {
                    //                text.text = obj["Master"].ToString();
                    //            }
                    //        }
                    //        if (text.gameObject.name == "HPText")
                    //        {
                    //            text.text = obj["RestHP"].ToString();
                    //        }
                    //    }
                    //    RectTransform myPanel = GameObject.Instantiate<RectTransform>(originMyDBTexts);
                    //    myPanel.transform.parent = myDBcontents.transform;
                    //    myPanel.gameObject.SetActive(true);
                    //}

                    //ひとつずつ出力！
                    foreach (Text text in originDBTexts.GetComponentsInChildren<Text>())
                    {
                        if (text.gameObject.name == "RoomNameText")
                        {
                            text.text = WifiRSSI.isRoom(obj["roomNumber"].ToString());                        
                        }
                        if (text.gameObject.name == "MasterText")
                        {
                            if (string.IsNullOrEmpty(obj["userID"].ToString()))
                            {
                                text.text = "現在空いています！！！";
                            }
                            else
                            {
                                text.text = obj["userID"].ToString();
                            }
                        }
                        if (text.gameObject.name == "HPText")
                        {
                            //text.text = obj["RestHP"].ToString();
                        }                      
                    }
                    RectTransform panel = Instantiate<RectTransform>(originDBTexts);
                    panel.transform.parent = DBcontents.transform;
                    panel.gameObject.SetActive(true);
                }
                myAccessButton.text = "MyAccess\n" + userCount;
            }
            else
            {
                Debug.LogError("データベースエラー！ " + e.ErrorCode);
            }
        });
    }

}
