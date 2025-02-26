using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectStagePanel : MonoBehaviour
{
    [SerializeField] Button stageOneButton;
    [SerializeField] Button stageTwoButton;
    [SerializeField] Button stageThreeButton;

    private SceneChanger _sceneChanger;

    private void Awake()
    {
        _sceneChanger = GetComponent<SceneChanger>();
    }

    void Start()
    {
        stageOneButton.onClick.AddListener(() => _sceneChanger.SelectStage(1));
        stageTwoButton.onClick.AddListener(() => _sceneChanger.SelectStage(2));
        stageThreeButton.onClick.AddListener(() => _sceneChanger.SelectStage(3));
    }
}
