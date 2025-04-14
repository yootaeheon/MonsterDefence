using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace BasicNavMesh
{
    public class MapGenerator : MonoBehaviour
    {
        private Tilemap wallTilemap; // 씬에 Grid-TileMap 있는 하나의 타일이 생성됨

        private string filePath;

        [SerializeField] TileBase wallTile; // 타일 오브젝트
        [SerializeField] GameObject startPrefab;
        [SerializeField] GameObject endPrefab;
        [SerializeField] Button spawnButton; // UI 버튼 프리팹

        public List<GameObject> buttons = new List<GameObject>();

        [SerializeField] Canvas canvas; // 버튼이 추가될 캔버스

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
                        Vector3Int tilePosition = new Vector3Int(x, -y, 0); // 2D 좌표 (y는 반전)
                        wallTilemap.SetTile(tilePosition, wallTile);

                        // Wall 타일 생성 후 버튼도 함께 생성
                        CreateButtonAtTilePosition(tilePosition);
                    }
                    else if (data[y, x] == "Start")
                    {
                        Vector3Int startPos = new Vector3Int(x, -y, 0); // 2D 좌표 (y는 반전)
                        Instantiate(startPrefab, startPos, Quaternion.identity);
                    }
                    else if (data[y, x] == "End")
                    {
                        Vector3Int endPos = new Vector3Int(x, -y, 0); // 2D 좌표 (y는 반전)
                        Instantiate(endPrefab, endPos, Quaternion.identity);
                    }
                }
            }
        }

        private void CreateButtonAtTilePosition(Vector3Int tilePosition)
        {
            // 타일 위치 -> 월드 좌표 변환
            Vector3 worldPosition = wallTilemap.CellToWorld(tilePosition);

            // 월드 좌표 -> 화면 좌표 변환
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
            Vector2 realSpawnPos = new Vector2(screenPosition.x + 45, screenPosition.y + 45);

            // 버튼 인스턴스화 후, 캔버스의 자식으로 설정
            Button newButton = Instantiate(spawnButton, realSpawnPos, Quaternion.identity, canvas.transform);
            newButton.gameObject.SetActive(false);
            /*newButton.GetComponent<SpawnButtonData>().CanSpawn = true;*/
            buttons.Add(newButton.gameObject);

            // 버튼 클릭 이벤트 설정
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
