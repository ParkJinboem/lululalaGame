//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;



//public class VersionCheck : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
//		Updatecheck();
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }

//    void Updatecheck()
//    {
//		// ����Ƽ �÷��̾� ���ÿ� ������ ���� ����
//		System.Version client = new System.Version(Application.version);
//		Debug.Log("clientVersion: " + client);

//#if UNITY_EDITOR
//		//���� ���� ��ȸ�� iOS / Android ȯ�濡���� ȣ�� �� �� �ֽ��ϴ�.
//		Debug.Log("������ ��忡���� ���� ������ ��ȸ�� �� �����ϴ�.");
//		return;
//#endif

//		// �ڳ� �ֿܼ��� ������ ���� ������ ��ȸ -> �������� ������ ���� ������ ��ȸ???????????????
//		Backend.Utils.GetLatestVersion(callback => {
//			if (callback.IsSuccess() == false)
//			{
//				Debug.LogError("���� ������ ��ȸ�ϴµ� �����Ͽ����ϴ�.\n" + callback);
//				return;
//			}

//			var version = callback.GetReturnValuetoJSON()["version"].ToString();
//			Version server = new Version(version);

//			var result = server.CompareTo(client);
//			if (result == 0)
//			{
//				// 0 �̸� �� ������ ��ġ�ϴ� �� �Դϴ�.
//				// �ƹ� �۾� ���ϰ� ����
//				return;
//			}
//			else if (result < 0)
//			{
//				// 0 �̸��� ��� server ������ client ���� ���� ��� �Դϴ�.
//				// ����/���� ���� �˼��� �־��� ��� ���⿡ �ش� �� �� �ֽ��ϴ�.
//				// ex)
//				// �˼��� ��û�� Ŭ���̾�Ʈ ������ 3.0.0, 
//				// ���̺꿡 ������� Ŭ���̾�Ʈ ������ 2.0.0,
//				// �ڳ� �ֿܼ� ����� ������ 2.0.0 

//				// �ƹ� �۾��� ���ϰ� ����
//				return;
//			}
//			// 0���� ũ�� server ������ Ŭ���̾�Ʈ ���� ������ �� �ֽ��ϴ�.
//			else if (client == null)
//			{
//				// �� Ŭ���̾�Ʈ ���� ������ null�� ��쿡�� 0���� ū ���� ���ϵ� �� �ֽ��ϴ�.
//				// �� ���� �ƹ� �۾��� ���ϰ� �����ϵ��� �ϰڽ��ϴ�.
//				Debug.LogError("Ŭ���̾�Ʈ ���� ������ null �Դϴ�.");
//				return;
//			}

//			// ������� ���� ���� ������ server ����(�ڳ� �ֿܼ� ����� ����)�� 
//			// Ŭ���̾�Ʈ���� ���� ��� �Դϴ�.

//			// ������ ������ ������Ʈ�� �ϵ��� ������Ʈ UI�� ����ݴϴ�.
//			OpenUpdateUI();
//		});
//	}
//}
