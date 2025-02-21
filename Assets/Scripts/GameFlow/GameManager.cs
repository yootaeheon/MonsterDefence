using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    #region Property
    [SerializeField] int _maxLife;
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
}
