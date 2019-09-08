using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NCMB;

public class RegistManager : MonoBehaviour {
    [Header("Regist")]
    public InputField userIDText;
    public InputField mailAddressText;
    public InputField passwordText;
    public Button okButton;

    [Header("Login")]
    public InputField LuserIDText;
    public InputField LpasswordText;
    public Button LokButton;
    public GameObject incorrectText;


    public void valueChanged(string text)
    {
        if (!string.IsNullOrEmpty(userIDText.text) && !string.IsNullOrEmpty(mailAddressText.text) && !string.IsNullOrEmpty(passwordText.text))
        {
            okButton.interactable = true;
        }
        else
        {
            okButton.interactable = false;
        }
    }

    public void LvalueChanged(string text)
    {
        if (!string.IsNullOrEmpty(LuserIDText.text) && !string.IsNullOrEmpty(LpasswordText.text))
        {
            LokButton.interactable = true;
        }
        else
        {
            LokButton.interactable = false;
        }
    }

    public void okButtonTapped()
    {
        //セーブ及び、データベースに登録
        NCMBUser user = new NCMBUser();
        user.UserName = userIDText.text;
        user.Email = mailAddressText.text;
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
                //セーブ処理
                SaveData.userID = userIDText.text;
                PlayerPrefs.SetString("a", SaveData.userID);
                //セーブ処理ここまで
                SceneManager.LoadScene("StartScene");
            }
        });
    }

    public void LokButtonTapped()
    {
        //ユーザーを検索し，データをロード
        NCMBUser.LogInAsync(LuserIDText.text, LpasswordText.text, (NCMBException e) =>
        {
            if (e != null)
            {
                Debug.Log("ログインに失敗しました");
                incorrectText.SetActive(true);
            }
            else
            {
                SaveData.userID = LuserIDText.text;
                NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("SaveData");
                query.WhereEqualTo("userID", SaveData.userID);
                query.Find((List<NCMBObject> objects, NCMBException error) =>
                {
                    if (error != null)
                    {
                        incorrectText.SetActive(true);
                    }
                    else
                    {
                        //userIDをローカルセーブ
                        PlayerPrefs.SetString("a", SaveData.userID);

                        NCMBObject savedata = objects[0];
                        UserData user = FileController.Load<UserData>(savedata["savedata"].ToString());
                        SceneManager.LoadScene("MainMenu"); //直接メインメニューへ
                    }
                });
            }
        });
    }
}
