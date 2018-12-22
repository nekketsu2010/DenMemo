using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NCMB;

public class RegistManager : MonoBehaviour {
    public InputField userNameText;
    public InputField passwordText;
    public Button okButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void valueChanged(string text)
    {
        if (!string.IsNullOrEmpty(userNameText.text) && !string.IsNullOrEmpty(passwordText.text))
        {
            okButton.interactable = true;
        }
        else
        {
            okButton.interactable = false;
        }
    }

    public void okButtonTapped()
    {
        //セーブ及び、データベースに登録
        UserData.userName = userNameText.text;
        NCMBUser user = new NCMBUser();
        user.UserName = UserData.userName;
        user.Password = passwordText.text;
        user.SignUpAsync((NCMBException e) =>
        {
            if (e != null)
            {
                Debug.Log("ユーザ登録失敗！: " + e.ErrorMessage);
            }
            else
            {
                Debug.Log("ユーザ登録完了");
                UserData.userID = user.ObjectId;
                //セーブ処理
                SaveData data = new SaveData();
                SaveData.Save(data);
                FileController.Save("a", data);
                //セーブ処理ここまで
                SceneManager.LoadScene("StartScene");
            }
        });
    }
}
