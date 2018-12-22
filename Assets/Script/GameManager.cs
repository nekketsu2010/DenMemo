using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using NCMB;

public class GameManager : MonoBehaviour {
    private wifiInfo wifiInfo;

    public Button accessButton;
    public Text userName;
    public Text myAccessButton;
    public Text accessStation;
    public Image professorImage;
    public Image progressBar;
    public GameObject accessPanel;
    public GameObject contents; //originPanelの親
    public RectTransform originPanel; //画面の前面に出す履歴のやつのオリジナルパネル
    public GameObject DBcontents;
    public RectTransform originDBTexts;
    public GameObject myDBcontents;
    public RectTransform originMyDBTexts;

    private Monster monster;
    private Monster.Param param;
    private string statusText;
    private int userCount = 0; //ユーザがマスターの部屋の数
    // Use this for initialization
    void Start () {
        monster = Resources.Load<Monster>("Professor/ProfessorList");
        professorImage.sprite = monster.param.Where(x => x.Name == UserData.HaveNames[0].Name).First().Image;
        param = monster.param.Where(x => x.Name == UserData.HaveNames[0].Name).First();
        print(param.Level);

        userName.text = UserData.userName;
        renewDB();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void AccessTapped()
    {
        StartCoroutine(wifiGet());
        accessButton.enabled = false;
    }

    IEnumerator wifiGet()
    {
        //wifi情報を取得
        wifiInfo = new wifiInfo();

        yield return new WaitForSeconds(0.5f);
        //svmリストの作成
        string result = WifiRSSI.SVMList(wifiInfo);
        Debug.Log(result);
        //Scale s = new Scale(result);
        //yield return new WaitForSeconds(3f);
        //Debug.Log("正規化 " + s.getResult());
        //Predict p = new Predict(result/*,room*/);

        //サーバーに予測させる
        Debug.Log("ページ読み込むよ");
        progressBar.gameObject.SetActive(true);
        string url = "http://a0f90791.ngrok.io/sample-game-server/libsvm/predict";
        WWWForm form = new WWWForm();
        //string rssi = "5611.0 1:1.0 3:1.0 4:1.0 5:-0.0786516853932584 6:1.0 9:1.0 10:1.0 12:1.0 13:1.0 14:1.0 20:1.0 23:1.0 24:1.0 25:1.0 26:-0.8857142857142857 27:-1.0 28:0.1954022988505748 31:-1.0 33:-0.1235955056179775 35:1.0 36:1.0 37:0.5 38:-0.75 39:-0.19999999999999996 42:1.0 43:1.0 44:-0.06666666666666665 45:1.0 47:1.0 48:1.0 49:1.0 53:1.0 55:1.0 58:1.0 60:1.0 61:1.0 62:1.0 64:1.0 68:-0.92 69:1.0 70:1.0 71:1.0 73:1.0 75:1.0 76:1.0 78:1.0 79:1.0 80:1.0 82:1.0 85:1.0 87:1.0 90:1.0 94:1.0 96:1.0 97:1.0 98:1.0 103:1.0 106:1.0 107:1.0 108:1.0 112:1.0 113:1.0 114:1.0 115:1.0 118:-0.6483516483516483 119:1.0 122:1.0 123:1.0 124:1.0 125:1.0 127:-0.8987341772151899 128:-0.975 129:1.0 130:1.0 131:1.0 132:1.0 133:-0.825 134:1.0 137:-1.0 138:1.0 140:-0.9384615384615385 143:1.0 144:-1.0 146:1.0 147:-1.0 151:1.0 152:1.0 154:1.0 158:1.0 159:-0.9230769230769231 162:1.0 167:1.0 169:1.0 170:-1.0 172:1.0 175:1.0 177:1.0 178:-1.0 180:1.0 182:1.0 197:1.0 199:1.0 201:-0.5802469135802469 204:1.0 206:1.0 207:1.0 209:1.0 211:1.0 213:1.0 225:1.0 230:1.0 232:1.0 233:1.0 236:1.0 239:-0.8987341772151899 240:1.0 243:1.0 246:1.0 247:1.0 249:1.0 251:1.0 254:1.0 255:1.0 259:1.0 260:1.0 261:1.0 262:1.0 264:1.0 265:1.0 270:1.0 271:1.0 280:1.0 281:1.0 282:-0.6296296296296297 283:1.0 284:1.0 285:1.0 290:1.0 291:1.0 293:-0.7142857142857143 294:1.0 296:-1.0 298:1.0 302:1.0 304:1.0 305:-1.0 318:1.0 321:1.0 324:1.0 326:1.0 330:1.0 331:1.0 337:1.0 339:1.0 341:1.0 349:1.0 351:1.0 353:1.0 357:1.0 359:1.0 360:1.0 364:1.0 368:1.0 369:1.0 370:1.0 373:1.0 376:1.0 378:1.0 379:1.0 392:1.0 394:1.0 401:1.0 402:1.0 405:1.0 406:1.0 407:1.0 408:1.0 410:1.0 416:1.0 417:1.0 418:1.0 419:1.0 420:1.0 423:1.0 424:1.0 425:1.0 426:1.0 428:1.0 429:1.0 430:1.0 431:1.0 435:1.0 437:1.0 438:1.0 443:1.0 446:1.0 447:1.0 449:1.0 450:1.0 452:1.0 454:1.0 455:1.0 461:1.0 462:1.0 465:1.0 468:1.0 472:1.0 473:1.0 474:1.0 475:1.0 477:1.0 480:1.0 482:1.0 490:1.0 492:1.0 499:1.0 503:1.0 504:1.0 513:1.0 515:1.0 525:1.0 528:1.0 532:1.0 533:1.0 534:1.0 535:1.0 538:1.0 541:1.0 544:1.0 545:1.0 547:1.0 551:1.0 553:1.0 554:1.0 557:1.0 559:1.0 561:1.0 562:1.0 563:1.0 566:1.0 569:1.0 570:1.0 571:1.0 573:1.0 574:1.0 575:1.0 576:1.0 578:1.0 580:1.0 581:1.0 583:1.0 585:1.0 587:1.0 588:1.0 590:1.0 593:1.0 594:1.0 596:1.0 600:1.0 601:1.0 605:1.0 608:1.0 609:1.0 610:1.0 614:1.0 615:1.0 618:1.0 622:1.0 625:1.0 627:1.0 635:1.0 637:1.0 641:1.0 643:1.0 644:1.0 645:1.0 646:1.0 648:1.0 651:1.0 654:1.0 658:1.0 660:1.0 661:1.0 663:1.0 665:1.0 669:1.0 671:1.0 673:1.0 674:1.0 675:1.0 676:1.0 678:1.0 682:1.0 684:1.0 688:1.0 691:1.0 694:1.0 695:1.0 696:1.0 697:1.0 698:1.0 701:1.0 702:1.0 703:1.0 705:1.0 707:1.0 708:1.0 709:1.0 711:1.0 713:1.0 715:1.0 716:1.0 717:1.0 718:1.0 719:1.0 720:1.0 722:1.0 723:1.0 726:1.0 728:1.0 731:1.0 734:1.0 744:1.0 745:1.0 746:1.0 748:1.0 752:1.0 753:1.0 754:1.0 755:1.0 758:1.0 759:1.0 761:1.0 762:1.0 763:1.0 764:1.0 765:1.0 768:1.0 769:1.0 771:1.0 772:1.0 774:1.0 775:1.0 777:1.0 779:1.0 780:1.0 782:1.0 783:1.0 785:1.0 786:1.0 788:1.0 790:1.0 791:1.0 795:1.0 797:1.0 798:1.0 800:1.0 801:1.0 802:1.0 806:1.0 807:1.0 809:1.0 812:1.0 823:1.0 825:1.0 827:1.0 829:1.0 833:1.0 834:1.0 837:1.0 842:1.0 843:1.0 852:1.0 854:1.0 857:1.0 859:1.0 863:1.0 865:1.0 866:1.0 867:1.0 869:1.0 879:1.0 880:1.0 881:1.0 887:1.0 889:1.0 890:1.0 894:1.0 909:1.0 911:1.0 913:1.0 914:1.0 915:1.0 919:1.0 920:1.0 925:1.0 926:1.0 928:1.0 929:1.0 930:1.0 933:1.0 936:1.0 937:1.0 939:1.0 941:1.0 943:1.0 950:1.0 952:1.0 954:1.0 955:1.0 964:1.0 969:1.0 970:1.0 972:1.0 978:1.0 980:1.0 992:1.0 993:1.0 995:1.0 997:1.0 998:1.0 999:1.0 1002:1.0 1003:1.0 1006:1.0 1007:1.0 1008:1.0 1009:1.0 1011:1.0 1013:1.0 1015:1.0 1016:1.0 1020:1.0 1023:1.0 1025:1.0 1028:1.0 1030:1.0 1032:1.0 1034:1.0 1036:1.0 1038:1.0 1039:1.0 1040:1.0 1041:1.0 1042:1.0 1043:1.0 1044:1.0 1045:1.0 1055:1.0 1056:1.0 1060:-1.0 1065:1.0 1066:1.0 1070:1.0 1072:-0.8390804597701149 1073:1.0 1085:1.0 1088:1.0 1090:1.0 1092:1.0 1096:1.0 1100:1.0 1101:1.0 1110:1.0 1128:1.0 1129:1.0 1131:1.0 1132:1.0 1133:1.0 1135:1.0 1143:-0.927710843373494 1144:1.0 1146:1.0 1149:1.0 1151:1.0 1153:1.0 1154:1.0 1159:1.0 1160:1.0 1161:1.0 1163:1.0 1167:1.0 1170:1.0 1171:1.0 1173:1.0 1177:1.0 1178:1.0 1182:1.0 1184:1.0 1186:1.0 1187:1.0 1190:1.0 1191:1.0 1200:1.0 1213:1.0 1214:1.0 1215:1.0 1217:1.0 1218:1.0 1220:1.0 1227:1.0 1230:1.0 1231:1.0 1232:1.0 1239:1.0 1242:1.0 1246:1.0 1250:1.0 1251:1.0 1258:-0.8461538461538461 1259:1.0 1260:1.0 1261:1.0 1262:1.0 1263:1.0 1264:1.0 1265:1.0 1269:1.0 1272:1.0 1274:1.0 1275:1.0 1277:1.0 1279:1.0 1281:1.0 1283:1.0 1285:1.0 1287:1.0 1289:1.0 1292:1.0 1293:1.0 1295:1.0 1297:1.0 1300:1.0 1301:1.0 1303:1.0 1305:1.0 1308:1.0 1311:1.0 1312:1.0 1313:1.0 1318:1.0 1320:1.0 1322:1.0 1328:1.0 1329:1.0 1337:1.0 1340:1.0 1341:1.0 1358:1.0 1360:1.0 1361:1.0 1368:1.0 1369:1.0 1379:1.0 1380:1.0 1395:1.0 1396:1.0 1399:1.0 1404:1.0 1409:1.0 1410:1.0 1413:1.0 1419:1.0 1421:1.0 1422:1.0 1425:1.0 1446:1.0 1449:1.0 1451:1.0 1454:1.0 1456:1.0 1457:1.0 1461:1.0 1462:1.0 1463:1.0 1465:1.0 1468:1.0 1469:1.0 1474:1.0 1480:1.0 1481:1.0 1483:1.0 1487:1.0 1488:1.0 1491:1.0 1492:1.0 1493:1.0 1495:1.0 1496:1.0 1498:1.0 1499:1.0 1500:1.0 1501:1.0 1503:1.0 1505:1.0 1507:1.0 1511:1.0 1516:1.0 1521:1.0 1529:1.0 1534:1.0 1535:1.0 1539:1.0 1540:1.0 1541:1.0 1556:1.0 1557:1.0 1560:1.0 1561:1.0 1562:1.0 1563:1.0 1564:1.0 1566:1.0 1567:1.0 1576:1.0 1591:1.0 1598:1.0 1602:1.0 1603:1.0 1606:1.0 1611:1.0 1615:1.0 1622:1.0 1628:1.0 1630:1.0 1632:1.0 1638:1.0 1649:1.0 1650:1.0 1652:1.0 1658:1.0 1661:1.0 1662:1.0 1663:1.0 1664:1.0 1680:1.0 1682:1.0 1684:1.0 1686:1.0 1687:1.0 1689:1.0 1692:1.0 1695:1.0 1696:1.0 1699:1.0 1700:1.0 1709:1.0 1712:1.0 1713:1.0 1715:1.0 1720:1.0 1721:1.0 1722:1.0 1724:1.0 1725:1.0 1726:1.0 1736:1.0 1738:1.0 1739:1.0 1740:1.0 1744:1.0 1745:1.0 1751:1.0 1756:1.0 1757:1.0 1759:1.0 1761:1.0 1762:1.0 1763:1.0 1767:1.0 1768:1.0 1770:1.0 1772:1.0 1775:1.0 1778:1.0 1784:1.0 1788:1.0 1790:1.0 1793:1.0 1797:1.0 1798:1.0 1802:1.0 1805:1.0 1806:1.0 1807:1.0 1809:1.0 1814:1.0 1816:1.0 1817:1.0 1818:1.0 1819:1.0 1822:1.0 1824:1.0 1829:1.0 1830:1.0 1831:1.0 1834:1.0 1835:1.0 1837:1.0 1839:1.0 1844:1.0 1851:1.0 1854:1.0 1855:1.0 1857:1.0 1858:1.0 1859:1.0 1861:1.0 1862:1.0 1864:1.0 1866:1.0 1867:1.0 1869:1.0 1870:1.0 1871:1.0 1872:1.0 1876:1.0 1877:1.0 1878:1.0 1881:1.0 1883:1.0 1884:1.0 1885:1.0 1887:1.0 1888:1.0 1889:1.0 1893:1.0 1894:1.0 1896:1.0 1898:1.0 1899:1.0 1901:1.0 1904:1.0 1906:1.0 1907:1.0 1908:1.0 1909:1.0 1910:1.0 1911:1.0 1914:1.0 1915:1.0 1917:1.0 1918:1.0 1919:1.0 1923:1.0 1925:1.0 1927:1.0 1931:1.0 1932:1.0 1939:1.0 1941:1.0 1945:1.0 1946:1.0 1950:1.0 1952:1.0 1953:1.0 1954:1.0 1956:1.0 1958:1.0 1959:1.0 1964:1.0 1969:1.0 1970:1.0 1971:1.0 1973:1.0 1974:1.0 1976:1.0 1978:1.0 1985:1.0 1987:1.0 1991:1.0 1994:1.0 1995:1.0 1996:1.0 1997:1.0 1999:1.0 2007:1.0 2012:1.0 2013:1.0 2016:1.0 2017:1.0 2018:1.0 2019:1.0 2020:1.0 2021:1.0 2022:1.0 2023:1.0 2025:1.0 2026:1.0 2027:1.0 2028:1.0 2030:1.0 2033:1.0 2034:1.0 2037:1.0 2039:1.0 2040:1.0 2043:1.0 2045:1.0 2048:1.0 2050:1.0 2053:1.0 2056:1.0 2061:1.0 2064:1.0 2070:1.0 2071:1.0 2072:1.0 2078:1.0 2081:1.0 2082:1.0 2086:1.0 2087:1.0 2089:1.0 2090:1.0 2093:1.0 2094:1.0 2099:1.0 2100:1.0 2101:1.0 2104:1.0 2106:1.0 2108:1.0 2111:1.0 2112:1.0 2114:1.0 2115:1.0 2116:1.0 2119:1.0 2120:1.0 2124:1.0 2125:1.0 2126:1.0 2127:1.0 2131:1.0 2136:1.0 2137:1.0 2138:1.0 2142:1.0 2145:1.0 2147:1.0 2152:1.0 2153:1.0 2155:1.0 2157:1.0 2159:1.0 2161:1.0 2162:1.0 2168:1.0 2169:1.0 2172:1.0 2174:1.0 2175:1.0 2176:1.0 2179:1.0 2181:1.0 2184:1.0 2187:1.0 2188:1.0 2189:1.0 2193:1.0 2194:1.0 2200:1.0 2201:1.0 2206:1.0 2208:1.0 2210:1.0 2213:1.0 2216:1.0 2219:1.0 2225:1.0 2228:1.0 2229:1.0 2231:1.0 2233:1.0 2234:1.0 2235:1.0 2238:1.0 2239:1.0 2241:1.0 2242:1.0 2244:1.0 2245:1.0 2246:1.0 2249:1.0 2252:1.0 2253:1.0 2255:1.0 2256:1.0 2257:1.0 2259:1.0 2260:1.0 2263:1.0 2265:1.0 2267:1.0 2270:1.0 2272:1.0 2273:1.0 2274:1.0 2277:1.0 2278:1.0 2281:1.0 2282:1.0 2285:1.0 2288:1.0 2292:1.0 2293:1.0 2294:1.0 2296:1.0 2307:1.0 2316:1.0 2319:1.0 2322:1.0 2325:1.0 2329:1.0 2330:1.0 2331:1.0 2334:1.0 2336:1.0 2337:1.0 2340:1.0 2345:1.0 2346:1.0 2384:1.0 2385:1.0 2388:1.0 2396:1.0 2397:1.0 2400:1.0 2401:1.0 2402:1.0 2403:1.0 2410:1.0 2411:1.0 2412:1.0 2414:1.0 2415:1.0 2419:1.0 2420:1.0 2421:1.0 2426:1.0 2427:1.0 2431:1.0 2432:1.0 2434:1.0 2435:1.0 2438:1.0 2440:1.0 2442:1.0 2443:1.0 2445:1.0 2447:1.0 2449:1.0 2450:1.0 2452:1.0 2454:1.0 2461:1.0 2465:1.0";
        form.AddField("rssi", result);
        WWW www = new WWW(url, form);
        while (!www.isDone)
        { // ダウンロードの進捗を表示
            progressBar.fillAmount += Random.Range(0f, 0.05f);
            yield return new WaitForSeconds(0.05f);
        }
        progressBar.fillAmount = 0;
        progressBar.gameObject.SetActive(false);
        //yield return www;
        if (www.error == null)
        {
            Debug.Log(www.text);
            result = www.text;
            //部屋番号変換の前にデータベースにアクセスして陣地取り処理を行う
            //部屋番号が返却されていれば文字数は少ないので
            if (result.Length <= 10)
            {
                //データベースから部屋番号で検索
                //QueryTestを検索するクラスを作成
                NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Room");
                //Scoreの値が7と一致するオブジェクト検索
                query.WhereEqualTo("Number", WifiRSSI.isRoomNumber(result));
                query.FindAsync((List<NCMBObject> objList, NCMBException e) => {
                    if (e != null)
                    {
                        //検索失敗時の処理
                        Debug.Log("失敗した！");
                    }
                    else
                    {
                        //Numberがresultのオブジェクトを出力（当然一個しか出ない＜被りがないからね＞）
                        foreach (NCMBObject obj in objList)
                        {
                            //データを取り出す
                            string masterName = "";
                            //誰も入っていないとき
                            if (obj["Master"] != null)
                            {
                                masterName = obj["Master"].ToString();
                            }
                            int restHp = System.Convert.ToInt32(obj["RestHP"]);
                            int defense = System.Convert.ToInt32(obj["DEF"]);
                            Debug.Log("objectId:" + obj["Number"]);

                            //ダメージ計算を行う
                            //DEF/2を自分のATKから引いたダメージだけ与えることにする（とりあえず）
                            int ATK = param.Attack - defense / 2;
                            restHp -= ATK;
                            //現マスターの残りHPが0以下になったら、もしくはそもそもマスター居ないならマスター交代
                            if (restHp <= 0 || string.IsNullOrEmpty(masterName))
                            {
                                obj["Master"] = UserData.userName;
                                obj["MasterID"] = UserData.userID;
                                obj["RestHP"] = param.Hp;
                                obj["DEF"] = param.Defense;
                                statusText = "新しくあなたがマスターになりました！";
                            }
                            else if (obj["MasterID"].ToString() != UserData.userID)
                            {
                                obj["RestHP"] = restHp;
                                statusText = "マスターに " + ATK + " のダメージを与えました！";
                            }
                            else
                            {
                                statusText = "すでにあなたがマスターです！";
                            }
                            obj.Save();
                        }
                    }
                });
            }

            result = WifiRSSI.isRoom(result);
        }
        else
        {
            Debug.LogError(www.error);
            Debug.Log("うんち");
        }
        accessButton.enabled = true;
        Debug.Log(result);

        accessStation.text = result;
        accessPanel.SetActive(true);
    }

    //アクセスパネルを消したとき
    public void DeleteAccessPanel()
    {
        //アクセスパネルのテキストを写してInstantiate
        Text[] texts = originPanel.GetComponentsInChildren<Text>();
        foreach(Text text in texts)
        {
            if (text.gameObject.name == "StatusText")
            {
                text.text = statusText;
            }
            if (text.gameObject.name == "StationText")
            {
                text.text = accessStation.text;
            }
        }
        RectTransform panel = GameObject.Instantiate<RectTransform>(originPanel);
        panel.transform.parent = contents.transform;
        panel.SetAsFirstSibling();
        panel.gameObject.SetActive(true);
        renewDB();
    }

    //現在のマスターの一覧を更新
    public void renewDB()
    {
        //contentsの子供を消す処理（元だけは消さない）
        for (int i = 0; i < DBcontents.transform.childCount; i++)
        {
            GameObject child = DBcontents.transform.GetChild(i).gameObject;
            if (child.name != originDBTexts.name)
            {
                Destroy(child.gameObject);
            }
        }
        for (int i = 0; i < myDBcontents.transform.childCount; i++)
        {
            GameObject child = myDBcontents.transform.GetChild(i).gameObject;
            if (child.name == originMyDBTexts.name)
            {
                Destroy(child.gameObject);
            }
        }
        userCount = 0;
        //DBにアクセスして上から下までデータいただく
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Room");
        query.OrderByAscending("Count");
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e == null)
            {
                //データ取得成功時
                foreach (NCMBObject obj in objList)
                {
                    //マスターが自分だった場合
                    //if (obj["MasterID"].ToString() == UserData.userID)
                    //{
                    //    userCount++;
                    //    foreach (Text text in originMyDBTexts.GetComponentsInChildren<Text>())
                    //    {
                    //        if (text.gameObject.name == "RoomNameText")
                    //        {
                    //            text.text = WifiRSSI.isRoom(obj["Number"].ToString());
                    //        }
                    //        if (text.gameObject.name == "MasterText")
                    //        {
                    //            if (string.IsNullOrEmpty(obj["Master"].ToString()))
                    //            {
                    //                text.text = "現在空いています！！！";
                    //            }
                    //            else
                    //            {
                    //                text.text = obj["Master"].ToString();
                    //            }
                    //        }
                    //        if (text.gameObject.name == "HPText")
                    //        {
                    //            text.text = obj["RestHP"].ToString();
                    //        }
                    //    }
                    //    RectTransform myPanel = GameObject.Instantiate<RectTransform>(originMyDBTexts);
                    //    myPanel.transform.parent = myDBcontents.transform;
                    //    myPanel.gameObject.SetActive(true);
                    //}

                    //ひとつずつ出力！
                    foreach (Text text in originDBTexts.GetComponentsInChildren<Text>())
                    {
                        if (text.gameObject.name == "RoomNameText")
                        {
                            text.text = WifiRSSI.isRoom(obj["Number"].ToString());                        
                        }
                        if (text.gameObject.name == "MasterText")
                        {
                            if (string.IsNullOrEmpty(obj["Master"].ToString()))
                            {
                                text.text = "現在空いています！！！";
                            }
                            else
                            {
                                text.text = obj["Master"].ToString();
                            }
                        }
                        if (text.gameObject.name == "HPText")
                        {
                            text.text = obj["RestHP"].ToString();
                        }                      
                    }
                    RectTransform panel = GameObject.Instantiate<RectTransform>(originDBTexts);
                    panel.transform.parent = DBcontents.transform;
                    panel.gameObject.SetActive(true);
                }
                myAccessButton.text = "MyAccess\n" + userCount;
            }
            else
            {
                Debug.LogError("データベースエラー！ " + e.ErrorCode);
            }
        });
    }

}
