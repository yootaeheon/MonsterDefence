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
        CurHp -= damage - Defense; // ���� ������ = ������ - ����
        _animator.Play(AnimDefine.TakeDamage);
        Debug.Log($"{damage - Defense} ���ظ� �Ծ���!");
    }

    public void Die()
    {
        Destroy(gameObject); // ���� Ǯ�� �����Ͽ� ReturnPool ����
    }
}
