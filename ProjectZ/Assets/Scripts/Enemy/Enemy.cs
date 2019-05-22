using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamagable<int>, IKillable
{
    // Event to be sent when enemy dies
    public delegate void EnemyDeath(GameObject killedEnemy);
    public static event EnemyDeath EnemyDied;
    // Event to be sent when enemy is clicked on
    public delegate void EnemyClicked(Enemy selectedEnemy);
    public static event EnemyClicked EnemySelected;

    public EnemyImportance enemyImportance;
    public EnemyElement enemyElement;
    public EnemyName enemyName;
    public EnemyStats enemyStats;

    [Header("Class References")]
    [SerializeField]
    EnemyOverheadUI enemyOverheadUI = null;
    [SerializeField]
    EnemySelectUI enemySelectUI = null;

    #region Interface Functions
    public void TakeDamage(int damageTaken)
    {
        enemyStats.HP -= damageTaken;
        enemyOverheadUI.SetHP(enemyStats.HP);
        Debug.Log("Enemy:: Took " + damageTaken + " damage, HP now " + enemyStats.HP);

        if (enemyStats.HP <= 0) Killed();
    }

    public void Killed()
    {
        Debug.Log("Enemy:: " + name + " was killed");
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

    void OnMouseDown()
    {
        // Send ray to get position of click
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            enemySelectUI.transform.position = hit.point + (Vector3.Normalize(Camera.main.transform.position - hit.point) * 1.3f); // Bring slightly closer to camera
        }
        // Send message that this enemy was clicked on
        if (EnemySelected != null) EnemySelected(this);
        enemySelectUI.EnemySelected(.4f);
    }
    #endregion

    public void Deselected()
    {
        enemySelectUI.EnemyDeselected();
    }
}
