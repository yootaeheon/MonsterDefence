using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : CharacterState
{
    public StateIdle(CharacterController controller) : base(controller) { }

    public override void Init()
    {
        ThisType = StateType.Idle;
    }

    public override void OnUpdate()
    {
        Debug.Log("Idle On Update");

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Machine.ChangeState(StateType.Attack);
        }
    }
}