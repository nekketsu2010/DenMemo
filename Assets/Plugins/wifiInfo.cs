using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wifiInfo : MonoBehaviour {
    private string[] SSIDs;
    private string[] BSSIDs;
    private int[] RSSIs;

    public wifiInfo()
    {
#if UNITY_ANDROID
        AndroidJavaClass ssidManager = new AndroidJavaClass("com.lib.nekketsu.nativeplugin.NativeMethod");

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
        }));
#endif
    }

    public int Length() { return SSIDs.Length; }
    public string[] getSSIDs() { return SSIDs; }
    public string[] getBSSIDs() { return BSSIDs; }
    public int[] getRSSIs() { return RSSIs; }
    public string getSSID(int i) { return SSIDs[i]; }
    public string getBSSID(int i) { return BSSIDs[i]; }
    public int getRSSI(int i) { return RSSIs[i]; }
 
}
