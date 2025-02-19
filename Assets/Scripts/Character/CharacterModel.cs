using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 캐릭터 모델 (데이터 담당)
/// </summary>
[System.Serializable]
public class CharacterModel
{
    [SerializeField] Vector2 _attackRange;
    public Vector2 AttackRange { get { return _attackRange; } set { _attackRange = value; } }

    [SerializeField] int _damage;
    public int Damage { get { return _damage; } set { _damage = value; } }
}
