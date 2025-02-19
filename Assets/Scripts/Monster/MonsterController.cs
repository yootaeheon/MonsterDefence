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
        CurHp -= damage - Defense; // 최종 데미지 = 데미지 - 방어력
        Debug.Log($"{damage - Defense} 피해를 입었다!");
    }

    public void Die()
    {
        Destroy(gameObject); // 추후 풀링 적용하여 ReturnPool 적용
    }

    // 탐색한 최단 경로를 따라 이동하는 코루틴
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

        // 추후 리턴풀/파괴로 수정
        Destroy(gameObject);
    }
}
