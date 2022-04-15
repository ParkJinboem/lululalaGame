using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainScineUI : MonoBehaviour
{
    public GameObject settingPanel;
    public GameObject okButton;

    public GameObject bgmScroll;
    public GameObject soundeffectScroll;

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
        tempdiamond = 50;
        tempNickkname = "닉네임입니다.";
    }

    // Update is called once per frame
    void Update()
    {
        heart.text = tempheart.ToString();
        diamond.text = tempdiamond.ToString();
        nickname.text = tempNickkname.ToString();

        //bgmScroll.GetComponentInChildren<Scrollbar>().image.fillAmount = 1;
    }

    public void Setting_On()
    {
        settingPanel.SetActive(true);
    }
    public void Setting_OFF()
    {
        settingPanel.SetActive(false);
    }



}
