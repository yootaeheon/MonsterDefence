using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/SkillData")]
public class SkillSO : ScriptableObject
{
    public string SkillName;

    public int SkillDamage; // ��ų �������� ������� �ʴ� ��ų�� 0 ���� ó��

    public string CoolTime;

    public bool CanUse;

    public int Cost;

    public AnimationClip SkillClip;

}
