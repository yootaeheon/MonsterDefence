using System.Collections;
using UnityEngine;

public class MonsterController : Monster, IDamagable
{
    Coroutine followPathRoutine;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _animator.Play(AnimDefine.Move);
        followPathRoutine = StartCoroutine(FollowPath());
    }

    private void Update()
    {
        if (CurHp <= 0)
        {
            Die();
        }
    }

    WaitForSeconds waitCurAnim = new(0.5f);
    IEnumerator ReturnMoveAnim()
    {
        yield return waitCurAnim;
        _animator.Play(AnimDefine.Move);
    }

    public void TakeDamage(int damage)
    {
        CurHp -= damage - Defense; // 최종 데미지 = 데미지 - 방어력
        _animator.Play(AnimDefine.TakeDamage);
        Debug.Log($"{damage - Defense} 피해를 입었다!");
        StartCoroutine(ReturnMoveAnim());
    }

    public void Die()
    {
        _animator.Play(AnimDefine.Dead);

        GameManager.Instance.MonsterCount--;
        if (GameManager.Instance.MonsterCount == 0)
        {
            GameManager.Instance.OnStageClear?.Invoke();
        }
        Destroy(gameObject);
    }
}
