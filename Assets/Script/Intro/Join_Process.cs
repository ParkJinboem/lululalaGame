using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Join_Process : MonoBehaviour
{
    //배경 어둡게
    public Image mainImage;
    public Image startButton;
    public Image changeId;

    //스크롤뷰 설정
    public ScrollRect terOfSer;

    //인풋필드 설정
    public InputField loginUserID;
    public InputField loginUserPW;
    public InputField signInUserID;
    public InputField signInUserPW;
    public InputField signInUserPWChk;
    public InputField signInUserNick;
    public InputField searchIDinputfield;
    public InputField searchPWinputfield;

    //팝업창
    public GameObject loginInPanel;
    public GameObject signInPanel;
    public GameObject search_IDPassPanel;
    public GameObject search_IDPanel;
    public GameObject search_PassPanel;
    public GameObject gameStartPanel;
    public GameObject terOfSerPanel;
    public GameObject[] searchID;
    public GameObject[] searchpass;

    //팝업창 엑스박스
    public GameObject signIn_Xbox;
    public GameObject loginIn_Xbox;
    public GameObject SearchIDPass_Xbox;
    public GameObject terOfSer_Xbox;

    //에러메시지 팝업창
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

    public void SiginIN_On()    //이용약관창 동의합니다 버튼
    {
        signInPanel.SetActive(true);
        loginInPanel.SetActive(false);
        terOfSerPanel.SetActive(false);
        mainImage.color = new Color(0.6f, 0.6f, 0.6f, 1.0f);
        startButton.color = new Color(0.6f, 0.6f, 0.6f, 1.0f);
        changeId.color = new Color(0.6f, 0.6f, 0.6f, 1.0f);
    }
    public void TerOfSer_On() //로그인판넬 회원가입 버튼
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
    }   //계정전환 버튼

    public void SearchIDPass()  //아이디 패스워드찾기창
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
    public void SearchID_On() //아이디찾기 켜짐버튼창
    {
        search_IDPanel.SetActive(true);
        search_PassPanel.SetActive(false);

        searchID[0].SetActive(true);
        searchID[1].SetActive(false);
        searchpass[0].SetActive(false);
        searchpass[1].SetActive(true);
    }
    public void SearchID_Off() //아이디찾기 꺼짐버튼찾기창
    {
        search_IDPanel.SetActive(true);
        search_PassPanel.SetActive(false);

        searchID[0].SetActive(true);
        searchID[1].SetActive(false);
        searchpass[0].SetActive(false);
        searchpass[1].SetActive(true);
        searchPWinputfield.text = null;
    }
    public void SearchPass_On() //비번찾기 켜짐버튼찾기창
    {
        search_PassPanel.SetActive(true);
        search_IDPanel.SetActive(false);

        searchID[0].SetActive(false);
        searchID[1].SetActive(true);
        searchpass[0].SetActive(true);
        searchpass[1].SetActive(false);
    }
    public void SearchPass_Off()   //비번찾기창 꺼짐버튼창
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
    }   //로그인 닫기 버튼
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
    }   //회원가입 닫기 버튼
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
    }   //아이디/비번찾기 닫기버튼
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
    }   //이용약관 닫기 버튼

    public void ScrollViewUp()  //스크롤뷰 위로올리기 버튼
    {

    }
    public void ScrollViewDown()  //스크롤뷰 내리기 버튼
    {

    }


    public void LoginButton()   //로그인창 로그인 버튼
    {
        Debug.Log("로그인버튼 클릭");
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
    public void LoginErrorClose()   //로그인창 에러닫기
    {
        nullId_login.SetActive(false);
        nonResId_login.SetActive(false);
        nullPas_login.SetActive(false);
        nonRespas_login.SetActive(false);
    }
    public void SigninButton()  //회원가입창 회원가입 버튼
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
            Register(tempid,temppw,tempnic); //회원가입
            SceneManager.LoadScene("03MainScene");
        }
    }
    public void SigninIdCheckButton()   //아이디 중복체크 버튼
    {
        string tempId = signInUserID.text;

        if (tempId == "a")  //등록된 아이디
        {
            idDoubleChk_signin.SetActive(true);
            Invoke("SigninErrorClose", 2.0f);
        }
        else if (tempId == "")   //아이디 빈칸
        {
            nullId_signin.SetActive(true);
            Invoke("SigninErrorClose", 2.0f);
        }
        else    //아이다 사용가능
        {
            idRes_signin.SetActive(true);
            Invoke("SigninErrorClose", 2.0f);
        }
    }
    public void SigninNicCheckButton()  //닉네임 중복체크 버튼
    {
        string tempNic = signInUserNick.text;

        if (tempNic == "a")  //등록된 닉네임
        {
            nicDoubleChk_signin.SetActive(true);
            Invoke("SigninErrorClose", 2.0f);
        }
        else if (tempNic == "")   //닉네임 빈칸
        {
            nullNic_signin.SetActive(true);
            Invoke("SigninErrorClose", 2.0f);
        }
        else    //닉네임 사용가능
        {
            nicRes_signin.SetActive(true);
            Invoke("SigninErrorClose", 2.0f);
        }
    }
    public void SigninErrorClose()  //회원가입창 에러닫기
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
        
    }   //아이디  찾기창 아이디 찾기 버튼
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
    }   //비밀번호 찾기창 비밀번호 찾기 버튼
    public void searchIdPwErrorClose()
    {
        nullId_searchIdPw.SetActive(false);
        nonResId_searchIdPw.SetActive(false);
        nullNic_searchIdPw.SetActive(false);
        nonResNic_searchIdPw.SetActive(false);
    }   //아이디/비밀번호 찾기창 에러닫기



    public void GoGame()
    {
        SceneManager.LoadScene("03MainScene");
    }

    public void Register(string Id, string Pw, string Nic)
    {

    }





}
