using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public  class SceneChanger : MonoBehaviour
{
   

    public static void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public  void SelectStage(int stageNum)
    {
        SceneManager.LoadScene(2);

        switch (stageNum)
        {
            case 1: GameManager.Instance.FilePath = "Map_01.csv"; break;
            case 2: GameManager.Instance.FilePath = "Map_02.csv"; break;
            case 3: GameManager.Instance.FilePath = "Map_03.csv"; break;
        }
    }
}
