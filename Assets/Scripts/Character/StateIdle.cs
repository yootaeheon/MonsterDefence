using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class StateIdle : CharacterState
{
    private bool _canAttack;
    public StateIdle(CharacterController controller) : base(controller) { }

    public override void Init()
    {
        stateType = StateType.Idle;
    }

    public override void OnUpdate()
    {
        Debug.Log("Idle On Update");

        _canAttack = Physics2D.OverlapBox(Controller.transform.position, Controller.Model.AttackRange, 0, Controller.MonsterLayer);
        if (_canAttack)
        {
            Machine.ChangeState(StateType.Attack);
        }
    }
}