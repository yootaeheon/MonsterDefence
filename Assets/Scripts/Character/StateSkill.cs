using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateSkill : CharacterState, IAtivateSkill
{
    private Collider2D cols;

    public StateSkill(CharacterController controller) : base(controller) { }

    public override void Init()
    {
        stateType = StateType.Skill;
    }

    public override void Enter()
    {
        Controller.StartCoroutine(SkillRoutine());
    }

    public override void OnUpdate()
    {
       
    }

    public override void Exit()
    {
        Machine.ChangeState(StateType.Attack);
        Debug.Log("Skill -> Attack");
    }

    private void ActivateSkill()
    {
        Debug.Log("Skill 사용");
        // Play("adapter.SkillAnim")

        cols = Physics2D.OverlapBox(Controller.transform.position, Controller.Model.AttackRange, 0, Controller.MonsterLayer);

        IDamagable damagable;
        damagable = cols.GetComponent<IDamagable>();
        damagable.TakeDamage(Controller.Model.SkillDamage);
        Controller.Model.CurMana -= Controller.Model.Cost;
        Debug.Log("Skill 사용 완료");
    }

    WaitForSeconds delay = new(1f);
    IEnumerator SkillRoutine()
    {
        yield return delay;
        ActivateSkill();
        yield return null;
        Exit();
    }
}
