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

    public string AnimName; // HashToString으로 해싱할것 // 애니메이션/터 override 공부해보기
}
