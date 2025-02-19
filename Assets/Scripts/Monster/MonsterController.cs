using System.Collections;
using UnityEngine;

public class MonsterController : Monster, IDamagable
{
    Coroutine followPathRoutine;

    private void Start()
    {
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
        CurHp -= damage - Defense; // 최종 데미지 = 데미지 - 방어력
        Debug.Log($"{damage - Defense} 피해를 입었다!");
    }

    public void Die()
    {
        Destroy(gameObject); // 추후 풀링 적용하여 ReturnPool 적용
    }
}
