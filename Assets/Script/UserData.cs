using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour {
    public static string userName;
    public static string userID;
    public static int rank;
    public static int exp;
    public static List<Monster.Param> HaveNames = new List<Monster.Param>(); //持っている教授の名前
}
