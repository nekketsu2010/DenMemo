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
        monster = Resources.Load<Monster>("Professor/ProfessorList");
    }

// Update is called once per frame
void Update () {
		
	}

    public void postInform(Image image)
    {
        SelectCanvas.SetActive(false);
        ConfirmCanvas.SetActive(true);
        this.image.sprite = image.sprite;
        if (image.gameObject.name.Contains("Ohyama"))
        {
            profileText.text = "Name : Ohyama\nHP : 15\nATK : 8\nDEF : 5";
            param = monster.param.Where(x => x.Name.Contains("大山")).First();
        }
        else if (image.gameObject.name.Contains("Kawakatsu"))
        {
            profileText.text = "Name : Kawakatsu\nHP : 15\nATK : 5\nDEF : 8";
            param = monster.param.Where(x => x.Name.Contains("川勝")).First();
        }
        else if (image.gameObject.name.Contains("Nemoto"))
        {
            profileText.text = "Name : Nemoto\nHP : 18\nATK : 6\nDEF : 6";
            param = monster.param.Where(x => x.Name.Contains("根本")).First();
        }
    }

    public void decideButtonTapped()
    {
        UserData.HaveNames.Add(param);
        print(param.Name);
        SceneManager.LoadScene("MainMenu");
    }
}
