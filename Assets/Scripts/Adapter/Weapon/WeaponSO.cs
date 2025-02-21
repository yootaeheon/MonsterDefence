using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/WeaponData")]
public class WeaponSO : ScriptableObject
{
    public string WeaponName;

    public int Damage;

    public Vector2 AttackRange;

    public int AttackDelay;

    public AnimationClip IdleClip;

    public AnimationClip AttackClip;

    public AnimationClip SkillClip;
}
