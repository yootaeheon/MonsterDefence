using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] GameObject monster1;

    [SerializeField] Transform startPos;

    private void Start()
    {
        Instantiate(monster1, startPos.position, Quaternion.identity);
    }
}
