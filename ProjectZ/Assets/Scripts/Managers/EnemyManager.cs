using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Events to be sent when all enemies have died
    public delegate void AllEnemiesKilled();
    public static event AllEnemiesKilled AllEnemiesDied;

    [Header("Stat Multipliers")]
    public EnemyStatMultipliers enemyStatMultipliers;

    [Header("Enemy Chances")]
    public EnemyChances enemyChances;

    [Header("Made Enemies")]
    public List<GameObject> madeEnemies = new List<GameObject>();

    [Header("Class References")]
    [SerializeField]
    EnemyMaker enemyMaker = null;

    EnemyConfigurer enemyConfigurer = null;

    #region Unity API
    // Start is called before the first frame update
    void Start()
    {
        enemyConfigurer = GetComponent<EnemyConfigurer>();
        enemyMaker.enemyStatMultipliers = enemyStatMultipliers;
        enemyMaker.enemyChances = enemyChances;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Events
    void OnEnable()
    {
        EventManager.PassOnEnemyDied += DestroyEnemy;
    }
    void OnDisable()
    {
        EventManager.PassOnEnemyDied -= DestroyEnemy;
    }
    #endregion

    // GameManager calls
    #region GameManager Calls

    public void EnemyTakeDamage(Enemy selectedEnemy, PlayerStats playerStats)
    {
        enemyConfigurer.ConfigureTakeDamage(selectedEnemy, playerStats);
    }

    public void AllEnemiesTakeDamage(PlayerStats playerStats)
    {
        // If ever needing to attack all enemies available..
        // While iterating over a list, it is possible an earlier element has been removed, and will cause problems.
        // Create a temperary list and iterate through that, which may subsequently cause their kill event to happen
        // and be removed from the original list
        List<GameObject> tempList = new List<GameObject>();
        for (int i = 0; i < madeEnemies.Count; i++)
        {
            tempList.Add(madeEnemies[i]);
        }
        foreach (GameObject enemy in tempList)
        {
            EnemyTakeDamage(enemy.GetComponentInChildren<Enemy>(), playerStats);
        }
    }

    // Enemy Spawning
    // Random enemy importance
    public void SpawnEnemy(int amount, int atUnit, EnemyName enemyName, bool randomRareImportance, int stage, Vector3[] positions, Vector3 rotation)
    {
        for (int i = 0; i < amount; i++)
        {
            madeEnemies.Add(enemyMaker.MakeEnemy(enemyName, randomRareImportance, stage, positions[i], rotation));
        }
    }

    // Fixed enemy importance
    public void SpawnEnemy(int amount, int atUnit, EnemyName enemyName, EnemyImportance enemyImportance, bool randomRareImportance, int stage, Vector3[] positions, Vector3 rotation)
    {
        for (int i = 0; i < amount; i++)
        {
            madeEnemies.Add(enemyMaker.MakeEnemy(enemyName, enemyImportance, randomRareImportance, stage, positions[i], rotation));
        }
    }

    // Random enemy with fixed importance
    public void SpawnEnemy(int amount, int atUnit, EnemyImportance enemyImportance, bool randomRareImportance, int stage, Vector3[] positions, Vector3 rotation)
    {
        for (int i = 0; i < amount; i++)
        {
            madeEnemies.Add(enemyMaker.MakeEnemy(enemyMaker.GetRandomEnemyName(), enemyImportance, randomRareImportance, stage, positions[i], rotation));
        }
    }

    // Random enemy with random importance
    public void SpawnEnemy(int amount, int atUnit, bool randomRareImportance, int stage, Vector3[] positions, Vector3 rotation)
    {
        for (int i = 0; i < amount; i++)
        {
            madeEnemies.Add(enemyMaker.MakeEnemy(enemyMaker.GetRandomEnemyName(), randomRareImportance, stage, positions[i], rotation));
        }
    }

    public void DestroyAllEnemies()
    {
        foreach (GameObject enemy in madeEnemies)
        {
            Destroy(enemy);
        }
        madeEnemies.Clear();
    }

    // Callback to event from EventManager, which passed the killedEnemy through
    public void DestroyEnemy(GameObject killedEnemy, int exp)
    {
        Debug.Log("EnemyManager:: Heard from EventManager that " + killedEnemy.name + " has died, Destorying it..");
        madeEnemies.Remove(killedEnemy);
        Destroy(killedEnemy);

        // Pass on an event if all enemies have been killed
        if (madeEnemies.Count == 0)
        {
            if (AllEnemiesDied != null) AllEnemiesDied();
        }
    }
    #endregion
}
