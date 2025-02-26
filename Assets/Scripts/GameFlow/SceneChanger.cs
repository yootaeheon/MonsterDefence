using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public static void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void SelectStage(int stageNum)
    {
        GameManager.Instance.ChallengeStage = stageNum;

        GameManager.Instance.MonsterCount = stageNum * 8;

        if (!CheckEligibility(stageNum))
        {
            Debug.Log("아직 진행 불가");
            return;
        }

        SceneManager.LoadScene(2);
        switch (stageNum)
        {
            case 1: GameManager.Instance.FilePath = "Map_01.csv"; break;
            case 2: GameManager.Instance.FilePath = "Map_02.csv"; break;
            case 3: GameManager.Instance.FilePath = "Map_03.csv"; break;
        }


    }

    public bool CheckEligibility(int ChallengeStage)
    {
        if (ChallengeStage - DatabaseManager.Instance.CurStage  <= 1) //지금이 1 도전 2
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
