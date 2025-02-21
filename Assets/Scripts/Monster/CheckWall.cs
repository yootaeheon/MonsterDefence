using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// 1. Ÿ�� �� ��ü�� ������ �����ϰ� ����
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
                // ���� ������ �̿��� Ư�� ������ �浹ü�� �ִ��� �������� �������� ���� üũ
                Collider2D col =Physics2D.OverlapCircle(new Vector2(x,y), 0.4f);
                if (col != null)
                {
                    Instantiate(checker, new Vector3(x + tilemap.tileAnchor.x, y + tilemap.tileAnchor.y, 0), Quaternion.identity);
                }


                // Ÿ�� ���� Ÿ���� �ִ��� �������� �������� ���� üũ
                /*bool hasTile = tilemap.HasTile(new Vector3Int(x, y,0));
                if (hasTile)
                {
                    Instantiate(checker, new Vector3(x+tilemap.tileAnchor.x, y+tilemap.tileAnchor.y, 0), Quaternion.identity);
                }*/
            }
        }
    }
}
