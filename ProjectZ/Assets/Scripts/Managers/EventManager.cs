using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // Create events to be passed on
    // Level Unit has moved
    public delegate void PassOnUnitChange();
    public static event PassOnUnitChange PassOnUnitIncremented;
    // An Enemy has died
    public delegate void PassOnEnemyDeath(GameObject killedEnemy);
    public static event PassOnEnemyDeath PassOnEnemyDied;

    #region Unity API
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Events
    void OnEnable()
    {
        LevelMover.UnitIncremented += BroadcastLevelUnitIncremented;
        Enemy.EnemyDied += BroadcastEnemyDied;
    }
    void OnDisable()
    {
        LevelMover.UnitIncremented -= BroadcastLevelUnitIncremented;
    }
    #endregion

    // Pass events on
    void BroadcastLevelUnitIncremented()
    {
        if (PassOnUnitIncremented != null) PassOnUnitIncremented();
        Debug.Log("Heared from LevelMover that it has incremented a Unit.. Broadcasting");
    }
    void BroadcastEnemyDied(GameObject killedEnemy)
    {
        if (PassOnEnemyDied != null) PassOnEnemyDied(killedEnemy);
        Debug.Log("Heared from an Enemy that " + killedEnemy.name + " has died.. Broadcasting");
    }
}
