using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Characteristics")]
    [SerializeField]
    int stage = 1;
    [SerializeField]
    bool stageComplete = false;

    [Header("Enemy Spawning")]
    [SerializeField]
    int enemiesToSpawn = 1;

    [Header("Class References")]
    [SerializeField]
    LevelManager levelManager = null;
    [SerializeField]
    EnemyManager enemyManager = null;
    [SerializeField]
    PlayerManager playerManager = null;

    [Header("Currently Selected Enemy")]
    [SerializeField]
    Enemy selectedEnemy = null;

    #region Unity API
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!levelManager.MoveLevel()) stageComplete = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) levelManager.MakeLevel();
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            stageComplete = false;
            levelManager.ResetLevel();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) enemyManager.SpawnEnemy(enemiesToSpawn, levelManager.atUnit, true, stage, levelManager.GetNextEnemySpawnPositions(enemiesToSpawn), new Vector3(0, 90, 0));
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (selectedEnemy)
            {
                int playerDamage;
                bool playerCriticalChance = playerManager.GetPlayerCriticalHit();
                if (playerCriticalChance == true) playerDamage = playerManager.GetPlayerStats().criticalDamage;
                else playerDamage = playerManager.GetPlayerStats().attackPower;

                selectedEnemy.TakeDamage(playerDamage, playerCriticalChance);
            }
            //enemyManager.AttackAllEnemies(Random.Range(1, 3), Random.value > 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) enemyManager.DestroyAllEnemies();
        if (Input.GetKeyDown(KeyCode.W)) InitialiseLevel();
    }

    void OnEnable()
    {
        EventManager.PassOnEnemySelected += ChangeSelectedEnemy;
        EventManager.PassOnEnemyDied += AnEnemyDied;
        EventManager.PassOnUnitIncrementedSuccessful += NewUnitSuccessfullyReached;
        EventManager.PassOnAllEnemiesDied += AllEnemiesDied;
        EventManager.PassOnAtEndOfLevel += EndOfLevelReached;
    }

    void OnDisable()
    {
        EventManager.PassOnEnemySelected -= ChangeSelectedEnemy;
        EventManager.PassOnEnemyDied -= AnEnemyDied;
        EventManager.PassOnUnitIncrementedSuccessful -= NewUnitSuccessfullyReached;
        EventManager.PassOnAllEnemiesDied -= AllEnemiesDied;
        EventManager.PassOnAtEndOfLevel -= EndOfLevelReached;
    }
    #endregion

    void InitialiseLevel()
    {
        levelManager.MakeLevel();
        NewUnitSuccessfullyReached();
    }

    #region Event Calls
    // Callback to event from EventManager, which passed the e_selectedEnemy through
    void ChangeSelectedEnemy(Enemy e_selectedEnemy)
    {
        Debug.Log("GameManager:: Heard from EventManager that " + e_selectedEnemy.name + " was clicked on, setting my 'selectedEnemy'");
        if (selectedEnemy != null) selectedEnemy.Deselected();
        selectedEnemy = e_selectedEnemy;
    }

    // Callback to event from EventManager, which passed the killedEnemy through
    // Called when any enemy dies
    void AnEnemyDied(GameObject killedEnemy, int exp)
    {
        Debug.Log("GameManager:: Heard from EventManager that " + killedEnemy.name + " has died.. Giving PlayerManager " + exp);
        if (killedEnemy.GetComponent<Enemy>() == selectedEnemy) selectedEnemy = null;
        playerManager.GivePlayerEXP(exp); 
    }

    // Callback to the EventManager, if the Level's Unit was successfully moved
    void NewUnitSuccessfullyReached()
    {
        // Reached last unit? Spawn boss
        if (levelManager.atUnit == levelManager.levelUnits)
        {
            Debug.Log("GameManager:: Heard from EventManager that a Unit was successfully incremented.. Spawning Boss");
            enemyManager.SpawnEnemy(enemiesToSpawn, levelManager.atUnit, EnemyImportance.Boss, true, stage, levelManager.GetNextEnemySpawnPositions(enemiesToSpawn), new Vector3(0, 90, 0));
        }
        else
        {
            Debug.Log("GameManager:: Heard from EventManager that a Unit was successfully incremented.. Spawning next enemies");
            enemyManager.SpawnEnemy(enemiesToSpawn, levelManager.atUnit, true, stage, levelManager.GetNextEnemySpawnPositions(enemiesToSpawn), new Vector3(0, 90, 0));
        }
    }

    // Callback to the EventManager, all enemies have died
    void AllEnemiesDied()
    {
        Debug.Log("GameManager:: Heard from EventManager that all enemies are dead.. Moving Level");
        levelManager.MoveLevel();
    }

    //calledback to the EventManager, end of level has been reached
    void EndOfLevelReached()
    {
        Debug.Log("GameManager:: Heard from EventManager that end of level reached.. Setting flag");
        stageComplete = true;
        stage++;
        levelManager.ResetLevel();
        levelManager.MakeLevel();
        NewUnitSuccessfullyReached();
    }
    #endregion
}
