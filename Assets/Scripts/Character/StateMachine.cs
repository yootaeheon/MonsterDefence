using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    Idle, Attack, Skill
}

public class StateMachine
{
    private Dictionary<StateType, CharacterState> _stateContainer;
    public StateType CurrentType { get; private set; }
    private CharacterState CurrentState => _stateContainer[CurrentType];

    public StateMachine(params CharacterState[] states)
    {
        _stateContainer = new Dictionary<StateType, CharacterState>();

        foreach (CharacterState state in states)
        {
            if (!_stateContainer.ContainsKey(state.ThisType))
            {
                _stateContainer.Add(state.ThisType, state);
            }
            state.Machine = this;
        }

        CurrentType = states[0].ThisType;
        CurrentState.Enter();
    }

    public void OnUpdate()
    {
        CurrentState.OnUpdate();
    }

    public void ChangeState(StateType state)
    {
        CurrentType = state;
        Debug.Log($"현재 상태 {state}");
        CurrentState.Enter();
    }
}