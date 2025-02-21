using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/SkillData")]
public class SkillSO : ScriptableObject
{
    public string SkillName;

    public int SkillDamage; // 스킬 데미지를 사용하지 않는 스킬은 0 으로 처리

    public string CoolTime;

    public bool CanUse;

    public int Cost;

    public AnimationClip SkillClip;

}
