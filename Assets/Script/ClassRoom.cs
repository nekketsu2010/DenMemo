using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassRoom : ScriptableObject {
    public List<Param> param = new List<Param>();

    [System.Serializable]
    public class Param
    {
        public int num;
        public string roomName;
    }
}
