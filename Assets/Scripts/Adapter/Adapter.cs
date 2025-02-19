using UnityEngine;

public class Adapter : MonoBehaviour
{
    [SerializeField] StatusSO _status;

    [SerializeField] WeaponSO _weapon;

    [SerializeField] SkillSO _skill;

    private CharacterController _controller;
    private CharacterModel _model => _controller.Model;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        // Status �ʱ�ȭ
        _model.MaxHp = _status.MaxHp;
        _model.MaxMana = _status.MaxMana;
        _model.Defense = _status.Defense;

        // Weapon �ʱ�ȭ
        _model.WeaponName = _weapon.WeaponName;
        _model.Damage = _weapon.Damage;
        _model.AttackRange = _weapon.AttackRange;
        _model.AttackDelay = _weapon.AttackDelay;
        _model.WeaponAnimName = _weapon.AnimName;

        // Skill �ʱ�ȭ
        _model.SkillName = _skill.SkillName;
        _model.CoolTime = _skill.CoolTime;
        _model.CanUse = _skill.CanUse;
        _model.Cost = _skill.Cost;
    }
}

