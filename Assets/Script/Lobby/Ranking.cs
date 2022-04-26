using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    public GameObject ranking_Panel;
    public GameObject ranking_Xbox;

    public void RankingPanel_on()
    {
        ranking_Panel.SetActive(true);
    }
    public void Ranking_Xbox()
    {
        ranking_Panel.SetActive(false);
    }
}
