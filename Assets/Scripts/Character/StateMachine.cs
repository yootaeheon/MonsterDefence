using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 만든 상태 종류
/// </summary>
public enum StateType
{
    Idle, Attack, Skill
}

/// <summary>
/// 각 상태를 저장하고 전환해주는 역할의 클래스
/// 상태 머신
/// </summary>

public class StateMachine
{
    private Dictionary<StateType, CharacterState> _stateContainer;             // 딕셔너리로 상태들을 키로 받아고 캐릭터 스테이트를 값으로 하여 딕셔너리에 저장한 컨테이너 생성
    public StateType CurrentType { get; private set; }                         // 상태 열거형 중 현재 상태
    private CharacterState CurrentState => _stateContainer[CurrentType];       // 딕셔너리에서 현재 상태를 반환하는 값
                                                                               
    public StateMachine(params CharacterState[] states)                        // 상태 머신 (컨테이너 인스턴스를 생성하고 각 상태들을 집어넣음)
    {                                                                         
        _stateContainer = new Dictionary<StateType, CharacterState>();        
                                                                              
        foreach (CharacterState state in states)                              
        {                                                                     
            if (!_stateContainer.ContainsKey(state.stateType))                
            {                                                                 
                _stateContainer.Add(state.stateType, state);                  
            }                                                                 
            state.Machine = this;                                             
        }                                                                     
                                                                              
        CurrentType = states[0].stateType;                                     // 시작 시 Idle() 상태로 시작
        CurrentState.Enter();                                                 
    }                                                                         
                                                                              
    public void OnUpdate()                                                     // 현재 상태의 OnUpdate를 실행하게 해줌
    {                                                                        
        CurrentState.OnUpdate();                                             
    }                                                                        
                                                                             
    public void ChangeState(StateType state)                                   // 상태를 전환하는 기능
    {                                                                         
        CurrentType = state;                                                  
        Debug.Log($"현재 상태 {state}");                                       
        CurrentState.Enter();                                                 
    }
}