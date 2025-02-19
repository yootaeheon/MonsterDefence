using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 추상 클래스 이용 캐릭터 상태 선언
/// 각 상태들은 캐릭터스테이트를 상속받음
/// </summary>
public abstract class CharacterState
{
    public CharacterController Controller { get; private set; }         // 캐릭터 컨트롤러 참조
    public StateMachine Machine { get; set; }                           // 상태 전환 해주는 상태머신 참조
    public StateType stateType { get; protected set; }                  // 상태 열거형

    public CharacterModel Model;
                                                                        
    public CharacterState(CharacterController controller)               // 상태 전환 후 초기화 과정 
    {                                                                   
        Controller = controller;                                        
        Init();                                                         
    }                                                                   
                                                                        
    public abstract void Init();                                        
    public virtual void Enter() { }                                     
    public virtual void OnUpdate() { }                                  
    public virtual void Exit() { }                                      
}
