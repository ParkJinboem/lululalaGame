using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.IO;
using UnityEngine.SceneManagement;



public class AppCheck : MonoBehaviour
{
    //서버 주소 및 데이터객체 생성
    public static string appCheckURL = "Http://192.168.219.147:8077/Load_AppCheck";
    public static string serviceCheckURL = "Http://192.168.219.147:8077/Load_ServiceCheck";

    static Load_AppCheck load_AppData = new Load_AppCheck();
    static Load_ServiceCheck load_ServiceData = new Load_ServiceCheck();

    //로딩 이미지
    [SerializeField]
    Image loadingImageBar;
    public Image[] loadingText_10;
    public Image[] loadingText_1;
    public Image loadingTextPercent;

    //public Text versionText;
    //public Text serviceText;

    // Start is called before the first frame update
    void Start()
    {
        //GetServerData();
        //AppCheckSucces();
        //ServiceCheck();
        for (int i = 0; i < 10; i++)
        {
            loadingText_10[i].enabled = false;
            loadingText_1[i].enabled = false;
        }
        StartCoroutine(LoadSceneProcess());
    }
    IEnumerator LoadSceneProcess()  //비동기식 로딩
    {
        yield return null;
        AsyncOperation opration = SceneManager.LoadSceneAsync("02IntroScene");
        //opration.isDone  ==> 작업완료의 유무를 boolean형으로 반환
        //opration.progress ==> 진행정도를 float형 0 ,1 을 반홤(0-진행중 1-진행완료)
        //opration.allowSceneActivation ==> 로딩이 완료되면 신을 넘기고 false면 progress가 0.9f서 멈춤, true면 불러온 씬으로넘김
        opration.allowSceneActivation = false;

        float timer = 0f;
        while (!opration.isDone)
        {
            yield return null;

            if (loadingImageBar.fillAmount < 0.9f)
            {
                loadingImageBar.fillAmount = opration.progress;

                for (int i = 0; i < 10; i++)
                {
                    loadingText_1[i].enabled = true;
                    loadingText_10[0].enabled = true;
                }
            }
            else if (opration.progress >= 0.9f)
            {
                timer += Time.unscaledDeltaTime;
                loadingImageBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);

                if (loadingImageBar.fillAmount >= 1.0f && opration.progress >= 0.9f)
                {
                    opration.allowSceneActivation = true;
                    yield break;
                }
            }

        }
    }

    //public void AppCheckSucces()
    public static Load_AppCheck AppCheckSucces()
    {
        //JsonUtility사용 string, bytep[]로 변환
        string str = JsonUtility.ToJson(load_AppData);
        var bytes = System.Text.Encoding.UTF8.GetBytes(str);

        //요청보낼 주소와 세팅
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(appCheckURL);
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
        Load_AppCheck appCheckObj = JsonUtility.FromJson<Load_AppCheck>(json);

        Debug.Log("result : " + appCheckObj.result);
        Debug.Log("app_down : " + appCheckObj.app_down);
        Debug.Log("app_version : " + appCheckObj.app_version);
        //versionText.text = load_AppData.app_version.ToString();
        return load_AppData;
    }
    //public void ServiceCheck()
    public static Load_ServiceCheck ServiceCheck()
    {
        //JsonUtility사용 string, bytep[]로 변환
        string str = JsonUtility.ToJson(load_ServiceData);
        var bytes = System.Text.Encoding.UTF8.GetBytes(str);

        //요청보낼 주소와 세팅
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceCheckURL);
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
        Load_ServiceCheck serviceCheckObj = JsonUtility.FromJson<Load_ServiceCheck>(json);


        Debug.Log("result : " + serviceCheckObj.result);
        Debug.Log("service_state : " + serviceCheckObj.service_state);
        Debug.Log("service_message : " + serviceCheckObj.service_message);

        //serviceText.text = load_ServiceData.service_state.ToString();
        return load_ServiceData;
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
};
