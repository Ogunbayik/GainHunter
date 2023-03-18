using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private float monsterPatrolDistance;
    [SerializeField] private LayerMask monsterLayer;
    [SerializeField] private bool canSpawn;
    
    private Vector3 boxSize;
    void Start()
    {
        boxSize = new Vector3(monsterPatrolDistance, 0f, monsterPatrolDistance);
    }

    void Update()
    {
        var checkBox = Physics.CheckBox(transform.position, boxSize, Quaternion.identity, monsterLayer);

        if (checkBox)
        {
            canSpawn = false;
        }
        else
        {
            canSpawn = true;
        }
    }

    public bool CanSpawn()
    {
        return canSpawn;
    }

}
