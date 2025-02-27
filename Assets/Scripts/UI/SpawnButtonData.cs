using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnButtonData : MonoBehaviour
{   
    [SerializeField] bool canSpawn;
    public bool CanSpawn { get { return canSpawn; } set { canSpawn = value; } }
}
