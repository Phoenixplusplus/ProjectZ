using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryAttack_UI : MonoBehaviour
{
    // Event to be sent when player presses PrimaryAttack Button
    public delegate void PlayerPrimaryAttack();
    public static event PlayerPrimaryAttack PlayerPrimaryAttacked;
    // Event to be sent when player presses PrimaryAttack Button with a 'Timed Attack'
    public delegate void PlayerPrimaryTimedAttack();
    public static event PlayerPrimaryTimedAttack PlayerPrimaryTimedAttacked;

    Animator animator = null;
    public bool timedAttack = false;
    public int timedAttackCount = 0;
    public int timesPressed = 0;
    public float animationMultiplier = 0;

    #region Unity API
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    public void PrimaryAttackClicked()
    {
        animator.speed = 1 + animationMultiplier;
        if (animator.GetBool("PrimaryAttack") != true)
        {
            if (PlayerPrimaryAttacked != null) PlayerPrimaryAttacked();
            animator.SetBool("PrimaryAttack", true);
            Debug.Log("PrimaryAttack_UI:: PrimaryAttack Activated");
        }
        else
        {
            if (!timedAttack) timesPressed++;
            if (timedAttack && timesPressed == 0)
            {
                timedAttackCount++;
                if (PlayerPrimaryTimedAttacked != null) PlayerPrimaryTimedAttacked();
                animator.Play("PrimaryAttack", 0, 0);
                Debug.Log("PrimaryAttack_UI:: TimedAttack Activated");
            }
        }
    }

    public void PrimaryAttackFinished()
    {
        animator.SetBool("PrimaryAttack", false);
        timedAttackCount = 0;
        timesPressed = 0;
        TimedAttackFinished();
    }

    public void TimedAttackStart() { timedAttack = true; }
    public void TimedAttackFinished() { timedAttack = false; }
}
