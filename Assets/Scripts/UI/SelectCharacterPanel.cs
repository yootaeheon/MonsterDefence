using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacterPanel : MonoBehaviour
{
    [SerializeField] Button spearButton; 
    [SerializeField] Button fireButton; 
    [SerializeField] Button samuraiButton; 
    [SerializeField] Button longSwordButton; 
    [SerializeField] Button staffButton; 

    private CharacterSpawner spawner;

    private void Awake()
    {
        spawner = GetComponent<CharacterSpawner>();
    }

    private void Start()
    {
        spearButton.onClick.AddListener(() => SelectCharacter(1));
        fireButton.onClick.AddListener(() => SelectCharacter(2));
        samuraiButton.onClick.AddListener(() => SelectCharacter(3));
        longSwordButton.onClick.AddListener(() => SelectCharacter(4));
        staffButton.onClick.AddListener(() => SelectCharacter(5));
    }

    public void SelectCharacter(int index)
    {
        spawner.charNum = index;
        spawner.selected = true;
        Debug.Log($"{index}번 캐릭터 선택");
    }
}
