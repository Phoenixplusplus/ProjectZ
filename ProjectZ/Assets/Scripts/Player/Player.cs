using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable<int>, IKillable
{
    // Event to be sent when player dies
    public delegate void PlayerDeath();
    public static event PlayerDeath PlayerDied;

    [Header("Player Stats")]
    public EnemyElement playerElement;
    public PlayerStats playerStats;
    public bool isDead = false;

    [Header("Class References")]
    [SerializeField]
    GameObject damageNumberUI = null;
    DamageNumberUI damageNumberComponent = null;

    #region Unity API
    // Start is called before the first frame update
    void Start()
    {
        damageNumberComponent = damageNumberUI.GetComponent<DamageNumberUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
