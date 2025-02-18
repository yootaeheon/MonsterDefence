using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Init()
    {
        _state = new StateMachine(new StateIdle(this), new StateAttack(this));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 boxPosition = transform.position;
        Vector2 boxSize = Model.AttackRange;

        Gizmos.DrawWireCube(boxPosition, boxSize);
    }
}