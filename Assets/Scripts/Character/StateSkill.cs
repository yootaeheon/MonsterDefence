using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

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
        Controller.Model.CurMana = 0;
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

    public void ActivateSkill()
    {
        Debug.Log("Skill 사용");
        // Play("adapter.SkillAnim")

        switch (Controller.Model.SkillType)
        {
            case CharacterModel.Type_Skill.PowerUp:
                PowerUp();
                break;

            case CharacterModel.Type_Skill.SpeedUp:
                Controller.StartCoroutine(SpeedUp());
                break;
        }
    }

    WaitForSeconds delay = new(1f);
    IEnumerator SkillRoutine()
    {
        yield return delay;
        ActivateSkill();
        yield return null;
        Exit();
    }

    public void PowerUp()
    {
        cols = Physics2D.OverlapBox(Controller.transform.position, Controller.Model.AttackRange, 0, Controller.MonsterLayer);

        IDamagable damagable;
        damagable = cols.GetComponent<IDamagable>();
        damagable.TakeDamage(Controller.Model.SkillDamage);
        Controller.Model.CurMana -= Controller.Model.Cost;
        Debug.Log("스킬 사용 완료");
    }

    WaitForSeconds duration = new(4f);
    IEnumerator SpeedUp()
    {
        int AttackDelay = Controller.Model.AttackDelay;

        Controller.Model.AttackDelay = Controller.Model.AttackDelay - 1;
        yield return duration;
        Controller.Model.AttackDelay = AttackDelay;
    }
}
