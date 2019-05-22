using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Stat Multipliers")]
    public EnemyStatMultipliers enemyStatMultipliers;

    [Header("Enemy Chances")]
    public EnemyChances enemyChances;

    [Header("Made Enemies")]
    [SerializeField]
    List<GameObject> madeEnemies = new List<GameObject>();

    [Header("Class References")]
    [SerializeField]
    EnemyMaker enemyMaker;

    #region Unity API
    // Start is called before the first frame update
    void Start()
    {
        enemyMaker.enemyStatMultipliers = enemyStatMultipliers;
        enemyMaker.enemyChances = enemyChances;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            for (int i = 0; i < 5; i++)
            {
                madeEnemies.Add(enemyMaker.MakeEnemy((EnemyName)Random.Range(0, 3), true, 1, new Vector3(10, 0, 0), new Vector3(0, 90, 0)));
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
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
                enemy.GetComponent<Enemy>().TakeDamage(5);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) DestroyAllEnemies();
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

    void DestroyAllEnemies()
    {
        foreach (GameObject enemy in madeEnemies)
        {
            Destroy(enemy);
        }
        madeEnemies.Clear();
    }

    // Callback to event from EventManager, which passed the killedEnemy through
    void DestroyEnemy(GameObject killedEnemy)
    {
        Debug.Log("EnemyManager has heard from EventManager that " + killedEnemy.name + " has died, Destorying it..");
        madeEnemies.Remove(killedEnemy);
        Destroy(killedEnemy);
    }
}
