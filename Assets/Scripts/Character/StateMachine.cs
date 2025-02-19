using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ���� ����
/// </summary>
public enum StateType
{
    Idle, Attack, Skill
}

/// <summary>
/// �� ���¸� �����ϰ� ��ȯ���ִ� ������ Ŭ����
/// ���� �ӽ�
/// </summary>

public class StateMachine
{
    private Dictionary<StateType, CharacterState> _stateContainer;             // ��ųʸ��� ���µ��� Ű�� �޾ư� ĳ���� ������Ʈ�� ������ �Ͽ� ��ųʸ��� ������ �����̳� ����
    public StateType CurrentType { get; private set; }                         // ���� ������ �� ���� ����
    private CharacterState CurrentState => _stateContainer[CurrentType];       // ��ųʸ����� ���� ���¸� ��ȯ�ϴ� ��
                                                                               
    public StateMachine(params CharacterState[] states)                        // ���� �ӽ� (�����̳� �ν��Ͻ��� �����ϰ� �� ���µ��� �������)
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
                                                                              
        CurrentType = states[0].stateType;                                     // ���� �� Idle() ���·� ����
        CurrentState.Enter();                                                 
    }                                                                         
                                                                              
    public void OnUpdate()                                                     // ���� ������ OnUpdate�� �����ϰ� ����
    {                                                                        
        CurrentState.OnUpdate();                                             
    }                                                                        
                                                                             
    public void ChangeState(StateType state)                                   // ���¸� ��ȯ�ϴ� ���
    {                                                                         
        CurrentType = state;                                                  
        Debug.Log($"���� ���� {state}");                                       
        CurrentState.Enter();                                                 
    }
}