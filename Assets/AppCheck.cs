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
    public string nomal = "�����Դϴ�.";
    public string check = "�������Դϴ�.";
    public string error = "�����Դϴ�.";
}

public class AppCheck : MonoBehaviour
{
    //���� �ּ� �� �����Ͱ�ü ����
    public string baseURL = "Http://192.168.219.147:8077/Load_AppCheck";
    ServerData data = new ServerData();
    

    //�ε� �̹���
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

    //    //JsonUtility��� string, bytep[]�� ��ȯ
    //    string str = JsonUtility.ToJson(data);
    //    var bytes = System.Text.Encoding.UTF8.GetBytes(str);

    //    //��û���� �ּҿ� ����
    //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL);
    //    request.Method = "POST";
    //    request.ContentType = "application/json";
    //    request.ContentLength = bytes.Length;

    //    //Stream �������� �����͸� ����
    //    using (var stream = request.GetRequestStream())
    //    {
    //        stream.Write(bytes, 0, bytes.Length);
    //        stream.Flush();
    //        stream.Close();
    //    }

    //    //���䵥���͸� StreamReader�� ����
    //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
    //    StreamReader reader = new StreamReader(response.GetResponseStream());
    //    string json = reader.ReadToEnd();

    //    //string ���� JsonUtility�� Ŀ���� Ŭ����
    //    ServerData info = JsonUtility.FromJson<ServerData>(json);
    //    Debug.Log(info.result);
    //    return info;
    //}




    //public ServerData AppCheckSucces()
    public void AppCheckSucces()
    {
        //JsonUtility��� string, bytep[]�� ��ȯ
        string str = JsonUtility.ToJson(data.app_version);
        var bytes = System.Text.Encoding.UTF8.GetBytes(str);

        //��û���� �ּҿ� ����
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL);
        request.Method = "POST";
        request.ContentType = "application/json";
        request.ContentLength = bytes.Length;

        //Stream �������� �����͸� ����
        using (var stream = request.GetRequestStream())
        {
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
            stream.Close();
        }
        //���䵥���͸� StreamReader�� ����
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();

        //string ���� JsonUtility�� Ŀ���� Ŭ����
        ServerData app_version = JsonUtility.FromJson<ServerData>(json);
        Debug.Log(data.app_version);
        curValue = 50;
        versionText.text = data.app_version.ToString();
        //return app_version;
    }


    //public ServerData ServiceCheck()
    public void ServiceCheck()
    {
        //JsonUtility��� string, bytep[]�� ��ȯ
        string str = JsonUtility.ToJson(data.service_state);
        var bytes = System.Text.Encoding.UTF8.GetBytes(str);

        //��û���� �ּҿ� ����
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL);
        request.Method = "POST";
        request.ContentType = "application/json";
        request.ContentLength = bytes.Length;

        //Stream �������� �����͸� ����
        using (var stream = request.GetRequestStream())
        {
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
            stream.Close();
        }

        //���䵥���͸� StreamReader�� ����
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();

        //string ���� JsonUtility�� Ŀ���� Ŭ����
        ServerData service_state = JsonUtility.FromJson<ServerData>(json);
        Debug.Log(data.service_state);
        curValue = 100;
        serviceText.text = data.service_state.ToString();
        //return service_state;
    }
}
