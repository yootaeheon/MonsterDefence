using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace BasicNavMesh
{
    public class MapGenerator : MonoBehaviour
    {
        private Tilemap wallTilemap; // ���� Grid-TileMap �ִ� �ϳ��� Ÿ���� ������

        private string filePath;

        [SerializeField] TileBase wallTile; // Ÿ�� ������Ʈ
        [SerializeField] GameObject startPrefab;
        [SerializeField] GameObject endPrefab;
        [SerializeField] Button spawnButton; // UI ��ư ������

        public List<GameObject> buttons = new List<GameObject>();

        [SerializeField] Canvas canvas; // ��ư�� �߰��� ĵ����

        private CSVParser data = new CSVParser();

        private List<string> Map = new List<string>() { "Map_01", "Map_02", "Map_03" };

        private CharacterSpawner spawner;

        private void Awake()
        {
            spawner = FindAnyObjectByType<CharacterSpawner>();
            filePath = GameManager.Instance.FilePath;

            wallTilemap = FindAnyObjectByType<Tilemap>();


            Generate();
        }

        public void Generate()
        {
            data.Load(filePath);

            for (int y = 0; y < data.SizeY; y++)
            {
                for (int x = 0; x < data.SizeX; x++)
                {
                    if (data[y, x] == "Wall")
                    {
                        Vector3Int tilePosition = new Vector3Int(x, -y, 0); // 2D ��ǥ (y�� ����)
                        wallTilemap.SetTile(tilePosition, wallTile);

                        // Wall Ÿ�� ���� �� ��ư�� �Բ� ����
                        CreateButtonAtTilePosition(tilePosition);
                    }
                    else if (data[y, x] == "Start")
                    {
                        Vector3Int startPos = new Vector3Int(x, -y, 0); // 2D ��ǥ (y�� ����)
                        Instantiate(startPrefab, startPos, Quaternion.identity);
                    }
                    else if (data[y, x] == "End")
                    {
                        Vector3Int endPos = new Vector3Int(x, -y, 0); // 2D ��ǥ (y�� ����)
                        Instantiate(endPrefab, endPos, Quaternion.identity);
                    }
                }
            }
        }

        private void CreateButtonAtTilePosition(Vector3Int tilePosition)
        {
            // Ÿ�� ��ġ -> ���� ��ǥ ��ȯ
            Vector3 worldPosition = wallTilemap.CellToWorld(tilePosition);

            // ���� ��ǥ -> ȭ�� ��ǥ ��ȯ
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
            Vector2 realSpawnPos = new Vector2(screenPosition.x + 45, screenPosition.y + 45);

            // ��ư �ν��Ͻ�ȭ ��, ĵ������ �ڽ����� ����
            Button newButton = Instantiate(spawnButton, realSpawnPos, Quaternion.identity, canvas.transform);
            newButton.gameObject.SetActive(false);
            /*newButton.GetComponent<SpawnButtonData>().CanSpawn = true;*/
            buttons.Add(newButton.gameObject);

            // ��ư Ŭ�� �̺�Ʈ ����
            newButton.onClick.AddListener(() =>
            {
                RectTransform buttonRect = newButton.GetComponent<RectTransform>();

                spawner.Spawn(Camera.main.ScreenToWorldPoint(buttonRect.position));
                newButton.GetComponent<Image>().enabled = false;
                newButton.onClick.AddListener(() => Destroy(newButton));
            });
        }


        private void OnButtonClicked(Vector2 buttonPos)
        {
            spawner.Spawn(buttonPos);
        }
    }
}
