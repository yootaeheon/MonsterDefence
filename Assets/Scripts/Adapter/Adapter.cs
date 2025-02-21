using UnityEngine;

[System.Serializable]
public class Adapter : MonoBehaviour
{
    [SerializeField] StatusSO _status;
    public StatusSO Status {  get { return _status; }  set { _status = value; } }

    [SerializeField] WeaponSO _weapon;
    public WeaponSO Weapon {  get { return _weapon; }  set { _weapon = value; } }

    [SerializeField] SkillSO _skill;
    public SkillSO Skill { get { return _skill; } set { _skill = value; } }

    private CharacterController _controller;
    private CharacterModel _model => _controller.Model;


    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        // Status 초기화
        _model.MaxHp = Status.MaxHp;
        _model.MaxMana = Status.MaxMana;
        _model.Defense = Status.Defense;

        // Weapon 초기화
        _model.WeaponName = Weapon.WeaponName;
        _model.Damage = Weapon.Damage;
        _model.AttackRange = Weapon.AttackRange;
        _model.AttackDelay = Weapon.AttackDelay;
        _model.WeaponAnimName = Weapon.AnimName;

        _model.Sprite = Weapon.IdleSR;

        // Skill 초기화
        _model.SkillName = Skill.SkillName;
        _model.SkillDamage = Skill.SkillDamage;
        _model.CoolTime = Skill.CoolTime;
        _model.CanUse = Skill.CanUse;
        _model.Cost = Skill.Cost;
    }
}

