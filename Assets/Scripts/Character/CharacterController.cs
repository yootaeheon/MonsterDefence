using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 캐릭터의 컨트롤러 역할 (로직 담당)
/// </summary>
public class CharacterController : MonoBehaviour 
{
    public CharacterModel Model;

    public Adapter Adapter { get; set; }

    public Animator Animator {  get; set; }    

    [HideInInspector] public int MonsterLayer = 1 << 6;
    
    private StateMachine _state;


    private void Awake()
    {
        Init();

        Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Model.CurHp = Model.MaxHp;
    }

    private void Update()
    {
        _state.OnUpdate();

        if (Model.CurMana >= Model.MaxMana)
        {
            Model.CurMana = Model.MaxMana;
        }
        if (Model.CurMana <= 0)
        {
            Model.CurMana = 0;
        }
    }

    // 상태들을 초기화
    private void Init()
    {
        _state = new StateMachine(new StateIdle(this), new StateAttack(this), new StateSkill(this));
    }

    // 공격 범위 기즈모 출력
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 boxPosition = transform.position;
        Vector2 boxSize = Model.AttackRange;

        Gizmos.DrawWireCube(boxPosition, boxSize);
    }

    public void Flip(bool isRight)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (isRight ? 1 : -1);
        transform.localScale = scale;
    }
}