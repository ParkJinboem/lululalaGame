using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;

[System.Serializable]
public class LoginData
{
    public int member_no;
    public string company_id;
}

[System.Serializable]
public class MemeberData
{
    public List<MemberDataDetail> result;
    public string code;
    public string message = "Sucess";
}
[System.Serializable]
public class MemberDataDetail
{

}

public class HttpWeb : MonoBehaviour
{
    public string baseURL = "Http://192.168.219.147:8077/Load_AppCheck";


    private void Start()
    {
        GetMemeberData();
    }
    private MemeberData GetMemeberData()
    {
        //�������� ���� ������
        LoginData data = new LoginData();
        data.member_no = 50;
        data.company_id = "COM0000003";

        //JsonUtility ��� string, byte[]�� ��ȯ
        string str = JsonUtility.ToJson(data);
        var bytes = System.Text.Encoding.UTF8.GetBytes(str);

        //��û ���� �ּҿ� ����
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
        MemeberData info = JsonUtility.FromJson<MemeberData>(json);
        Debug.Log(info.message);
        return info;
    }

}
