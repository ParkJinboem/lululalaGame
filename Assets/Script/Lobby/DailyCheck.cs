using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DailyCheck : MonoBehaviour
{

    //�⼮üũ �˾�â
    public GameObject dailyCheck_Panel;
    public GameObject dailyCheck_Xbox;





    public void DailyCheck_on()
    {
        dailyCheck_Panel.SetActive(true);
    }
    public void DailyCheck_Xbox()
    {
        dailyCheck_Panel.SetActive(false);
    }
 
}
