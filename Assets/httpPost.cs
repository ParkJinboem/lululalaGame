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
//    public string nomal = "�����Դϴ�.";
//    public string check = "�������Դϴ�.";
//    public string error = "�����Դϴ�.";
//}

//public class httpPost : MonoBehaviour
//{
//    //���� �ּ� �� �����Ͱ�ü ����
//    public string baseURL = "Http://192.168.219.147:8077/Load_AppCheck";
//    ServerData data = new ServerData();

//    //�ε� �̹���
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

//    //    //JsonUtility��� string, bytep[]�� ��ȯ
//    //    string str = JsonUtility.ToJson(data);
//    //    var bytes = System.Text.Encoding.UTF8.GetBytes(str);

//    //    //��û���� �ּҿ� ����
//    //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL);
//    //    request.Method = "POST";
//    //    request.ContentType = "application/json";
//    //    request.ContentLength = bytes.Length;

//    //    //Stream �������� �����͸� ����
//    //    using (var stream = request.GetRequestStream())
//    //    {
//    //        stream.Write(bytes, 0, bytes.Length);
//    //        stream.Flush();
//    //        stream.Close();
//    //    }

//    //    //���䵥���͸� StreamReader�� ����
//    //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
//    //    StreamReader reader = new StreamReader(response.GetResponseStream());
//    //    string json = reader.ReadToEnd();

//    //    //string ���� JsonUtility�� Ŀ���� Ŭ����
//    //    ServerData info = JsonUtility.FromJson<ServerData>(json);
//    //    Debug.Log(info.result);
//    //    return info;
//    //}
//    public ServerData AppCheckSucces()
//    {
//        //JsonUtility��� string, bytep[]�� ��ȯ
//        string str = JsonUtility.ToJson(data.app_version);
//        var bytes = System.Text.Encoding.UTF8.GetBytes(str);

//        //��û���� �ּҿ� ����
//        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL);
//        request.Method = "POST";
//        request.ContentType = "application/json";
//        request.ContentLength = bytes.Length;

//        //Stream �������� �����͸� ����
//        using (var stream = request.GetRequestStream())
//        {
//            stream.Write(bytes, 0, bytes.Length);
//            stream.Flush();
//            stream.Close();
//        }

//        //���䵥���͸� StreamReader�� ����
//        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
//        StreamReader reader = new StreamReader(response.GetResponseStream());
//        string json = reader.ReadToEnd();

//        //string ���� JsonUtility�� Ŀ���� Ŭ����
//        ServerData app_version = JsonUtility.FromJson<ServerData>(json);
//        Debug.Log(app_version);
//        curValue = 50;
//        return app_version;

        
//    }
//}
