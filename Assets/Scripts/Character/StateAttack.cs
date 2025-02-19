using System.Collections;
using UnityEngine;

public class StateAttack : CharacterState
{
    private Collider2D cols;

    public StateAttack(CharacterController controller) : base(controller) { }

    public override void Init()
    {
        stateType = StateType.Attack;
    }

    public override void Enter()
    {
        /*Controller.StartCoroutine(AttackRoutine());*/
    }

    public override void OnUpdate()
    {
        Debug.Log("Attack On Update");
        if (cols == null)
        {
            cols = Physics2D.OverlapBox(Controller.transform.position, Controller.Model.AttackRange, 0, Controller.MonsterLayer);
        }
        else
        {
            if (Vector3.Distance(Controller.transform.position, cols.transform.position) > 6f)
            {
                cols = null;
                Exit();
                return;
            }

            if (attackRoutine == null)
            {
                attackRoutine = Controller.StartCoroutine(AttackRoutine());
            }
        }
    }

    public override void Exit()
    {
        Debug.Log("exit 돌입 완료");
        Machine.ChangeState(StateType.Idle);
    }

    private void Attack()
    {
        Debug.Log("attack 돌입");
        IDamagable damagable;
        damagable = cols.GetComponent<IDamagable>();
        damagable.TakeDamage(Controller.Model.Damage);
        Debug.Log("공격 성공");
    }


    Coroutine attackRoutine;
    public IEnumerator AttackRoutine()
    {
        while (cols != null)
        {
            Attack();
            yield return new WaitForSeconds(2f);
        }
        attackRoutine = null;
    }

}