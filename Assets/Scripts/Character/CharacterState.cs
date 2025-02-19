using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �߻� Ŭ���� �̿� ĳ���� ���� ����
/// �� ���µ��� ĳ���ͽ�����Ʈ�� ��ӹ���
/// </summary>
public abstract class CharacterState
{
    public CharacterController Controller { get; private set; }         // ĳ���� ��Ʈ�ѷ� ����
    public StateMachine Machine { get; set; }                           // ���� ��ȯ ���ִ� ���¸ӽ� ����
    public StateType stateType { get; protected set; }                  // ���� ������

    public CharacterModel Model;
                                                                        
    public CharacterState(CharacterController controller)               // ���� ��ȯ �� �ʱ�ȭ ���� 
    {                                                                   
        Controller = controller;                                        
        Init();                                                         
    }                                                                   
                                                                        
    public abstract void Init();                                        
    public virtual void Enter() { }                                     
    public virtual void OnUpdate() { }                                  
    public virtual void Exit() { }                                      
}
