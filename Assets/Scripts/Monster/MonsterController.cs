using UnityEngine;

public class MonsterController : Monster, IDamagable
{
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
        Debug.Log($"{damage - Defense} ���ظ� �Ծ���!");
    }

    public void Die()
    {
        Destroy(gameObject); // ���� Ǯ�� �����Ͽ� ReturnPool ����
    }
}
