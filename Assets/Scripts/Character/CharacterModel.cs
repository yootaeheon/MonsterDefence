using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 캐릭터 모델 (데이터 담당)
/// </summary>
[System.Serializable]
public class CharacterModel
{
    [Header("Status")]
    [SerializeField] string _characterName;
    public string CharacterName { get { return _characterName; } set { _characterName = value; } }

    [SerializeField] int _maxHp;
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }

    [SerializeField] int _curHp;
    public int CurHp { get { return _curHp; } set { _curHp = value; } }

    [SerializeField] int _maxMana;
    public int MaxMana { get { return _maxMana; } set { _maxMana = value; } }

    [SerializeField] int _curMana;
    public int CurMana { get { return _curMana; } set { _curMana = value; } }

    [SerializeField] int _defense;
    public int Defense { get { return _defense; } set { _defense = value; } }



    [Header("Weapon")]
    [SerializeField] string _weaponName;
    public string WeaponName { get { return _weaponName; } set { _weaponName = value; } }

    [SerializeField] Vector2 _attackRange;
    public Vector2 AttackRange { get { return _attackRange; } set { _attackRange = value; } }

    [SerializeField] int _attackDelay;
    public int AttackDelay { get { return _attackDelay; } set { _attackDelay = value; } }

    [SerializeField] int _damage;
    public int Damage { get { return _damage; } set { _damage = value; } }


    public enum Type_Skill { PowerUp, SpeedUp }

    [Header("Skill")]
    [SerializeField] Type_Skill _skillType;
    public Type_Skill SkillType { get { return _skillType; } }

    [SerializeField] string _skillName;
    public string SkillName { get { return _skillName; } set { _skillName = value; } }

    [SerializeField] int _skillDamage;
    public int SkillDamage { get { return _skillDamage; } set { _skillDamage = value; } }

    [SerializeField] string _coolTime;
    public string CoolTime { get { return _coolTime; } set { _coolTime = value; } }

    [SerializeField] bool _canUse;
    public bool CanUse { get { return _canUse; } set { _canUse = value; } }

    [SerializeField] int _cost;
    public int Cost { get { return _cost; } set { _cost = value; } }
}
