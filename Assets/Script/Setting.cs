using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public Scrollbar bgm_Scrollbar;
    public Image bgm_ScrollGauge;

    public Scrollbar soundEffect_Scrollbar; 
    public Image soundEffect_ScrollGauge;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
        bgm_ScrollGauge.fillAmount = bgm_Scrollbar.value;
        soundEffect_ScrollGauge.fillAmount = soundEffect_Scrollbar.value;
    }
}
