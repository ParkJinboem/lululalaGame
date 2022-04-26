using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Join_Process : MonoBehaviour
{
    //��� ��Ӱ�
    public Image mainImage;
    public Image startButton;
    public Image changeId;

    //��ũ�Ѻ� ����
    public ScrollRect terOfSer;

    //��ǲ�ʵ� ����
    public InputField loginUserID;
    public InputField loginUserPW;
    public InputField signInUserID;
    public InputField signInUserPW;
    public InputField signInUserPWChk;
    public InputField signInUserNick;
    public InputField searchIDinputfield;
    public InputField searchPWinputfield;

    //�˾�â
    public GameObject loginInPanel;
    public GameObject signInPanel;
    public GameObject search_IDPassPanel;
    public GameObject search_IDPanel;
    public GameObject search_PassPanel;
    public GameObject gameStartPanel;
    public GameObject terOfSerPanel;
    public GameObject[] searchID;
    public GameObject[] searchpass;

    //�˾�â �����ڽ�
    public GameObject signIn_Xbox;
    public GameObject loginIn_Xbox;
    public GameObject SearchIDPass_Xbox;
    public GameObject terOfSer_Xbox;

    //�����޽��� �˾�â
    public GameObject nonResId_login;
    public GameObject nullId_login;
    public GameObject nonRespas_login;
    public GameObject nullPas_login;

    public GameObject nullId_signin;
    public GameObject nullPas_signin;
    public GameObject nullNic_signin;
    public GameObject pasDiff_signin;

    public GameObject idDoubleChk_signin;
    public GameObject idRes_signin;
    public GameObject nicDoubleChk_signin;
    public GameObject nicRes_signin;

    public GameObject nullId_searchIdPw;
    public GameObject nonResId_searchIdPw;
    public GameObject nullNic_searchIdPw;
    public GameObject nonResNic_searchIdPw;

    public void SiginIN_On()    //�̿���â �����մϴ� ��ư
    {
        signInPanel.SetActive(true);
        loginInPanel.SetActive(false);
        terOfSerPanel.SetActive(false);
        mainImage.color = new Color(0.6f, 0.6f, 0.6f, 1.0f);
        startButton.color = new Color(0.6f, 0.6f, 0.6f, 1.0f);
        changeId.color = new Color(0.6f, 0.6f, 0.6f, 1.0f);
    }
    public void TerOfSer_On() //�α����ǳ� ȸ������ ��ư
    {
        terOfSerPanel.SetActive(true);
        loginInPanel.SetActive(false);
        mainImage.color = new Color(0.6f, 0.6f, 0.6f, 1.0f);
        startButton.color = new Color(0.6f, 0.6f, 0.6f, 1.0f);
        changeId.color = new Color(0.6f, 0.6f, 0.6f, 1.0f);
    }

    public void LoginIN_On()
    {
        loginInPanel.SetActive(true);
        mainImage.color = new Color(0.6f, 0.6f, 0.6f, 1.0f);
        startButton.color = new Color(0.6f, 0.6f, 0.6f, 1.0f);
        changeId.color = new Color(0.6f, 0.6f, 0.6f, 1.0f);
    }   //������ȯ ��ư

    public void SearchIDPass()  //���̵� �н�����ã��â
    {
        loginInPanel.SetActive(false);
        search_IDPassPanel.SetActive(true);
        search_IDPanel.SetActive(true);
        search_PassPanel.SetActive(false);
        searchID[0].SetActive(true);
        searchID[1].SetActive(false);
        searchpass[0].SetActive(false);
        searchpass[1].SetActive(true);

        mainImage.color = new Color(0.6f, 0.6f, 0.6f, 1.0f);
        startButton.color = new Color(0.6f, 0.6f, 0.6f, 1.0f);
        changeId.color = new Color(0.6f, 0.6f, 0.6f, 1.0f);
    }
    public void SearchID_On() //���̵�ã�� ������ưâ
    {
        search_IDPanel.SetActive(true);
        search_PassPanel.SetActive(false);

        searchID[0].SetActive(true);
        searchID[1].SetActive(false);
        searchpass[0].SetActive(false);
        searchpass[1].SetActive(true);
    }
    public void SearchID_Off() //���̵�ã�� ������ưã��â
    {
        search_IDPanel.SetActive(true);
        search_PassPanel.SetActive(false);

        searchID[0].SetActive(true);
        searchID[1].SetActive(false);
        searchpass[0].SetActive(false);
        searchpass[1].SetActive(true);
        searchPWinputfield.text = null;
    }
    public void SearchPass_On() //���ã�� ������ưã��â
    {
        search_PassPanel.SetActive(true);
        search_IDPanel.SetActive(false);

        searchID[0].SetActive(false);
        searchID[1].SetActive(true);
        searchpass[0].SetActive(true);
        searchpass[1].SetActive(false);
    }
    public void SearchPass_Off()   //���ã��â ������ưâ
    {

        search_PassPanel.SetActive(true);
        search_IDPanel.SetActive(false);

        searchID[0].SetActive(false);
        searchID[1].SetActive(true);
        searchpass[0].SetActive(true);
        searchpass[1].SetActive(false);
        searchIDinputfield.text = null;
    }

    public void LoginIN_Xbox()
    {
        loginInPanel.SetActive(false);
        mainImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        startButton.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        changeId.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        loginUserID.text = null;
        loginUserPW.text = null;
        signInUserID.text = null;
        signInUserPW.text = null;
        signInUserPWChk.text = null;
        signInUserNick.text = null;
        searchIDinputfield.text = null;
        searchPWinputfield.text = null;
    }   //�α��� �ݱ� ��ư
    public void SignIN_Xbox()
    {
        signInPanel.SetActive(false);
        mainImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        startButton.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        changeId.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        loginUserID.text = null;
        loginUserPW.text = null;
        signInUserID.text = null;
        signInUserPW.text = null;
        signInUserPWChk.text = null;
        signInUserNick.text = null;
        searchIDinputfield.text = null;
        searchPWinputfield.text = null;
    }   //ȸ������ �ݱ� ��ư
    public void SearchIDPas_Xbox()
    {
        search_IDPassPanel.SetActive(false);
        mainImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        startButton.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        changeId.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        loginUserID.text = null;
        loginUserPW.text = null;
        signInUserID.text = null;
        signInUserPW.text = null;
        signInUserPWChk.text = null;
        signInUserNick.text = null;
        searchIDinputfield.text = null;
        searchPWinputfield.text = null;
    }   //���̵�/���ã�� �ݱ��ư
    public void TerOfSer_Xbox()
    {
        terOfSerPanel.SetActive(false);
        mainImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        startButton.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        changeId.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        loginUserID.text = null;
        loginUserPW.text = null;
        signInUserID.text = null;
        signInUserPW.text = null;
        signInUserPWChk.text = null;
        signInUserNick.text = null;
        searchIDinputfield.text = null;
        searchPWinputfield.text = null;
    }   //�̿��� �ݱ� ��ư

    public void ScrollViewUp()  //��ũ�Ѻ� ���οø��� ��ư
    {

    }
    public void ScrollViewDown()  //��ũ�Ѻ� ������ ��ư
    {

    }


    public void LoginButton()   //�α���â �α��� ��ư
    {
        Debug.Log("�α��ι�ư Ŭ��");
        string tempid = loginUserID.text;
        //string tempid = GetComponent<LoginData>().user_id;
        string temppw = loginUserPW.text;


        if (tempid == "" && temppw == "")
        {
            nullId_login.SetActive(true);
            Invoke("LoginErrorClose", 2.0f);
        }
        else if (tempid == "")
        {
            nullId_login.SetActive(true);
            Invoke("LoginErrorClose", 2.0f);
        }
        else if (temppw == "")
        {
            nullPas_login.SetActive(true);
            Invoke("LoginErrorClose", 2.0f);
        }
        else if (tempid == "a")
        {
            nonResId_login.SetActive(true);
            Invoke("LoginErrorClose", 2.0f);
        }
        else if (temppw == "a")
        {
            nonRespas_login.SetActive(true);
            Invoke("LoginErrorClose", 2.0f);
        }
        else
        {
            SceneManager.LoadScene("03MainScene");
        }

    }
    public void LoginErrorClose()   //�α���â �����ݱ�
    {
        nullId_login.SetActive(false);
        nonResId_login.SetActive(false);
        nullPas_login.SetActive(false);
        nonRespas_login.SetActive(false);
    }
    public void SigninButton()  //ȸ������â ȸ������ ��ư
    {

        string tempid = signInUserID.text;
        string temppw = signInUserPW.text;
        string temppwchk = signInUserPWChk.text;
        string tempnic = signInUserNick.text;

        if(tempid == "")
        {
            nullId_signin.SetActive(true);
            Invoke("SigninErrorClose", 2.0f);
        }
        else if(temppw =="" || temppwchk=="")
        {
            nullPas_signin.SetActive(true);
            Invoke("SigninErrorClose", 2.0f);
        }
        else if(tempnic == "")
        {
            nullNic_signin.SetActive(true);
            Invoke("SigninErrorClose", 2.0f);
        }
        else if(temppw != temppwchk)
        {
            pasDiff_signin.SetActive(true);
            Invoke("SigninErrorClose", 2.0f);
        }
        else
        {
            Register(tempid,temppw,tempnic); //ȸ������
            SceneManager.LoadScene("03MainScene");
        }
    }
    public void SigninIdCheckButton()   //���̵� �ߺ�üũ ��ư
    {
        string tempId = signInUserID.text;

        if (tempId == "a")  //��ϵ� ���̵�
        {
            idDoubleChk_signin.SetActive(true);
            Invoke("SigninErrorClose", 2.0f);
        }
        else if (tempId == "")   //���̵� ��ĭ
        {
            nullId_signin.SetActive(true);
            Invoke("SigninErrorClose", 2.0f);
        }
        else    //���̴� ��밡��
        {
            idRes_signin.SetActive(true);
            Invoke("SigninErrorClose", 2.0f);
        }
    }
    public void SigninNicCheckButton()  //�г��� �ߺ�üũ ��ư
    {
        string tempNic = signInUserNick.text;

        if (tempNic == "a")  //��ϵ� �г���
        {
            nicDoubleChk_signin.SetActive(true);
            Invoke("SigninErrorClose", 2.0f);
        }
        else if (tempNic == "")   //�г��� ��ĭ
        {
            nullNic_signin.SetActive(true);
            Invoke("SigninErrorClose", 2.0f);
        }
        else    //�г��� ��밡��
        {
            nicRes_signin.SetActive(true);
            Invoke("SigninErrorClose", 2.0f);
        }
    }
    public void SigninErrorClose()  //ȸ������â �����ݱ�
    {
        nullId_signin.SetActive(false);
        nullPas_signin.SetActive(false);
        nullNic_signin.SetActive(false);
        pasDiff_signin.SetActive(false);
        idDoubleChk_signin.SetActive(false);
        nicDoubleChk_signin.SetActive(false);
        idRes_signin.SetActive(false);
        nicRes_signin.SetActive(false);
    }
    public void SearchIdButton()
    {
        string tempId = searchIDinputfield.text;
        if (tempId == "")
        {
            nullId_searchIdPw.SetActive(true);
            Invoke("searchIdPwErrorClose", 2.0f);
        }
        else if (tempId == "a")
        {
            nonResId_searchIdPw.SetActive(true);
            Invoke("searchIdPwErrorClose", 2.0f);
        }
        
    }   //���̵�  ã��â ���̵� ã�� ��ư
    public void SearchPwButton()
    {
        string temppw = searchPWinputfield.text;
        if (temppw == "")
        {
            nullNic_searchIdPw.SetActive(true);
            Invoke("searchIdPwErrorClose", 2.0f);
        }
        else if (temppw == "a")
        {
            nonResNic_searchIdPw.SetActive(true);
            Invoke("searchIdPwErrorClose", 2.0f);
        }
    }   //��й�ȣ ã��â ��й�ȣ ã�� ��ư
    public void searchIdPwErrorClose()
    {
        nullId_searchIdPw.SetActive(false);
        nonResId_searchIdPw.SetActive(false);
        nullNic_searchIdPw.SetActive(false);
        nonResNic_searchIdPw.SetActive(false);
    }   //���̵�/��й�ȣ ã��â �����ݱ�



    public void GoGame()
    {
        SceneManager.LoadScene("03MainScene");
    }

    public void Register(string Id, string Pw, string Nic)
    {

    }





}
