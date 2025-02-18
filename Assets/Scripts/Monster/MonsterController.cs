public class MonsterController : Monster, IDamageable
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
        CurHp -= damage;
    }

    public void Die()
    {
        Destroy(gameObject); // ���� Ǯ�� �����Ͽ� ReturnPool ����
    }
}
