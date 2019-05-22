using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamagable<int>, IKillable
{
    // Create event for enemy death
    public delegate void EnemyDeath(GameObject killedEnemy);
    public static event EnemyDeath EnemyDied;

    public EnemyImportance enemyImportance;
    public EnemyElement enemyElement;
    public EnemyName enemyName;
    public EnemyStats enemyStats;

    [Header("Class References")]
    [SerializeField]
    EnemyOverheadUI enemyOverheadUI;

    #region Interface Functions
    public void TakeDamage(int damageTaken)
    {
        enemyStats.HP -= damageTaken;
        enemyOverheadUI.SetHP(enemyStats.HP);
        Debug.Log("Took " + damageTaken + " damage, HP now " + enemyStats.HP);

        if (enemyStats.HP <= 0) Killed();
    }

    public void Killed()
    {
        Debug.Log(name + " was killed");
        // Send message that this enemy has died
        if (EnemyDied != null) EnemyDied(this.gameObject);
    }
    #endregion

    #region Unity API
    // Start is called before the first frame update
    void Start()
    {
        enemyOverheadUI.ConfigureOverheadUI(enemyStats, enemyName, enemyImportance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
}
