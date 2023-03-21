using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Grassy[] grassyPlaces;

    [SerializeField] private Transform monsterPrefab;
    [SerializeField] private float startSpawnDelayTimer;
    private float spawnDelayTimer;
    private Grassy currentGrassy;
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
            currentGrassy = grassy;
            if (canSpawn)
            {
                spawnDelayTimer -= Time.deltaTime;

                if (spawnDelayTimer <= 0)
                {
                    Instantiate(monsterPrefab, grassy.transform.position, Quaternion.identity);
                    spawnDelayTimer = startSpawnDelayTimer;
                }
            }
        }
    }




}
