using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    #region Property
    public string FilePath { get; set; }

    [SerializeField] int _maxLife = 20;
    public int MaxLife { get { return _maxLife; } }

    [SerializeField] int _curLife;
    public int CurLife { get { return _curLife; } set { _curLife = value; } }
    #endregion


    private void Awake()
    {
        SetSingleton();

        CurLife = MaxLife;
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (CurLife <= 0)
        {
            Debug.Log("Game Over");
        }
    }

    public bool CheckEligibility(int ChallengeStage)
    {
        if (DatabaseManager.Instance.CurStage < ChallengeStage - 1)
        {
            return false;
        }
        return true;
    }

    public void ClearStage(int ChallengeStage)
    {
        if (DatabaseManager.Instance.CurStage < ChallengeStage)
        {
            DatabaseManager.Instance.CurStage = ChallengeStage;
        }
    }
}
