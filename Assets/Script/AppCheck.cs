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
    public string appCheckURL = "Http://192.168.219.147:8077/Load_AppCheck";
    public string serviceCheckURL = "Http://192.168.219.147:8077/Load_ServiceCheck";

    Load_AppCheck load_AppData = new Load_AppCheck();
    Load_ServiceCheck load_ServiceData = new Load_ServiceCheck();

    //로딩 이미지
    [SerializeField]
    Image loadingImageBar;

    public Image[] loadingText_10;
    public Image[] loadingText_1;
    public Image loadingTextPercent;

    

    //public Text versionText;
    //public Text serviceText;

    //로딩씬
    static string nextScene;

    public static void LoadScene(string SceneName)
    {
        nextScene = SceneName;
        SceneManager.LoadScene("01LoadingScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GotoIntro());
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
    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op =  SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;    //씬을 90프로까지만 로딩하고 대기(페이크로딩을 구현)

        float timer = 0f;
        while(!op.isDone)   //씬로딩이 끝나지 않으면 계속해서 반복
        {
            Debug.Log("isDone");
            yield return null;

            if(op.progress < 0.9f)  //씬의 진행도가 90프로보다 작으면
            {
                Debug.Log(op.progress);
                loadingImageBar.fillAmount = op.progress;
            }
            else //페이커 로딩 구현
            {
                Debug.Log("fakeloading");
                timer += Time.unscaledDeltaTime;
                loadingImageBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);   //나머지10퍼세트를 1초에 걸쳐서 채움
                if(loadingImageBar.fillAmount>=1.0f)
                {
                    op.allowSceneActivation = true;     //씬을 불러옴
                    yield break;
                }
            }
        }
    }

    //public void AppCheckSucces()
    public Load_AppCheck AppCheckSucces()
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
    public Load_ServiceCheck ServiceCheck()
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

  

    IEnumerator GotoIntro()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(2);
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
