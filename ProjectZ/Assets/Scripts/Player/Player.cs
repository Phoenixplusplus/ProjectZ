using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable<int>, IKillable
{
    [Header("Player Stats")]
    public EnemyElement playerElement;
    public PlayerStats playerStats;

    #region Unity API
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion Unity API

    #region Interfaces
    public void TakeDamage(int damageTaken, bool critical)
    {

    }

    public void Killed()
    {

    }
    #endregion
}
