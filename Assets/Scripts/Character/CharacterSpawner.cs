using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> Characters = new List<GameObject>();

    public int charNum { get; set; }

    public bool selected { get; set; }

    private void Awake()
    {
        selected = false;
    }

    public void Spawn(Vector2 spawnPos)
    {
        Instantiate(Characters[charNum-1], spawnPos, Quaternion.identity);
        Debug.Log($"{charNum}번 캐릭터 생성");
        selected = false;
        charNum = 0;
    }

    private void Update()
    {
        /*Vector2 mousePos = Input.mousePosition;
        mousePos = Camera.main.WorldToScreenPoint(mousePos);

        Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);*/

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        if (selected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Spawn(mousePosition);
            }
        }
    }
}
