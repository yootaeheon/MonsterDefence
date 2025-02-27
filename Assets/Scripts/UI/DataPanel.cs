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

    private void Start()
    {
        GetUI<TextMeshProUGUI>("NickNameText").text = BackendManager.Auth.CurrentUser.DisplayName;
        GetUI<TextMeshProUGUI>("LevelText").text = $"{DatabaseManager.Instance.goldRef}.LV";
        Debug.Log(DatabaseManager.Instance.goldRef);
        Debug.Log(DatabaseManager.Instance);
        GetUI<TextMeshProUGUI>("GoldText").text = $"{1}G";
    }

    private void Update()
    {
        if (DatabaseManager.Instance.goldRef == null)
        {
            Debug.Log("≥Œ¿Ã¥Ÿ!!!!!!!!!!!!!!!!!!!!!!!!!");
            return;
        }

        GetUI<TextMeshProUGUI>("LevelText").text = $"{DatabaseManager.Instance.goldRef}.LV";
        Debug.Log(DatabaseManager.Instance.goldRef);
        Debug.Log(DatabaseManager.Instance);
    }
}
