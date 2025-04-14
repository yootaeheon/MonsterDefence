using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultCanvas : UIBInder
{
    [SerializeField] GameObject _clearPanel;
    [SerializeField] GameObject _gameOverPanel;

    private void Awake()
    {
        BindAll();
    }

    private void Update()
    {
        GetUI<TextMeshProUGUI>("KillCountText").text = $"KillCount : {GameManager.Instance.MonsterCount} / {GameManager.Instance.ChallengeStage * 8}";
        GetUI<TextMeshProUGUI>("CurLifeText").text = $"Life : {GameManager.Instance.CurLife} / 5";
    }

    public void OnClearPanel()
    {
        _clearPanel.gameObject.SetActive(true);
    }

    public void OffClearPanel()
    {
        _clearPanel.gameObject.SetActive(false);
    }

    public void OnGameOverPanel()
    {
        _gameOverPanel.gameObject.SetActive(true);
    }

    public void OffGameOverPanel()
    {
        _gameOverPanel.gameObject.SetActive(false);
    }
}
