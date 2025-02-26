using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    #region Property
    public string FilePath { get; set; }

    public int ChallengeStage {  get; set; }

    [SerializeField] int _maxLife = 5;
    public int MaxLife { get { return _maxLife; } }

    [SerializeField] int _curLife;
    public int CurLife { get { return _curLife; } set { _curLife = value; } }

    [SerializeField] int monsterCount;
    public int MonsterCount { get { return monsterCount; } set { monsterCount = value; } }
    #endregion

    public UnityEvent OnStageClear = new UnityEvent();

   

    private void Awake()
    {
        SetSingleton();

        CurLife = MaxLife;
    }

    private void OnEnable()
    {
        OnStageClear.AddListener(Clear);
    }

    private void OnDisable()
    {
        OnStageClear.RemoveAllListeners();
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

    private void Start()
    {
        monsterCount = ChallengeStage * 8;
    }

    private void Update()
    {
        if (CurLife <= 0)
        {
            Debug.Log("Game Over");
            SceneChanger.ChangeScene(1);
            return;
        }
    }

    public bool CheckEligibility(int ChallengeStage)
    {
        if (DatabaseManager.Instance.CurStage - ChallengeStage <= 1) 
        { 
            return true; 
        }
        else
        {
            return false;
        }
    }

    public void ClearStage(int ChallengeStage)
    {
        SceneChanger.ChangeScene(1);

        if (DatabaseManager.Instance.CurStage < ChallengeStage)
        {
            DatabaseManager.Instance.CurStage = ChallengeStage;
        }

        
    }

    public void Clear()
    {
        ClearStage(ChallengeStage);
    }
}
