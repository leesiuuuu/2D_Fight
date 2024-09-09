using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class NormalAttack : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement PM;
    private bool CanCombo = false;
    public GameObject Effect;
    void Start()
    {
        Effect.SetActive(false);
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
            PlayerMovement.isSkilled = false;
        }

    }

    void PunchAtk()
    {
        PlayerMovement.isSkilled = true;
        animator.SetBool("IsRunning", false);
        animator.SetTrigger("Punch");
    }

    void CounterAtk()
    {
        PlayerMovement.isSkilled = true;
        animator.SetBool("IsRunning", false);
        animator.SetTrigger("Counter");
    }
    void Skill2Atk()
    {
        PlayerMovement.isSkilled = true;
        animator.SetBool("IsRunning", false);
        animator.SetTrigger("Skill2");
    }
    public void ComboAble()
    {
        CanCombo = true;
    }
    public void CantAbleCombo()
    {
        CanCombo = false;
    }
    public void EffectEnable()
    {
        Effect.SetActive(true);
    }
    public void EffectDisable()
    {
        Effect.SetActive(false);
    }
}
