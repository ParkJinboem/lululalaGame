using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DailyReward : MonoBehaviour
{

    public GameObject dailyCheck_reward;
    public GameObject dailyCheck_Vmark;
    public GameObject dailyCheck_Dailybox;
    public DailyCheckBlinkAnim dailyCheck_DailyboxBlink;
    public bool[] attandDay = new bool[28];


    private void Awake()
    {
        //dailyCheck_DailyboxBlink = GetComponent<DailyCheckBlinkAnim>();
       
        dailyCheck_reward = this.gameObject;
        dailyCheck_Dailybox = transform.GetChild(1).gameObject;
        int day = int.Parse(gameObject.name);   //현재 오브젝트의 이름(숫자)을 날짜로 넣어줌
        attandDay[DateTime.Now.Day-1] = true;

        if (day == DateTime.Now.Day  && attandDay[DateTime.Now.Day - 1] ==true)
        {
            Debug.Log("오늘");
            dailyCheck_reward.GetComponent<Button>().interactable = true;
            dailyCheck_Dailybox.SetActive(true);
        }
        else if (day < DateTime.Now.Day)
        {
            Debug.Log("오늘아님");
            dailyCheck_reward.GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f, 1.0f);
            dailyCheck_reward.GetComponent<Button>().interactable = false;
            dailyCheck_Dailybox.SetActive(false);
        }
        else if(day > DateTime.Now.Day)
        {
            dailyCheck_Dailybox.SetActive(false);
        }
    }

    public void DailyCheck_reward()
    {

        int day = int.Parse(gameObject.name);   //현재 오브젝트의 이름(숫자)을 날짜로 넣어줌


        if (day < DateTime.Now.Day)
        {
            dailyCheck_reward.GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f, 1.0f);
            dailyCheck_reward.GetComponent<Button>().interactable = false;

        }
        else if (day == DateTime.Now.Day)
        {
            dailyCheck_reward.GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f, 1.0f);
            dailyCheck_reward.GetComponent<Button>().interactable = false;
            dailyCheck_Vmark.SetActive(true);
            dailyCheck_Dailybox.GetComponent<Image>().color = new Color(255, 0, 0, 255);   
            dailyCheck_DailyboxBlink.GetComponent<DailyCheckBlinkAnim>().time = 10.0f;
      


        }
        else
        {
            Debug.Log("미래입니다.");
        }


    }
}
