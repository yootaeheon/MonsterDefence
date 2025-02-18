using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : Monster
{
    private void Start()
    {
        StartCoroutine(FollowPath());
    }
}
