using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] GameObject _monster1;
    [SerializeField] GameObject _monster2;
    [SerializeField] GameObject _monster3;
    [SerializeField] GameObject _monster4;

    [HideInInspector] Transform _startPos;
    private int TotalSpawnNum;

    GameObject[] Monsters = new GameObject[4];

    public UnityEvent ClearStage;

    private void Awake()
    {
        Monsters[0] = _monster1;
        Monsters[1] = _monster2;
        Monsters[2] = _monster3;
        Monsters[3] = _monster4;
    }

    private void Start()
    {
        _startPos = GameObject.FindWithTag("StartPos").transform;
     
        TotalSpawnNum = GameManager.Instance.MonsterCount ;

        spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    WaitForSeconds monsterShortSpawnDelay = new(1.5f);
    WaitForSeconds monsterLongSpawnDelay = new(4f);
    Coroutine spawnRoutine;
    IEnumerator SpawnRoutine()
    {
        yield return monsterShortSpawnDelay;

        while (TotalSpawnNum >= 0)
        {
            for (int i = 0; i < 3; i++)
            {
                if (TotalSpawnNum == 0)
                {
                    yield break;
                }

                Debug.Log(TotalSpawnNum);
                Instantiate(Monsters[Random.Range(0, Monsters.Length)], _startPos.position, Quaternion.identity);
                TotalSpawnNum--;
                yield return monsterShortSpawnDelay;
            }
            yield return monsterLongSpawnDelay;
        }
    }
}
