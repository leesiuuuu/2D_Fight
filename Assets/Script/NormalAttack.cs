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
    void Start()
    {
        PM = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("NormalAtk"))
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                PM.enabled = false;
                animator.SetBool("IsRunning", false);
                animator.SetTrigger("Punch");
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                PM.enabled = false;
                animator.SetBool("IsRunning", false);
                animator.SetTrigger("Counter");
            }
        }
        if (!Input.anyKey)
        {
            PM.enabled = true;
        }
    }
}
