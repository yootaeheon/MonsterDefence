using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class StateIdle : CharacterState
{
    public StateIdle(CharacterController controller) : base(controller) { }

    public override void Init()
    {
        stateType = StateType.Idle;
    }

    public override void OnUpdate()
    {
        Debug.Log("Idle On Update");

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Machine.ChangeState(StateType.Attack);
        }

        bool CanAttack = Physics2D.OverlapBox(Controller.transform.position, Controller.Model.AttackRange, 0, Controller.MonsterLayer);
        if (CanAttack)
        {
            Machine.ChangeState(StateType.Attack);
        }
    }
}