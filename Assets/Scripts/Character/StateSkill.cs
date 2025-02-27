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
        Controller.StartCoroutine(SkillRoutine());
    }

    public override void OnUpdate()
    {
        if (cols == null)
        {
            Exit();
        }
    }

    public override void Exit()
    {
        Machine.ChangeState(StateType.Attack);
        Debug.Log("Skill -> Attack");
    }

    WaitForSeconds delay = new(3f);
    private int delay2 => Controller.Model.AttackDelay;
    IEnumerator SkillRoutine()
    {
        yield return delay;
        ActivateSkill();
        yield return delay;
        Exit();
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

    public void PowerUp()
    {
        cols = Physics2D.OverlapBox(Controller.transform.position, Controller.Model.AttackRange, 0, Controller.MonsterLayer);

        if (cols == null)
            return;

        Controller.Animator.Play(AnimDefine.CharSkill);
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
