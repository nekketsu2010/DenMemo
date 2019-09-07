using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "MyGame/Create MonsterTable", fileName = "MonsterTable")]
public class Monster : ScriptableObject {
    public List<Param> param = new List<Param>();

    [System.Serializable]
    public class Param
    {
        public string Name;
        public Sprite Image;
        public int Level = 1;
        public int Hp;
        public int Attack;
        public int Defense;
    }
}
