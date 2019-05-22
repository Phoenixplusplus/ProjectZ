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
                selectedEnemy.TakeDamage(5);
            }
            //enemyManager.AttackAllEnemies();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) enemyManager.DestroyAllEnemies();
    }

    void OnEnable()
    {
        EventManager.PassOnEnemySelected += ChangeSelectedEnemy;
        EventManager.PassOnEnemyDied += CheckSeletecEnemyDied;
    }

    void OnDisable()
    {
        EventManager.PassOnEnemySelected -= ChangeSelectedEnemy;
        EventManager.PassOnEnemyDied -= CheckSeletecEnemyDied;
    }
    #endregion

    // Callback to event from EventManager, which passed the e_selectedEnemy through
    void ChangeSelectedEnemy(Enemy e_selectedEnemy)
    {
        Debug.Log("GameManager:: Heard from EventManager that " + e_selectedEnemy.name + " was clicked on, setting my 'selectedEnemy'");
        if (selectedEnemy != null) selectedEnemy.Deselected();
        selectedEnemy = e_selectedEnemy;
    }

    // Callback to event from EventManager, which passed the killedEnemy through
    void CheckSeletecEnemyDied(GameObject killedEnemy)
    {
        if (killedEnemy.GetComponent<Enemy>() == selectedEnemy) selectedEnemy = null;
    }
}
