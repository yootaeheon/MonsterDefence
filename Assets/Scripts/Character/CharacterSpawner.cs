using BasicNavMesh;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> Characters = new List<GameObject>();

    [SerializeField] MapGenerator mapGenerator;

    public int charNum { get; set; }

    public bool selected { get; set; }

    private void Awake()
    {
        selected = false;
    }

    public void Spawn(Vector2 spawnPos)
    {
        if (!selected)
            return;

        Instantiate(Characters[charNum-1], spawnPos, Quaternion.identity);
        Debug.Log($"{charNum}번 캐릭터 생성");
        selected = false;
        charNum = 0;

        foreach (GameObject button in mapGenerator.buttons)
        {
            button.gameObject.SetActive(false);
        }
    }
}
