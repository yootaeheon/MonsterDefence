using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/SkillData")]
public class SkillSO : ScriptableObject
{
    public enum Type_Skill { PowerUp, SpeedUp }

    public string SkillName;

    public int SkillDamage; // ��ų �������� ������� �ʴ� ��ų�� 0 ���� ó��

    public string CoolTime;

    public bool CanUse;

    public int Cost;
}
