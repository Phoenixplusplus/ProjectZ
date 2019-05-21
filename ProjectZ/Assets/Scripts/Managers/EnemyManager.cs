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
            for (int i = 0; i < 3; i++)
            {
                madeEnemies.Add(enemyMaker.MakeEnemy((EnemyName)Random.Range(0, 3), true, 1, new Vector3(10, 0, 0), new Vector3(0, 90, 0)));
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            foreach (GameObject enemy in madeEnemies)
            {
                enemy.GetComponent<Enemy>().TakeDamage(5);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) DestroyEnemies();
    }
    #endregion

    void DestroyEnemies()
    {
        foreach (GameObject enemy in madeEnemies)
        {
            Destroy(enemy);
        }
        madeEnemies.Clear();
    }
}
