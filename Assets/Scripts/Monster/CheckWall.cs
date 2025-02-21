using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// 1. 타일 맵 전체를 벽으로 간주하고 진행
/// </summary>
public class CheckWall : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] GameObject checker;


    private void Start()
    {
        for (int y = tilemap.origin.y; y < tilemap.origin.y + tilemap.size.y; y += (int)tilemap.cellSize.y)
        {
            for (int x = tilemap.origin.x; x < tilemap.origin.x + tilemap.size.x; x += (int)tilemap.cellSize.x)
            {
                // 물리 엔진을 이용한 특정 지점에 충돌체가 있는지 없는지를 기준으로 벽을 체크
                Collider2D col =Physics2D.OverlapCircle(new Vector2(x,y), 0.4f);
                if (col != null)
                {
                    Instantiate(checker, new Vector3(x + tilemap.tileAnchor.x, y + tilemap.tileAnchor.y, 0), Quaternion.identity);
                }


                // 타일 맵의 타일이 있는지 없는지를 기준으로 벽을 체크
                /*bool hasTile = tilemap.HasTile(new Vector3Int(x, y,0));
                if (hasTile)
                {
                    Instantiate(checker, new Vector3(x+tilemap.tileAnchor.x, y+tilemap.tileAnchor.y, 0), Quaternion.identity);
                }*/
            }
        }
    }
}
