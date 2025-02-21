using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Monster : MonoBehaviour
{
    #region Data - Property
    [SerializeField] string _name;
    public string Name { get { return _name; } set { Name = value; } }

    [SerializeField] float _moveSpeed;
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    [SerializeField] int _maxHp;
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }

    [SerializeField] int _curHp;
    public int CurHp { get {return _curHp; } set { _curHp = value; } }

    [SerializeField] int _defense;
    public int Defense { get { return _defense; } set { _defense = value; } }
    #endregion


    private void OnEnable()
    {
        _curHp = _maxHp;
    }

    // 탐색한 최단 경로를 따라 이동하는 코루틴
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
        GameManager.Instance.CurLife--;
    }
}
