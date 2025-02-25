using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BasicNavMesh
{
    public class MapGenerator : MonoBehaviour
    {
        private Tilemap wallTilemap; // ���� Grid-TileMap �ִ� �ϳ��� Ÿ���� ������

        private string filePath;

        [SerializeField] TileBase wallTile; // Ÿ�� ������Ʈ
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
                        Vector3Int tilePosition = new Vector3Int(x, -y, 0); // 2D ��ǥ (y�� ����)
                        wallTilemap.SetTile(tilePosition, wallTile);
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
    }
}
