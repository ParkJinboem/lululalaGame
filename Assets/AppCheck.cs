using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.IO;
using UnityEngine.SceneManagement;

[System.Serializable]
public class UserData
{

}
[System.Serializable]
public class ServerData
{
    public int result = 1;
    public int service_state = 1;
    public string app_version = "1.0.0";
    public string app_down;
    public List<Service_State> service_stateMessage;
}
[System.Serializable]
public class Service_State
{
    public string nomal = "정상입니다.";
    public string check = "점검중입니다.";
    public string error = "에러입니다.";
}

public class AppCheck : MonoBehaviour
{
    //서버 주소 및 데이터객체 생성
    public string baseURL = "Http://192.168.219.147:8077/Load_AppCheck";
    ServerData data = new ServerData();
    

    //로딩 이미지
    public Image loadingImage;
    public Image[] loadingText;
    public RawImage loadingTextPercent;
    string loadSceneName;

    public float maxValue = 100;
    public float curValue;
    public Text versionText;
    public Text serviceText;


    // Start is called before the first frame update
    void Start()
    {
        //GetServerData();

        //AppCheckSucces();
        //ServiceCheck();
       
        for (int i = 0; i < 10; i++)
        {
            loadingText[i].enabled = false;
        }
        loadingTextPercent.enabled = false;
        LoadSceneProcess();
    }
    void Update()
    {
        loadingImage.fillAmount = curValue / maxValue;
        //if (curValue < 90f)
        //{
        //    curValue += Time.deltaTime * 3;
        //}
    }
    
    void LoadSceneProcess()
    {
        
        //float timer = 0.0f;
            
            //else
            //{
            //    timer += Time.unscaledDeltaTime;
            //    loadingImage.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
            //    if(loadingImage.fillAmount>=1.0f)
            //    {
     
            //    }
            //}
    }


    //ServerData GetServerData()
    //{
    //    ServerData data = new ServerData();

    //    //JsonUtility사용 string, bytep[]로 변환
    //    string str = JsonUtility.ToJson(data);
    //    var bytes = System.Text.Encoding.UTF8.GetBytes(str);

    //    //요청보낼 주소와 세팅
    //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL);
    //    request.Method = "POST";
    //    request.ContentType = "application/json";
    //    request.ContentLength = bytes.Length;

    //    //Stream 형식으로 데이터를 보냄
    //    using (var stream = request.GetRequestStream())
    //    {
    //        stream.Write(bytes, 0, bytes.Length);
    //        stream.Flush();
    //        stream.Close();
    //    }

    //    //응답데이터를 StreamReader로 받음
    //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
    //    StreamReader reader = new StreamReader(response.GetResponseStream());
    //    string json = reader.ReadToEnd();

    //    //string 값을 JsonUtility로 커스텀 클래스
    //    ServerData info = JsonUtility.FromJson<ServerData>(json);
    //    Debug.Log(info.result);
    //    return info;
    //}




    //public ServerData AppCheckSucces()
    public void AppCheckSucces()
    {
        //JsonUtility사용 string, bytep[]로 변환
        string str = JsonUtility.ToJson(data.app_version);
        var bytes = System.Text.Encoding.UTF8.GetBytes(str);

        //요청보낼 주소와 세팅
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL);
        request.Method = "POST";
        request.ContentType = "application/json";
        request.ContentLength = bytes.Length;

        //Stream 형식으로 데이터를 보냄
        using (var stream = request.GetRequestStream())
        {
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
            stream.Close();
        }
        //응답데이터를 StreamReader로 받음
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();

        //string 값을 JsonUtility로 커스텀 클래스
        ServerData app_version = JsonUtility.FromJson<ServerData>(json);
        Debug.Log(data.app_version);
        curValue = 50;
        versionText.text = data.app_version.ToString();
        //return app_version;
    }


    //public ServerData ServiceCheck()
    public void ServiceCheck()
    {
        //JsonUtility사용 string, bytep[]로 변환
        string str = JsonUtility.ToJson(data.service_state);
        var bytes = System.Text.Encoding.UTF8.GetBytes(str);

        //요청보낼 주소와 세팅
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL);
        request.Method = "POST";
        request.ContentType = "application/json";
        request.ContentLength = bytes.Length;

        //Stream 형식으로 데이터를 보냄
        using (var stream = request.GetRequestStream())
        {
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
            stream.Close();
        }

        //응답데이터를 StreamReader로 받음
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();

        //string 값을 JsonUtility로 커스텀 클래스
        ServerData service_state = JsonUtility.FromJson<ServerData>(json);
        Debug.Log(data.service_state);
        curValue = 100;
        serviceText.text = data.service_state.ToString();
        //return service_state;
    }
}
