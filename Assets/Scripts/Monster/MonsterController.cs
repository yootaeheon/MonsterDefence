using System.Collections;
using UnityEngine;

public class MonsterController : Monster, IDamagable
{
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
        CurHp -= damage - Defense; // ���� ������ = ������ - ����
        Debug.Log($"{damage - Defense} ���ظ� �Ծ���!");
    }

    public void Die()
    {
        Destroy(gameObject); // ���� Ǯ�� �����Ͽ� ReturnPool ����
    }

    // Ž���� �ִ� ��θ� ���� �̵��ϴ� �ڷ�ƾ
    Coroutine followPathRoutine;
    public IEnumerator FollowPath()
    {
        if (PathFinder.Path == null || PathFinder.Path.Count == 0)
            yield break;

        foreach (Vector2Int point in PathFinder.Path)
        {
            Vector3 targetPos = new Vector3(point.x, point.y, transform.position.z);

            while (Vector3.Distance(transform.position, targetPos) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, MoveSpeed * Time.deltaTime);
                yield return null;
                /* Debug.Log(targetPos);*/
            }
        }

        // ���� ����Ǯ/�ı��� ����
        Destroy(gameObject);
    }
}
