using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterState
{
    public CharacterController Controller { get; private set; }
    public StateMachine Machine { get; set; }
    public StateType ThisType { get; protected set; }

    public CharacterState(CharacterController controller)
    {
        Controller = controller;
        Init();
    }

    public abstract void Init();
    public virtual void Enter() { }
    public virtual void OnUpdate() { }
    public virtual void Exit() { }
}
