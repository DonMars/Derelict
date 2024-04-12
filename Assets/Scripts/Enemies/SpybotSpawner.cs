using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SpybotSpawner : MonoBehaviour
{
    [Header("Spawner Prefs")]
    [SerializeField] SpybotEnemySO[] spybotEnemy;
    [SerializeField] int enemiesToSpawnMin;
    [SerializeField] int enemiesToSpawnMax;
    [SerializeField] float spawnCooldown;
    [SerializeField] float spawnerTime = 0;
    [SerializeField] float spawnNextTime = 0;
    [SerializeField] int enemiesToSpawn;
    [SerializeField] int enemiesSpawned = 0;
    bool spawnSwitch = true;

    private void Start()
    {
        enemiesToSpawn = Random.Range(enemiesToSpawnMin, enemiesToSpawnMax+1);
    }

    private void Update()
    {
        spawnerTime += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if ((spawnNextTime == 0 || spawnerTime >= spawnNextTime) && enemiesSpawned < enemiesToSpawn)
            {
                SpawnEnemy();
                spawnNextTime = spawnerTime + spawnCooldown;
            }
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(RandomEnemy().enemyPrefab, transform.position, Quaternion.identity);
        enemiesSpawned += 1;
    }

    IEnumerator SpawnSpybot()
    {
        if (spawnSwitch && enemiesSpawned < enemiesToSpawn)
        {
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                GameObject enemy = Instantiate(RandomEnemy().enemyPrefab, transform.position, Quaternion.identity);
                enemiesSpawned += 1;
                spawnSwitch = false;
                yield return new WaitForSeconds(spawnCooldown);
                spawnSwitch = true;
            }
        }
    }

    SpybotEnemySO RandomEnemy()
    {
        int random = Random.Range(0, spybotEnemy.Length);
        return spybotEnemy[random];
    }
}
