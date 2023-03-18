using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Grassy[] grassyPlaces;

    [SerializeField] private Transform monsterPrefab;
    [SerializeField] private float startSpawnDelayTimer;
    private float spawnDelayTimer;
    private void Awake()
    {
        grassyPlaces = FindObjectsOfType<Grassy>();
    }

    private void Start()
    {
        SetupMonster();
    }
    private void SetupMonster()
    {
        foreach (var grassy in grassyPlaces)
        {
            Instantiate(monsterPrefab, grassy.transform.position, Quaternion.identity);
        }
        spawnDelayTimer = startSpawnDelayTimer;
    }

    void Update()
    {
        SpawnNewMonster();
    }
    
    private void SpawnNewMonster()
    {
        foreach (var grassy in grassyPlaces)
        {
            var canSpawn = grassy.GetComponentInChildren<SpawnPoint>().CanSpawn();

            if (canSpawn)
            {
                spawnDelayTimer -= Time.deltaTime;

                if (spawnDelayTimer <= 0)
                {
                    Debug.Log($"{grassy.name} place can spawn in there");
                    Instantiate(monsterPrefab, grassy.transform.position, Quaternion.identity);
                    spawnDelayTimer = startSpawnDelayTimer;
                }
            }
        }
    }
    





}
