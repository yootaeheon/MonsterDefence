using System.Collections;
using UnityEngine;

public class StateAttack : CharacterState
{
    [SerializeField] int _layer = 1<<6;

    public StateAttack(CharacterController controller) : base(controller)
    {

    }

    public override void Init()
    {
        ThisType = StateType.Attack;
    }

    public override void Enter()
    {
        Controller.StartCoroutine(AttackRoutine());
    }

    public override void OnUpdate()
    {
        Debug.Log("Attack On Update");
    }

    public override void Exit()
    {
        Debug.Log("exit ���� �Ϸ�");
        Machine.ChangeState(StateType.Idle);
    }

    private void Attack()
    {
        Debug.Log("attack ����");
        Collider2D cols = Physics2D.OverlapBox(
            Controller.transform.position,
            Controller.Model.AttackRange,
            0,
            _layer
            );

        if ( cols == null )
            return;

        IDamagable damagable;
        if (cols.CompareTag("Monster"))
        {
            damagable = cols.GetComponent<IDamagable>();
            damagable.TakeDamage(Controller.Model.Damage);
            Debug.Log("���� ����");
        }
    }

    public IEnumerator AttackRoutine()
    {
        Attack();
        yield return null;
        Exit();
    }

}