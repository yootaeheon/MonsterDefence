using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/StatusData")]
public class StatusSO : ScriptableObject
{
    public int MaxHp;

    public int Defense;

    public int MaxMana;

    // 캐릭터 의상? 생김새 추가
}
