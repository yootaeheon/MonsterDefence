using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] GameObject _monster1;

    [HideInInspector] Transform _startPos;

    private void Start()
    {
        _startPos = GameObject.FindWithTag("StartPos").transform;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_monster1, _startPos.position, Quaternion.identity);
        }
    }
}
