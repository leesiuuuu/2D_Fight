using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Animations;

public class NormalAttack : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement PM;
    private bool CanCombo = false;
    void Start()
    {
        PM = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.J) && !CanCombo)
        {
            PunchAtk();
        }
        else if (Input.GetKeyDown(KeyCode.K) && !CanCombo)
        {
            CounterAtk();
        }
        else if(Input.GetKeyDown(KeyCode.I) && !CanCombo)
        {
            Skill2Atk();
        }
        else if (!Input.anyKey)
        {
            PM.enabled = true;
        }

    }
    void PunchAtk()
    {
        PM.enabled = false;
        animator.SetBool("IsRunning", false);
        animator.SetTrigger("Punch");
    }
    void CounterAtk()
    {
        PM.enabled = false;
        animator.SetBool("IsRunning", false);
        animator.SetTrigger("Counter");
    }
    void Skill2Atk()
    {
        PM.enabled = false;
        animator.SetBool("IsRunning", false);
        animator.SetTrigger("Skill2");
    }
    public void ComboAble()
    {
        Debug.Log("Event Called!");
        CanCombo = true;
    }
    public void CantAbleCombo()
    {
        Debug.Log("Event Called?!");
        CanCombo = false;
    }
}
