using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainScineUI : MonoBehaviour
{

    public GameObject gameStartButton;
    public Text nickname;
    string tempNickkname;
    public Text heart;
    int tempheart; 
    public Text diamond;
    int tempdiamond;



    // Start is called before the first frame update
    void Start()
    {
        tempheart = 10;
        tempNickkname = "닉네임입니다.";
    }

    // Update is called once per frame
    void Update()
    {
        heart.text = tempheart.ToString();
        nickname.text = tempNickkname.ToString();
    }

    public void GameStart()
    {
        SceneManager.LoadScene("04PlayGame");
    }
}
