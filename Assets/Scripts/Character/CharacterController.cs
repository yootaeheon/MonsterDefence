using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ĳ������ ��Ʈ�ѷ� ���� (���� ���)
/// </summary>
public class CharacterController : MonoBehaviour 
{
    public CharacterModel Model;

    public Adapter Adapter { get; set; }

    [HideInInspector] public int MonsterLayer = 1 << 6;
    
    private StateMachine _state;


    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        Model.CurHp = Model.MaxHp;
    }

    private void Update()
    {
        _state.OnUpdate();

        if (Model.CurMana >= Model.MaxMana)
        {
            Model.CurMana = Model.MaxMana;
        }
    }

    // ���µ��� �ʱ�ȭ
    private void Init()
    {
        _state = new StateMachine(new StateIdle(this), new StateAttack(this), new StateSkill(this));
    }

    // ���� ���� ����� ���
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 boxPosition = transform.position;
        Vector2 boxSize = Model.AttackRange;

        Gizmos.DrawWireCube(boxPosition, boxSize);
    }
}