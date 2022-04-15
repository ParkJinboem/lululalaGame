using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScendLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("스페이스");
            AppCheck.LoadScene("01LoadingScene");
        }
    }
}
