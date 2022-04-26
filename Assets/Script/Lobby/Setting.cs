using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{

    public GameObject settingPanel;
    public GameObject okButton;
    public Scrollbar bgm_Scrollbar;
    public Image bgm_ScrollGauge;
    public Scrollbar soundEffect_Scrollbar; 
    public Image soundEffect_ScrollGauge;

    // Update is called once per frame
    void Update()
    {
 
        bgm_ScrollGauge.fillAmount = bgm_Scrollbar.value;
        soundEffect_ScrollGauge.fillAmount = soundEffect_Scrollbar.value;
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
