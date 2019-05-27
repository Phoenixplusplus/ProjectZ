using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // Create events to be passed on
    // Level Unit has moved
    public delegate void PassOnUnitChange();
    public static event PassOnUnitChange PassOnUnitIncremented;
    // Level Unit has moved successfully
    public delegate void PassOnUnitChangeSuccessful();
    public static event PassOnUnitChangeSuccessful PassOnUnitIncrementedSuccessful;
    // Event to be sent when at the end of level
    public delegate void PassOnEndOfLevel();
    public static event PassOnEndOfLevel PassOnAtEndOfLevel;
    // An enemy wants to attack
    public delegate void PassOnEnemyAttack(Enemy attackingEnemy);
    public static event PassOnEnemyAttack PassOnEnemyWantsToAttack;
    // An Enemy has died
    public delegate void PassOnEnemyDeath(GameObject killedEnemy, int exp);
    public static event PassOnEnemyDeath PassOnEnemyDied;
    // All enemies have died
    public delegate void PassOnAllEnemiesKilled();
    public static event PassOnAllEnemiesKilled PassOnAllEnemiesDied;
    // An Enemy was clicked on
    public delegate void PassOnEnemyClicked(Enemy selectedEnemy);
    public static event PassOnEnemyClicked PassOnEnemySelected;
    // Player died
    public delegate void PassOnPlayerDeath();
    public static event PassOnPlayerDeath PassOnPlayerDied;
    // Player requests attack
    public delegate void PassOnPlayerAttack();
    public static event PassOnPlayerAttack PassOnPlayerRequestAttack;
    // Player UI Primary Attack pressed
    public delegate void PassOnPlayerPrimaryAttack();
    public static event PassOnPlayerPrimaryAttack PassOnPlayerPrimaryAttacked;
    // Player UI Primary Attack pressed when 'Timed Attacked'
    public delegate void PassOnPlayerPrimaryTimedAttack();
    public static event PassOnPlayerPrimaryTimedAttack PassOnPlayerPrimaryTimedAttacked;

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
        Enemy.EnemySelected += BroadcastEnemySelected;
        Enemy.EnemyWantsToAttack += BroadcastEnemyWantsToAttack;
        LevelManager.NewUnitSuccessful += BroadcastNewUnitSuccessful;
        LevelManager.AtEndOfLevel += BroadcastAtEndOfLevel;
        EnemyManager.AllEnemiesDied += BroadcastAllEnemiesDied;
        Player.PlayerDied += BroadcastPlayerDied;
        Player.PlayerRequestAttack += BroadcastPlayerRequestAttack;
        PrimaryAttack_UI.PlayerPrimaryAttacked += BroadcastPlayerPrimaryAttacked;
        PrimaryAttack_UI.PlayerPrimaryTimedAttacked += BroadcastPlayerPrimaryTimedAttacked;
    }
    void OnDisable()
    {
        LevelMover.UnitIncremented -= BroadcastLevelUnitIncremented;
        Enemy.EnemyDied -= BroadcastEnemyDied;
        Enemy.EnemySelected -= BroadcastEnemySelected;
        Enemy.EnemyWantsToAttack -= BroadcastEnemyWantsToAttack;
        LevelManager.NewUnitSuccessful -= BroadcastNewUnitSuccessful;
        LevelManager.AtEndOfLevel -= BroadcastAtEndOfLevel;
        EnemyManager.AllEnemiesDied -= BroadcastAllEnemiesDied;
        Player.PlayerDied -= BroadcastPlayerDied;
        Player.PlayerRequestAttack -= BroadcastPlayerRequestAttack;
        PrimaryAttack_UI.PlayerPrimaryAttacked -= BroadcastPlayerPrimaryAttacked;
        PrimaryAttack_UI.PlayerPrimaryTimedAttacked -= BroadcastPlayerPrimaryTimedAttacked;
    }
    #endregion

    // Pass events on
    void BroadcastLevelUnitIncremented()
    {
        if (PassOnUnitIncremented != null) PassOnUnitIncremented();
        Debug.Log("EventManager:: Heared from LevelMover that it has incremented a Unit.. Broadcasting");
    }

    void BroadcastEnemyDied(GameObject killedEnemy, int exp)
    {
        if (PassOnEnemyDied != null) PassOnEnemyDied(killedEnemy, exp);
        Debug.Log("EventManager:: Heared from an Enemy that " + killedEnemy.name + " has died.. Broadcasting");
    }

    void BroadcastEnemySelected(Enemy selectedEnemy)
    {
        if (PassOnEnemySelected != null) PassOnEnemySelected(selectedEnemy);
        Debug.Log("EventManager:: Heared from Enemy that it has been clicked on.. Broadcasting");
    }

    void BroadcastNewUnitSuccessful()
    {
        if (PassOnUnitIncrementedSuccessful != null) PassOnUnitIncrementedSuccessful();
        Debug.Log("EventManager:: Heared from LevelManager that it has successfully incremented a Unit.. Broadcasting");
    }

    void BroadcastAllEnemiesDied()
    {
        if (PassOnAllEnemiesDied != null) PassOnAllEnemiesDied();
        Debug.Log("EventManager:: Heared from EnemyManager that all enemies are dead.. Broadcasting");
    }

    void BroadcastAtEndOfLevel()
    {
        if (PassOnAtEndOfLevel != null) PassOnAtEndOfLevel();
        Debug.Log("EventManager:: Heared from LevelManager that end of level reached.. Broadcasting");
    }

    void BroadcastPlayerDied()
    {
        if (PassOnPlayerDied != null) PassOnPlayerDied();
        Debug.Log("EventManager:: Heared from Player that it has died.. Broadcasting");
    }

    void BroadcastEnemyWantsToAttack(Enemy attackingEnemy)
    {
        if (PassOnEnemyWantsToAttack != null) PassOnEnemyWantsToAttack(attackingEnemy);
        Debug.Log("EventManager:: Heared from " + attackingEnemy.name + " it wants to attack.. Broadcasting");
    }

    void BroadcastPlayerRequestAttack()
    {
        if (PassOnPlayerRequestAttack != null) PassOnPlayerRequestAttack();
        Debug.Log("EventManager:: Heared from Player it wants to attack.. Broadcasting");
    }

    void BroadcastPlayerPrimaryAttacked()
    {
        if (PassOnPlayerPrimaryAttacked != null) PassOnPlayerPrimaryAttacked();
        Debug.Log("EventManager:: Heared from PlayerAttack_UI, Primary Attack has been pressed.. Broadcasting");
    }

    void BroadcastPlayerPrimaryTimedAttacked()
    {
        if (PassOnPlayerPrimaryTimedAttacked != null) PassOnPlayerPrimaryTimedAttacked();
        Debug.Log("EventManager:: Heared from PlayerAttack_UI, PrimaryTimedAttack has been activated.. Broadcasting");
    }
}
