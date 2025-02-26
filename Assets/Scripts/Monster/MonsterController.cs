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

    public void TakeDamage(int damage)
    {
        CurHp -= damage - Defense; // ���� ������ = ������ - ����
        _animator.Play(AnimDefine.TakeDamage);
        Debug.Log($"{damage - Defense} ���ظ� �Ծ���!");
    }

    public void Die()
    {
        _animator.Play(AnimDefine.Dead);
        GameManager.Instance.MonsterCount--;

        if (GameManager.Instance.MonsterCount == 0)
        {
            GameManager.Instance.OnStageClear?.Invoke();
        }

        Destroy(gameObject); // ���� Ǯ�� �����Ͽ� ReturnPool ����
    }
}
