using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Monster : MonoBehaviour
{
    #region 데이터-프로퍼티
    [SerializeField] new string name;
    public string Name { get { return name; } set { Name = value; } }

    [SerializeField] float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }

    [SerializeField] int maxHp;
    public int MaxHp { get { return maxHp; } set { maxHp = value; } }

    [SerializeField] int curHp;
    public int CurHp { get {return curHp; } set { curHp = value; } }

    [SerializeField] float defense;
    public float Defense { get { return defense; } set { defense = value; } }
    #endregion

    private List<Vector2Int> path;
    public List<Vector2Int> Path { get { return path; } set { path = value; } }

    private void Awake()
    {
        curHp = maxHp;
    }
}
