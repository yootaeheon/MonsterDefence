using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Monster : MonoBehaviour
{
    #region 데이터-프로퍼티
    [SerializeField] new string name;
    public string Name { get { return name; } set { Name = value; } }

    [SerializeField] float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }

    [SerializeField] float maxHp;
    public float MaxHp { get { return maxHp; } set { maxHp = value; } }

    [SerializeField] float curHp;
    public float CurHp { get {return curHp; } set { curHp = value; } }

    [SerializeField] float defense;
    public float Defense { get { return defense; } set { defense = value; } }
    #endregion

    private List<Vector2Int> path;

    private void Awake()
    {
        curHp = maxHp;
    }

    private void Start()
    {   
        path = GetComponent<PathFinder>().Path;
    }

    // 탐색한 최단 경로를 따라 이동하는 코루틴
    public  IEnumerator FollowPath()
    {
        if (path == null || path.Count == 0)
            yield break;

        foreach (Vector2Int point in path)
        {
            Vector3 targetPos = new Vector3(point.x, point.y, transform.position.z);

            while (Vector3.Distance(transform.position, targetPos) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
