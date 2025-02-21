using System.Collections;
using UnityEngine;

public class MonsterController : Monster, IDamagable
{
    Coroutine followPathRoutine;

    private Animator _animator;

    private AnimatorOverrideController _controller;

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
            _animator.Play(AnimDefine.Dead);
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        CurHp -= damage - Defense; // 최종 데미지 = 데미지 - 방어력
        _animator.Play(AnimDefine.TakeDamage);
        Debug.Log($"{damage - Defense} 피해를 입었다!");
    }

    public void Die()
    {
        Destroy(gameObject); // 추후 풀링 적용하여 ReturnPool 적용
    }
}
