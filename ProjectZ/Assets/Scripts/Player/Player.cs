using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable<int>, IKillable
{
    // Event to be sent when player dies
    public delegate void PlayerDeath();
    public static event PlayerDeath PlayerDied;
    // Event to be sent when player requests to apply damage
    public delegate void PlayerAttack();
    public static event PlayerAttack PlayerRequestAttack;

    [Header("Player Stats")]
    public EnemyElement playerElement;
    public PlayerStats playerStats;
    public bool isDead = false;

    [Header("Class References")]
    [SerializeField]
    GameObject damageNumberUI = null;
    DamageNumberUI damageNumberComponent = null;

    Animator animator = null;
    int timedAttackCount = 1;
    int maxTimedAttackCount = 3;

    #region Unity API
    // Start is called before the first frame update
    void Start()
    {
        damageNumberComponent = damageNumberUI.GetComponent<DamageNumberUI>();
        animator = transform.GetComponent<Animator>();

        SetIdle();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        EventManager.PassOnPlayerPrimaryAttacked += PrimaryAttack;
        EventManager.PassOnPlayerPrimaryTimedAttacked += TimedAttack;
    }

    void OnDisable()
    {
        EventManager.PassOnPlayerPrimaryAttacked -= PrimaryAttack;
        EventManager.PassOnPlayerPrimaryTimedAttacked += TimedAttack;
    }
    #endregion Unity API

    #region Interfaces
    public void TakeDamage(int damageTaken, bool critical)
    {
        playerStats.HP -= damageTaken;

        // Configure damageNumber and spawn it
        Vector3 clickedPosition = transform.position + Vector3.Normalize(Camera.main.transform.position - transform.position) * 1.3f;
        damageNumberComponent.critical = critical;
        if (damageTaken == 0) damageNumberComponent.text = "Evaded";
        else damageNumberComponent.text = damageTaken.ToString();
        damageNumberComponent.clickedPosition = clickedPosition;
        damageNumberComponent.topVector = new Vector3(-0.5f, 1f, 0);
        damageNumberComponent.bottomVector = new Vector3(-1.5f, -1f, 0);
        damageNumberComponent.randomZVariant = 1f;
        Instantiate(damageNumberUI, clickedPosition, Quaternion.identity);

        // Kill check
        if (playerStats.HP <= 0)
        {
            Killed();
            if (PlayerDied != null) PlayerDied();
        }
    }

    public void Killed()
    {
        playerStats.HP = 0;
        isDead = true;
    }

    public void Revive()
    {
        playerStats.HP = playerStats.maxHP;
        playerStats.SP = playerStats.maxSP;
        isDead = false;
    }
    #endregion

    // Animation
    void SetIdle()
    {
        animator.SetBool("Idle", true);
    }

    // Callback to event from EventManager->PrimaryAttack_UI, when the attack button is pressed without TimedAttack
    void PrimaryAttack()
    {
        animator.SetBool("UnarmedPrimaryAttack1", true);
    }

    // Callback to event from EventManager->PrimaryAttack_UI, when the attack button is pressed with TimedAttack
    void TimedAttack()
    {
        // Later, determine the maximum number of timedAttack chains that can happen for a specific weapon
        // at the moment, there will only be 2 more chains
        timedAttackCount++;
        if (timedAttackCount > 3) timedAttackCount = 1;
        animator.SetBool("UnarmedPrimaryAttack" + timedAttackCount, true);
    }

    // Animation Calls
    // Event called to tell EventManager->GameManager to apply damage
    public void PlayerApplyDamage()
    {
        if (PlayerRequestAttack != null) PlayerRequestAttack();
    }

    void PrimaryAttackFinish()
    {
        animator.SetBool("UnarmedPrimaryAttack1", false);
        animator.SetBool("UnarmedPrimaryAttack2", false);
        animator.SetBool("UnarmedPrimaryAttack3", false);
        timedAttackCount = 1;
    }

    // Annoyingly, seems that animation events can only call functions with no parameters
    void AttackOneFalse() { animator.SetBool("UnarmedPrimaryAttack1", false); }
    void AttackTwoFalse() { animator.SetBool("UnarmedPrimaryAttack2", false); }
    void AttackThreeFalse() { animator.SetBool("UnarmedPrimaryAttack3", false); }
}
