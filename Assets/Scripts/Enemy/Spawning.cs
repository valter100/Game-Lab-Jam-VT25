using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Spawning : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float spawnInterval = 3f;
    [SerializeField] int baseEnemiesPerWave = 1;
    [SerializeField] float scalingAmount = 1;
    float spawnIntervalReset = 3f;

    [Header("Prefabs")]
    [SerializeField] GameObject[] prefabList;
    [SerializeField] GameObject player;


    [SerializeField] Color colorAir;
    [SerializeField] Color colorEarth;
    [SerializeField] Color colorFire;
    [SerializeField] Color colorWater;


    int waveCount = 0;
    public void Start()
    {
        
    }

    public void Update()
    {
        spawnInterval -= Time.deltaTime;
        if (spawnInterval < 0)
        {
            SpawnEnemies();
        }
    }

    public void SpawnEnemies()
    {
        waveCount++;
        Transform closestPoint = spawnPoints.OrderBy(spawnPoint => Vector3.Distance(player.transform.position, spawnPoint.position)).First();

        List<Transform> validPoints = spawnPoints.Where(p => p != closestPoint).ToList();
        Transform randomPoint = validPoints[Random.Range(0, validPoints.Count)];
        int spawnedEnemy = Random.Range(0, prefabList.Length);
        int enemiesToSpawn = baseEnemiesPerWave + Mathf.FloorToInt(Mathf.Log(waveCount + 1) * 3);
        float scaledValue = Mathf.Pow(scalingAmount, waveCount);
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            float angle = i * Mathf.PI * 2f / enemiesToSpawn; 
            float x = Mathf.Cos(angle) * 2;
            float z = Mathf.Sin(angle) * 2;

            Vector3 position = randomPoint.position + new Vector3(x, 0f, z);
            BaseEnemy baseEnemy = Instantiate(prefabList[spawnedEnemy], position, Quaternion.identity).GetComponent<BaseEnemy>();
            baseEnemy.SetPlayer(player);
            baseEnemy.IncreaseScaling(scaledValue);
            DamageType damageType = baseEnemy.Resistances.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;

            SkinnedMeshRenderer[] sk = baseEnemy.GetComponentsInChildren<SkinnedMeshRenderer>();
            Color color = Color.white;
            switch (damageType)
            {
                case DamageType.None:
                    break;
                case DamageType.Fire:
                   color = colorFire;
                    break;
                case DamageType.Air:
                    color = colorAir;
                    break;
                case DamageType.Water:
                    color = colorWater;
                    break;
                case DamageType.Earth:
                    color = colorEarth;
                    break;
            }
            for (int j = 0; j < sk.Length; j++)
            {
                sk[j].material.color = color;
            }

            float variation = Random.Range(0.85f, 1.15f);
            baseEnemy.transform.localScale *= variation;
            baseEnemy.UpdateMoveSpeed(variation);
            baseEnemy.UpdateDamage(variation);
            baseEnemy.UpdateHealth(variation);
            spawnInterval = spawnIntervalReset;
        }


    }

    public void GetMoreCoins(float coins)
    {
        CoinManager.Instance.PickUp(coins * waveCount);
    }
}
