using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LoginData
{
    public string user_id;
    public string user_pass;
    public int result;
}
[System.Serializable]
public class Load_AppCheck  //��üũ ����
{
    public int result;
    public string app_version;
    public string app_down;
}
[System.Serializable]
public class Load_ServiceCheck  //����üũ ����
{
    public int result;
    public int service_state;
    public string service_message;
}
