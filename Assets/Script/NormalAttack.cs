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
    public GameObject AtkCol;
    void Start()
    {
        PM = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        AtkCol.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!animator.GetCurrentAnimatorStateInfo(0).IsTag("NormalAtk"))
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                PunchAtk();
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                CounterAtk();
            }
            if (!Input.anyKey)
            {
                AtkCol.SetActive(false);
                PM.enabled = true;
            }
        }
    }
    void PunchAtk()
    {
        PM.enabled = false;
        AtkCol.SetActive(true);
        animator.SetBool("IsRunning", false);
        animator.SetTrigger("Punch");
    }
    void CounterAtk()
    {
        PM.enabled = false;
        AtkCol.SetActive(true);
        animator.SetBool("IsRunning", false);
        animator.SetTrigger("Counter");
    }
}
