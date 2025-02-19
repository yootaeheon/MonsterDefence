using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 캐릭터의 컨트롤러 역할 (로직 담당)
/// </summary>
public class CharacterController : MonoBehaviour
{
    public CharacterModel Model;

    private StateMachine _state;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        _state.OnUpdate();
    }

    // 상태들을 초기화
    private void Init()
    {
        _state = new StateMachine(new StateIdle(this), new StateAttack(this));
    }

    // 공격 범위 기즈모 출력
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 boxPosition = transform.position;
        Vector2 boxSize = Model.AttackRange;

        Gizmos.DrawWireCube(boxPosition, boxSize);
    }
}