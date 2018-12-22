using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "MyGame/Create EquipmentTable", fileName = "EquipmentTable")]
public class Equipment : ScriptableObject {
    public EquipmentParam equipmentParams = new EquipmentParam();

    [System.Serializable]
    public class EquipmentParam
    {
        public string name;
        public Sprite image;
        public int hp;
        public int attack;
        public int deffense;
    }
}
