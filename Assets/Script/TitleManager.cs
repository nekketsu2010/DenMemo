using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NCMB;

public class TitleManager : MonoBehaviour {
    public Text titleText;
    public GameObject causionPanel;
    private float angularFrequency = 5f;
    private float time = 0.0f;
    private float deltaTime = 0.0333f;
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

        //プレイヤーの状況で次のシーンを決める
        //ここでデータのロード
        SaveData data = FileController.Load<SaveData>("a");
        //データが存在したとき
        if (data != null)
        {
            SaveData.Load(data);
            SceneManager.LoadScene("MainMenu"); //直接メインメニューへ
        }
        //はじめてのとき
        else
        {
            //新規登録画面へ
            SceneManager.LoadScene("RegistScene");
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
}
