using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamagable<int>, IKillable
{
    // Event to be sent when enemy dies
    public delegate void EnemyDeath(GameObject killedEnemy, int exp);
    public static event EnemyDeath EnemyDied;
    // Event to be sent when enemy is clicked on
    public delegate void EnemyClicked(Enemy selectedEnemy);
    public static event EnemyClicked EnemySelected;

    [Header("Enemy Stats")]
    public EnemyImportance enemyImportance;
    public EnemyElement enemyElement;
    public EnemyName enemyName;
    public EnemyStats enemyStats;

    [Header("Class References")]
    [SerializeField]
    EnemyOverheadUI enemyOverheadUI = null;
    [SerializeField]
    EnemySelectUI enemySelectUI = null;
    [SerializeField]
    GameObject damageNumberUI = null;
    DamageNumberUI damageNumberComponent = null;

    Vector3 clickedPosition = Vector3.zero;

    #region Interface Functions
    public void TakeDamage(int damageTaken, bool critical)
    {
        // Reduce health
        enemyStats.HP -= damageTaken;

        // Configure HP of overheadUI
        enemyOverheadUI.SetHP(enemyStats.HP);

        // In the event that an enemy is attacked without being clicked on
        if (clickedPosition == Vector3.zero)
        {
            clickedPosition = transform.position + Vector3.Normalize(Camera.main.transform.position - transform.position) * 1.3f;
        }
        // Configure damageNumber and spawn it
        damageNumberComponent.critical = critical;
        damageNumberComponent.text = damageTaken.ToString();
        damageNumberComponent.clickedPosition = clickedPosition;
        Instantiate(damageNumberUI, clickedPosition, Quaternion.identity);

        Debug.Log("Enemy:: Took " + damageTaken + " damage, HP now " + enemyStats.HP);

        // Kill check
        if (enemyStats.HP <= 0) Killed();
    }

    public void Killed()
    {
        Debug.Log("Enemy:: " + name + " was killed");
        // Send message that this enemy has died
        if (EnemyDied != null) EnemyDied(this.gameObject, enemyStats.exp);
    }
    #endregion

    #region Unity API
    // Start is called before the first frame update
    void Start()
    {
        enemyOverheadUI.ConfigureOverheadUI(enemyStats, enemyName, enemyImportance);
        damageNumberComponent = damageNumberUI.GetComponent<DamageNumberUI>();
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
            enemySelectUI.transform.position = hit.point + (Vector3.Normalize(Camera.main.transform.position - hit.point) * 1.3f); // Bring world UI slightly closer to camera
            clickedPosition = enemySelectUI.transform.position;
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
