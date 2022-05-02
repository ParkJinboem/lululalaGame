using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;


    public Text text_Diamond;
    public static int g_iDiamond =10;
    

    void Awake()
    {
        if(instance==null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        g_iDiamond = 0;
    }

    // Update is called once per frame
    void Update()
    {
        text_Diamond.text = g_iDiamond.ToString();
    }

    public void addDiamond()
    {
        g_iDiamond += 10;
    }
}
