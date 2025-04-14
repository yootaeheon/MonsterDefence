using BasicNavMesh;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacterPanel : MonoBehaviour
{
    [SerializeField] Button spearButton;
    [SerializeField] Button fireButton;
    [SerializeField] Button samuraiButton;
    [SerializeField] Button longSwordButton;
    [SerializeField] Button staffButton;

    [SerializeField] MapGenerator mapGenerator;

    private CharacterSpawner spawner;

    private void Awake()
    {
        spawner = GetComponent<CharacterSpawner>();
    }

    private void Start()
    {
        spearButton.onClick.AddListener(() => SelectCharacter(1, spearButton));
        fireButton.onClick.AddListener(() => SelectCharacter(2, fireButton));
        samuraiButton.onClick.AddListener(() => SelectCharacter(3, samuraiButton));
        longSwordButton.onClick.AddListener(() => SelectCharacter(4, longSwordButton));
        staffButton.onClick.AddListener(() => SelectCharacter(5, staffButton));
    }

    public void SelectCharacter(int index, Button clickedButton)
    {
        spawner.charNum = index;
        spawner.selected = true;
        Debug.Log($"{index}번 캐릭터 선택");

        clickedButton.enabled = false;
        
        foreach (GameObject button in mapGenerator.buttons)
        {
            button.gameObject.SetActive(true);
        }
    }
}
