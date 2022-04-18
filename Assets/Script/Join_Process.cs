using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Join_Process : MonoBehaviour
{
    bool IsPause;
    string blank = null;
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





    //public InputField user_pass;
    public GameObject signIn_Xbox;
    public GameObject loginIn_Xbox;
    public GameObject SearchID_Xbox;
    public GameObject SearchPass_Xbox;
    public GameObject terOfSer_Xbox;
    private void Awake()
    {
        
    }

    public void SiginIN_On()
    {
        signInPanel.SetActive(true);
        loginInPanel.SetActive(false);
        terOfSerPanel.SetActive(false);
    }
    public void TerOfSer_On()
    {
        terOfSerPanel.SetActive(true);
        loginInPanel.SetActive(false);

    }

    public void LoginIN_On()
    {
        loginInPanel.SetActive(true);
    }

    public void SearchIDPass()  //���̵� �н�����ã��â
    {
        search_IDPassPanel.SetActive(true);
        search_IDPanel.SetActive(true);
        search_PassPanel.SetActive(false);

        searchID[0].SetActive(true);
        searchID[1].SetActive(false);
        searchpass[0].SetActive(false);
        searchpass[1].SetActive(true);
    }
    public void SearchID_On() //���̵�ã�� ������ưâ
    {
        search_IDPanel.SetActive(false);

        searchID[0].SetActive(true);
        searchID[1].SetActive(false);
        searchpass[0].SetActive(false);
        searchpass[1].SetActive(true);

        search_PassPanel.SetActive(false);
    }
    public void SearchID_Off() //���̵�ã�� ������ưã��â
    {
        search_IDPanel.SetActive(true);

        searchID[0].SetActive(true);
        searchID[1].SetActive(false);
        searchpass[0].SetActive(false);
        searchpass[1].SetActive(true);

        search_PassPanel.SetActive(false);
    }
    public void SearchPass_On() //���ã�� ������ưã��â
    {
        search_PassPanel.SetActive(false);

        searchID[0].SetActive(true);
        searchID[1].SetActive(false);
        searchpass[0].SetActive(false);
        searchpass[1].SetActive(true);

        search_PassPanel.SetActive(false);
    }
    public void SearchPass_Off()   //���ã��â ������ưâ
    {
        search_PassPanel.SetActive(true);

        searchID[0].SetActive(false);
        searchID[1].SetActive(true);
        searchpass[0].SetActive(true);
        searchpass[1].SetActive(false);

        search_IDPanel.SetActive(false);
    }

    public void LoginIN_Xbox()
    {
        loginInPanel.SetActive(false);
        //inputID.text = blank.ToString();
    }
    public void SignIN_Xbox()
    {
        signInPanel.SetActive(false);
    }
    public void SearchIDPass_Xbox()
    {
        search_IDPassPanel.SetActive(false);
    }
    public void GoLoading()
    {
        SceneManager.LoadScene("01LoadingScene");
    }
    public void GoGame()
    {
        SceneManager.LoadScene("03MainScene");
    }

 

    public void TerOfSer_Xbox()
    {
        terOfSerPanel.SetActive(false);
    }

}
