using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SpybotSpawner : MonoBehaviour
{
    [Header("Spawner Prefs")]
    public bool randomlyDeactivate = false;
    [Range(1, 100)] public int deactivateChanceSet;
    [SerializeField] SpybotEnemySO[] spybotEnemy;
    [SerializeField] int enemiesToSpawnMin;
    [SerializeField] int enemiesToSpawnMax;
    [SerializeField] float spawnCooldown;
    public bool usesRandomCooldown;
    public int randomCooldownMin;
    public int randomCooldownMax;
    [SerializeField] float spawnerTime = 0;
    [SerializeField] float spawnNextTime = 0;
    [SerializeField] int enemiesToSpawn;
    [SerializeField] int enemiesSpawned = 0;
    bool spawnSwitch = true;
    bool spawnerOff = false;
    public Animator lightIndicator;
    bool lightIndicatorSwitch = false;

    private void Start()
    {
        enemiesToSpawn = Random.Range(enemiesToSpawnMin, enemiesToSpawnMax+1);

        if (usesRandomCooldown)
        {
            spawnCooldown = Random.Range(randomCooldownMin, randomCooldownMax+1); 
        }

        if(randomlyDeactivate)
        {
            int chance = Random.Range(1, 101);

            if (deactivateChanceSet >= chance)
            {
                spawnerOff = true;
            }
        }
    }

    private void Update()
    {
        spawnerTime += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !spawnerOff)
        {
            if (enemiesSpawned == enemiesToSpawn)
            {
                lightIndicator.ResetTrigger("isDetected");
                lightIndicator.SetTrigger("finishedSpawning");
            }

            if (!lightIndicatorSwitch)
            {
                lightIndicator.SetTrigger("isDetected");
            }

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
