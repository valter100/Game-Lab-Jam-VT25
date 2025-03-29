using System.Collections;
using UnityEngine;
using System.Linq;

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
        Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        int spawnedEnemy = Random.Range(0, prefabList.Length);
        int enemiesToSpawn = baseEnemiesPerWave + Mathf.FloorToInt(Mathf.Log(waveCount + 1) * 3);
        float scaledValue = Mathf.Pow(scalingAmount, waveCount);
        Debug.Log(scaledValue);
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


            switch (damageType)
            {
                case DamageType.None:
                    break;
                case DamageType.Fire:
                    baseEnemy.GetComponent<MeshRenderer>().material.color = colorFire;
                    break;
                case DamageType.Air:
                    baseEnemy.GetComponent<MeshRenderer>().material.color = colorAir;
                    break;
                case DamageType.Water:
                    baseEnemy.GetComponent<MeshRenderer>().material.color = colorWater;
                    break;
                case DamageType.Earth:
                    baseEnemy.GetComponent<MeshRenderer>().material.color = colorEarth;
                    break;
            }
        }
        spawnInterval = spawnIntervalReset;
    }
}
