using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CompanyScene : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(GotoLoadiong());
    }
    IEnumerator GotoLoadiong()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(1);
    }
}
