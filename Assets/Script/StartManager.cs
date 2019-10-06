using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour {
    public GameObject SelectCanvas;
    public GameObject ConfirmCanvas;
    public Image image;
    public Text profileText;

    private Monster monster;
    private Monster.Param param;
    private string name;

	// Use this for initialization
	void Start () {
        monster = Resources.Load<Monster>("MonsterTable");
    }

    // Update is called once per frame
    void Update () {
		
    }

    public void postInform(Image image)
    {
        SelectCanvas.SetActive(false);
        ConfirmCanvas.SetActive(true);
        this.image.sprite = image.sprite;
        if (image.gameObject.name.Contains("f001"))
        {
            param = monster.param.Where(x => x.id.Contains("f001")).First();
            profileText.text = "Name : " + param.Name + "\nHP : " + param.Hp + "\nATK : " + param.Attack + "\nDEF : " + param.Defense;
        }
        else if (image.gameObject.name.Contains("f003"))
        {
            param = monster.param.Where(x => x.id.Contains("f003")).First();
            profileText.text = "Name : " + param.Name + "\nHP : " + param.Hp + "\nATK : " + param.Attack + "\nDEF : " + param.Defense;
        }
        else if (image.gameObject.name.Contains("f023"))
        {
            param = monster.param.Where(x => x.id.Contains("f023")).First();
            profileText.text = "Name : " + param.Name + "\nHP : " + param.Hp + "\nATK : " + param.Attack + "\nDEF : " + param.Defense;
        }
    }

    public void decideButtonTapped()
    {
        UserData.HaveNames.Add(param);
        print(param.Name);
        UserData.Save();
        SceneManager.LoadScene("MainMenu");
    }
}
