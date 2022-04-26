using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DailyCheckBlinkAnim : MonoBehaviour
{
    public float time;

    private void Start()
    {
        gameObject.SetActive(true);
        StartCoroutine(ImageBlink());
        Debug.Log("Ω√¿€");

    }

    private void Update()
    {
        //if (time < 0.5f)
        //{
        //    GetComponent<Image>().color = new Color(255, 0, 0, 0);
        //}
        //else
        //{
        //    GetComponent<Image>().color = new Color(255, 0, 0, 255);

        //    if (time > 1f)
        //    {
        //        time = 0;
        //    }
        //}
        //time += Time.deltaTime;
    }

    public IEnumerator ImageBlink()
    {
        if (time < 1.0f)
        {
            GetComponent<Image>().color = new Color(255, 0, 0, 0);
        }
        else if(time > 9.0f)
        {
            GetComponent<Image>().color = new Color(255, 0, 0, 255);
        }
        else
        {
            GetComponent<Image>().color = new Color(255, 0, 0, 255);

            if (time > 1.0f)
            {
                time = 0;
            }
        }
        time += Time.deltaTime * 50;

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ImageBlink());
    }

}
