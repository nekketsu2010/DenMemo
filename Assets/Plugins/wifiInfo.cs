using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wifiInfo : MonoBehaviour {
    private string[] SSIDs;
    private string[] BSSIDs;
    private int[] RSSIs;
    private int[] Frequencies;

    public wifiInfo()
    {
#if UNITY_ANDROID
        AndroidJavaClass ssidManager = new AndroidJavaClass("com.gamecompletecompany.nativeplugin.NativeMethod");

        // Context(Activity)オブジェクトを取得する
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        context.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {

            // staticメソッドを呼び出す
            AndroidJavaObject results = ssidManager.CallStatic<AndroidJavaObject>("getScanResults", context);
            SSIDs = ssidManager.CallStatic<string[]>("getSSID", results);
            BSSIDs = ssidManager.CallStatic<string[]>("getBSSID", results);
            RSSIs = ssidManager.CallStatic<int[]>("getRSSI", results);
            Frequencies = ssidManager.CallStatic<int[]>("getFrequency", results);
        }));
#endif
    }

    public int Length() { return SSIDs.Length; }
    public string[] getSSIDs() { return SSIDs; }
    public string[] getBSSIDs() { return BSSIDs; }
    public int[] getRSSIs() { return RSSIs; }
    public int[] getFrequencies() { return Frequencies; }
    public string getSSID(int i) { return SSIDs[i]; }
    public string getBSSID(int i) { return BSSIDs[i]; }
    public int getRSSI(int i) { return RSSIs[i]; }
    public int getFrequency(int i) { return Frequencies[i]; }

    public string getWifi()
    {
        string result = "";
        for (int i = 0; i < SSIDs.Length; i++)
        {
            result += SSIDs[i] + "," + BSSIDs[i] + "," + RSSIs[i] + "," + Frequencies[i];
            if(i != SSIDs.Length - 1)
            {
                result += "\n";
            }
        }
        return result;
    }

}
