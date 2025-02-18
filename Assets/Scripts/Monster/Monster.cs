using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Monster : MonoBehaviour
{
    #region Data - Property
    [SerializeField] string _name;
    public string Name { get { return _name; } set { Name = value; } }

    [SerializeField] float _moveSpeed;
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    [SerializeField] int _maxHp;
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }

    [SerializeField] int _curHp;
    public int CurHp { get {return _curHp; } set { _curHp = value; } }

    [SerializeField] int _defense;
    public int Defense { get { return _defense; } set { _defense = value; } }
    #endregion

    private void Awake()
    {
        _curHp = _maxHp;
    }
}
