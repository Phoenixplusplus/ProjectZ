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
    [SerializeField]
    UIManager uiManager = null;

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
        if (Input.GetKeyDown(KeyCode.Alpha3)) enemyManager.SpawnEnemy(enemiesToSpawn, levelManager.atUnit, true, stage, levelManager.GetNextEnemySpawnPositions(enemiesToSpawn), new Vector3(0, 270, 0));
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            //uiManager.UpdatePlayerHP(playerManager.GetPlayerStats());
        }
        if (Input.GetKeyDown(KeyCode.W)) InitialiseLevel();
    }

    void OnEnable()
    {
        EventManager.PassOnEnemySelected += ChangeSelectedEnemy;
        EventManager.PassOnEnemyDied += AnEnemyDied;
        EventManager.PassOnUnitIncrementedSuccessful += NewUnitSuccessfullyReached;
        EventManager.PassOnAllEnemiesDied += AllEnemiesDied;
        EventManager.PassOnAtEndOfLevel += EndOfLevelReached;
        EventManager.PassOnPlayerDied += PlayerHasDied;
        EventManager.PassOnEnemyWantsToAttack += EnemyAttackPlayer;
        EventManager.PassOnPlayerRequestAttack += PlayerAttack;
    }

    void OnDisable()
    {
        EventManager.PassOnEnemySelected -= ChangeSelectedEnemy;
        EventManager.PassOnEnemyDied -= AnEnemyDied;
        EventManager.PassOnUnitIncrementedSuccessful -= NewUnitSuccessfullyReached;
        EventManager.PassOnAllEnemiesDied -= AllEnemiesDied;
        EventManager.PassOnAtEndOfLevel -= EndOfLevelReached;
        EventManager.PassOnPlayerDied -= PlayerHasDied;
        EventManager.PassOnEnemyWantsToAttack -= EnemyAttackPlayer;
        EventManager.PassOnPlayerRequestAttack -= PlayerAttack;
    }
    #endregion

    void InitialiseLevel()
    {
        levelManager.MakeLevel();
        NewUnitSuccessfullyReached();
        uiManager.ConfigurePlayerNameHPSPEXP_UI(playerManager.GetPlayerStats());
    }

    #region Event Calls
    // Callback to event from EventManager, player has died
    void PlayerHasDied()
    {
        Debug.Log("GameManager:: Heard from EventManager that player has died");
    }

    // Callback to event from EventManager, player wants to apply damage
    void PlayerAttack()
    {
        Debug.Log("GameManager:: Heard from EventManager that player wants to apply damage now");
        if (selectedEnemy)
        {
            enemyManager.EnemyTakeDamage(selectedEnemy, playerManager.GetPlayerStats());
        }
        // Attack all enemies if one is not targeted, temporary
        else
        {
            enemyManager.AllEnemiesTakeDamage(playerManager.GetPlayerStats());
        }
    }

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
        uiManager.UpdatePlayerEXP(playerManager.GetPlayerStats());
    }

    // Callback from EventManager, an enemy wants to attack player
    void EnemyAttackPlayer(Enemy attackingEnemy)
    {
        Debug.Log("GameManager:: Heard from EventManager that " + attackingEnemy.name + " wants to attack the player");
        playerManager.PlayerTakeDamage(attackingEnemy);
        uiManager.UpdatePlayerHP(playerManager.GetPlayerStats());
    }

    // Callback to the EventManager, if the Level's Unit was successfully moved
    void NewUnitSuccessfullyReached()
    {
        // Reached last unit? Spawn boss
        if (levelManager.atUnit == levelManager.levelUnits)
        {
            Debug.Log("GameManager:: Heard from EventManager that a Unit was successfully incremented.. Spawning Boss");
            enemyManager.SpawnEnemy(enemiesToSpawn, levelManager.atUnit, EnemyImportance.Boss, true, stage, levelManager.GetNextEnemySpawnPositions(enemiesToSpawn), new Vector3(0, 270, 0));
        }
        else
        {
            Debug.Log("GameManager:: Heard from EventManager that a Unit was successfully incremented.. Spawning next enemies");
            enemyManager.SpawnEnemy(enemiesToSpawn, levelManager.atUnit, true, stage, levelManager.GetNextEnemySpawnPositions(enemiesToSpawn), new Vector3(0, 270, 0));
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

        Debug.Log("GameManager:: Restarting level at stage " + stage);
        levelManager.ResetLevel();
        levelManager.MakeLevel();
        NewUnitSuccessfullyReached();
        stageComplete = false;
    }
    #endregion
}
