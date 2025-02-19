using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/SkillData")]
public class SkillSO : ScriptableObject
{
    public string SkillName;

    public string CoolTime;

    public bool CanUse;

    public int Cost;
}
