using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataPanel : UIBInder
{
    private void Awake()
    {
        BindAll();
    }
    private void OnEnable()
    {
        DatabaseManager.Instance.onLevelChanged.AddListener(LevelChanged);
        DatabaseManager.Instance.onLevelChanged.AddListener(CurStageChanged);
        DatabaseManager.Instance.onLevelChanged.AddListener(GoldChanged);
    }

    private void Start()
    {
        GetUI<TextMeshProUGUI>("NickNameText").text = BackendManager.Auth.CurrentUser.DisplayName;
    }
    

    private void LevelChanged()
    {
        GetUI<TextMeshProUGUI>("LevelText").text = $"{DatabaseManager.Instance.Level}.LV";
    }

    private void CurStageChanged()
    {
        GetUI<TextMeshProUGUI>("CurStage").text = $"{DatabaseManager.Instance.CurStage}.Stage";
    }

    private void GoldChanged()
    {
        GetUI<TextMeshProUGUI>("GoldText").text = $"{DatabaseManager.Instance.Gold}G";
    }

}
