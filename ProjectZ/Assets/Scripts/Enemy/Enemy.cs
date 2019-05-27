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
    // Event to be sent when enemy wants to attack
    public delegate void EnemyAttack(Enemy attackingEnemy);
    public static event EnemyAttack EnemyWantsToAttack;

    [Header("Enemy Stats")]
    public EnemyImportance enemyImportance;
    public EnemyElement enemyElement;
    public EnemyName enemyName;
    public EnemyStats enemyStats;

    // Animations chiefly control the flow of attacks and delays
    [Header("Animation & Attack")]
    [SerializeField]
    float minAttackDelay = 1.5f;
    [SerializeField]
    float maxAttackDelay = 3f;
    [SerializeField]
    float secondComboChance = 35f;
    [SerializeField]
    float thirdComboChance = 35f;
    float startMinAttackDelay = 1f;
    float startMaxAttackDelay = 2f;
    Animator animator = null;

    [Header("Class References")]
    [SerializeField]
    EnemyOverheadUI enemyOverheadUI = null;
    [SerializeField]
    EnemySelectUI enemySelectUI = null;
    [SerializeField]
    GameObject damageNumberUI = null;
    DamageNumberUI damageNumberComponent = null;

    // Used to position the damageNumberUI Worldspace
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
            clickedPosition = transform.position + ((Vector3.Normalize(Camera.main.transform.position - transform.position) / 2f));
        }
        // Configure damageNumber and spawn it
        damageNumberComponent.critical = critical;
        if (damageTaken == 0) damageNumberComponent.text = "Evaded";
        else damageNumberComponent.text = damageTaken.ToString();
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
        if (EnemyDied != null) EnemyDied(this.transform.parent.gameObject, enemyStats.exp);
    }
    #endregion

    #region Unity API
    // Start is called before the first frame update
    void Start()
    {
        enemyOverheadUI.ConfigureOverheadUI(enemyStats, enemyName, enemyImportance);
        damageNumberComponent = damageNumberUI.GetComponent<DamageNumberUI>();
        animator = GetComponent<Animator>();
        animator.speed = 1 + Mathf.Clamp01(enemyStats.agility / 600);

        AttackDelay();
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
            enemySelectUI.transform.position = hit.point + ((Vector3.Normalize(Camera.main.transform.position - hit.point) / 2f)); // Bring world UI slightly closer to camera
            clickedPosition = enemySelectUI.transform.position;
        }
        // Send message that this enemy was clicked on
        if (EnemySelected != null) EnemySelected(this);
        enemySelectUI.EnemySelected(.4f);
    }
    #endregion

    // Called by GameManager when another enemy other than this one has been clicked, and this enemy was the previous clicked enemy
    public void Deselected()
    {
        clickedPosition = Vector3.zero;
        enemySelectUI.EnemyDeselected();
    }

    // Primary event call by animations when they want to attack the player
    public void Attack()
    {
        if (EnemyWantsToAttack != null) EnemyWantsToAttack(this);
    }

    // Animation and Attacking Coroutines with their corresponding function calls
    void AttackDelay() { StartCoroutine(AttackDelay_IE()); }
    IEnumerator AttackDelay_IE()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Attack3", false);
        yield return new WaitForSeconds(Random.Range(minAttackDelay, maxAttackDelay));
        animator.SetBool("Attack", true);
        yield break;
    }

    void ComboOneFinish()
    {
        if (Random.Range(0, 100) <= secondComboChance) animator.SetBool("Attack2", true);
        else AttackDelay();
    }

    void ComboTwoFinish()
    {
        if (Random.Range(0, 100) <= thirdComboChance) animator.SetBool("Attack3", true);
        else AttackDelay();
    }
}
