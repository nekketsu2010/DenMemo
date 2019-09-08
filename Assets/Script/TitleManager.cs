using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NCMB;

public class TitleManager : MonoBehaviour {
    public Text titleText;
    public GameObject causionPanel;
    public GameObject TDUcausionPanel;

    public Image progressBar;

    private float angularFrequency = 5f;
    private float time = 0.0f;
    private float deltaTime = 0.0333f;

    private Coroutine coroutine;

	// Use this for initialization
	void Start () {
        //NCMBUser.LogOutAsync();
        StartCoroutine(flashTitleText());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToNextScene()
    {
        //ネットワークに接続されていないときの警告
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            causionPanel.SetActive(true);
            return;
        }

        if(coroutine == null)
        {
            coroutine = StartCoroutine(getMasterURL());
        }
    }

    IEnumerator flashTitleText()
    {
        while (true)
        {
            time += angularFrequency * deltaTime;
            Color color = titleText.color;
            color.a = Mathf.Sin(time) * 0.5f + 0.5f;
            titleText.color = color;
            yield return new WaitForSeconds(deltaTime);
        }
    }

    IEnumerator getMasterURL()
    {
        float timeOut = 10; //タイムアウト
        WWW www = new WWW(ShareData.redirectURL);
        var endTime = Time.realtimeSinceStartup + timeOut;
        while (!www.isDone && Time.realtimeSinceStartup < endTime)
        {
            progressBar.fillAmount += 0.01f;
            yield return new WaitForSeconds(0.1f);
        }

        if (!string.IsNullOrEmpty(www.error) || !www.isDone)
        {
            //電大のWi-Fiに接続する警告を出す
            Debug.Log(www.error);
            TDUcausionPanel.SetActive(true);
        }
        else
        {
            ShareData.masterURL = www.text;
            Debug.Log(ShareData.masterURL);

            //プレイヤーの状況で次のシーンを決める
            //ユーザーIDが登録されているか
            SaveData.userID = PlayerPrefs.GetString("a", "");
            if (!string.IsNullOrEmpty(SaveData.userID))
            {
                //データが存在したとき
                //ここでデータのロード
                NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("SaveData");
                query.WhereEqualTo("userID", SaveData.userID);
                query.Find((List<NCMBObject> objects, NCMBException error) =>
                {
                    if (error != null)
                    {
                    }
                    else
                    {
                        NCMBObject savedata = objects[0];
                        UserData user = FileController.Load<UserData>(savedata["savedata"].ToString());
                        SceneManager.LoadScene("MainMenu"); //直接メインメニューへ
                    }
                });
            }
            //はじめてのとき
            else
            {
                //新規登録画面へ
                SceneManager.LoadScene("RegistScene");
            }

        }
    }
}
