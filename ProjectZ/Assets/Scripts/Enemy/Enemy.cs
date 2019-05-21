using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamagable<int>, IKillable
{
    public EnemyImportance enemyImportance;
    public EnemyElement enemyElement;
    public EnemyName enemyName;
    public EnemyStats enemyStats;

    [Header("Overhead UI")]
    [SerializeField]
    Text enemyName_UI;
    [SerializeField]
    Slider enemyHealth_UI;


    #region Interface Functions
    public void TakeDamage(int damageTaken)
    {
        enemyStats.HP -= damageTaken;
        enemyHealth_UI.value = enemyStats.HP;
        Debug.Log("Took " + damageTaken + " damage, HP now " + enemyStats.HP);

        if (enemyStats.HP <= 0) Killed();
    }

    public void Killed()
    {
        Debug.Log(name + " was killed");
    }
    #endregion

    #region Unity API
    // Start is called before the first frame update
    void Start()
    {
        ConfigureOverheadUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    void ConfigureOverheadUI()
    {
        enemyName_UI.text = "Lv." + enemyStats.level + " " + enemyName.ToString();
        enemyHealth_UI.maxValue = enemyStats.HP;
        enemyHealth_UI.value = enemyHealth_UI.maxValue;
    }
}
