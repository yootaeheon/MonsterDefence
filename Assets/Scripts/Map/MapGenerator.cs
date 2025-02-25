using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BasicNavMesh
{
    public class MapGenerator : MonoBehaviour
    {
        private Tilemap wallTilemap; // 씬에 Grid-TileMap 있는 하나의 타일이 생성됨

        private string filePath;

        [SerializeField] TileBase wallTile; // 타일 오브젝트
        [SerializeField] GameObject startPrefab;
        [SerializeField] GameObject endPrefab;

        private CSVParser data = new CSVParser();

        private List<string> Map = new List<string>() {"Map_01", "Map_02", "Map_03" };

        private void Awake()
        {
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
    }
}
