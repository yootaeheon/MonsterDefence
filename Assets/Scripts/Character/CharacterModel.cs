using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterModel
{
    [SerializeField] Vector2 _attackRange;
    public Vector2 AttackRange { get { return _attackRange; } set { _attackRange = value; } }

    [SerializeField] int _damage;
    public int Damage { get { return _damage; } set { _damage = value; } }
}
