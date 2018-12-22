using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSSID : ScriptableObject {
    public List<Param> param = new List<Param>();

    [System.Serializable]
    public class Param
    {
        public int num;
        public string bssid;
    }
}
