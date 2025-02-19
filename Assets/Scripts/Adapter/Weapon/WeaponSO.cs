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

    public string AnimName; // HashToString���� �ؽ��Ұ� // �ִϸ��̼�/�� override �����غ���
}
