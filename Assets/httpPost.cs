//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using System.Net;
//using System.IO;

//[System.Serializable]
//public class UserData
//{

//}
//[System.Serializable]
//public class ServerData
//{
//    public int result = 1;
//    public int Service_state;
//    public string app_version = "1.0.0";
//    public string app_down;
//    public List<Service_State> service_state;
//}
//[System.Serializable]
//public class Service_State
//{
//    public string nomal = "정상입니다.";
//    public string check = "점검중입니다.";
//    public string error = "에러입니다.";
//}

//public class httpPost : MonoBehaviour
//{
//    //서버 주소 및 데이터객체 생성
//    public string baseURL = "Http://192.168.219.147:8077/Load_AppCheck";
//    ServerData data = new ServerData();

//    //로딩 이미지
//    public Image loadingImage;
//    public int maxValue = 100;
//    public int curValue;


//    // Start is called before the first frame update
//    void Start()
//    {
//        //GetServerData();
//        //AppCheckSucces();
//    }
//    void Update()
//    {
//        loadingImage.fillAmount = curValue / maxValue;
//    }

//    //ServerData GetServerData()
//    //{
//    //    ServerData data = new ServerData();

//    //    //JsonUtility사용 string, bytep[]로 변환
//    //    string str = JsonUtility.ToJson(data);
//    //    var bytes = System.Text.Encoding.UTF8.GetBytes(str);

//    //    //요청보낼 주소와 세팅
//    //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL);
//    //    request.Method = "POST";
//    //    request.ContentType = "application/json";
//    //    request.ContentLength = bytes.Length;

//    //    //Stream 형식으로 데이터를 보냄
//    //    using (var stream = request.GetRequestStream())
//    //    {
//    //        stream.Write(bytes, 0, bytes.Length);
//    //        stream.Flush();
//    //        stream.Close();
//    //    }

//    //    //응답데이터를 StreamReader로 받음
//    //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
//    //    StreamReader reader = new StreamReader(response.GetResponseStream());
//    //    string json = reader.ReadToEnd();

//    //    //string 값을 JsonUtility로 커스텀 클래스
//    //    ServerData info = JsonUtility.FromJson<ServerData>(json);
//    //    Debug.Log(info.result);
//    //    return info;
//    //}
//    public ServerData AppCheckSucces()
//    {
//        //JsonUtility사용 string, bytep[]로 변환
//        string str = JsonUtility.ToJson(data.app_version);
//        var bytes = System.Text.Encoding.UTF8.GetBytes(str);

//        //요청보낼 주소와 세팅
//        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL);
//        request.Method = "POST";
//        request.ContentType = "application/json";
//        request.ContentLength = bytes.Length;

//        //Stream 형식으로 데이터를 보냄
//        using (var stream = request.GetRequestStream())
//        {
//            stream.Write(bytes, 0, bytes.Length);
//            stream.Flush();
//            stream.Close();
//        }

//        //응답데이터를 StreamReader로 받음
//        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
//        StreamReader reader = new StreamReader(response.GetResponseStream());
//        string json = reader.ReadToEnd();

//        //string 값을 JsonUtility로 커스텀 클래스
//        ServerData app_version = JsonUtility.FromJson<ServerData>(json);
//        Debug.Log(app_version);
//        curValue = 50;
//        return app_version;

        
//    }
//}
