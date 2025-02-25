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
    }
}
