using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class AnimDefine
{
    public static int Idle = Animator.StringToHash("Idle");
    public static int Move = Animator.StringToHash("Move");
    public static int Dead = Animator.StringToHash("Dead");
    public static int TakeDamage = Animator.StringToHash("TakeDamage");

    public static int CharIdle = Animator.StringToHash("CharIdle");
    public static int CharAttack = Animator.StringToHash("CharAttack");
    public static int CharSkill = Animator.StringToHash("CharSkill");
}
