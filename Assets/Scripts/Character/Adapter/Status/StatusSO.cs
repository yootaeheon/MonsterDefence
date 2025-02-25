using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/StatusData")]
public class StatusSO : ScriptableObject
{
    public string CharacterName;

    public int MaxHp;

    public int Defense;

    public int MaxMana;
}
