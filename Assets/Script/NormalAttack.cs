using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class NormalAttack : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement PM;
    private bool CanCombo = false;
    public GameObject Effect;

    public float MaxDistance;
    public float MaxDamageAdd;

    private float Distance;
    private float DamageAdd;
    private float DamageAddPercent = 1.2f;
    private bool OnceToggle = false;

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
            PlayerMovement.AnimationStart = false;
        }
        if(Input.GetKey(KeyCode.U) && !PlayerMovement.Skill1)
        {
            if (!OnceToggle)
            {
                Skill1_Charging();
                OnceToggle = true;
            }
            Distance += DamageAddPercent * Time.deltaTime;
            DamageAdd += DamageAddPercent * Time.deltaTime;
        }
        if(Distance >= MaxDistance)
        {
            Distance = MaxDistance;
        }
        if(DamageAdd >= MaxDamageAdd)
        {
            DamageAdd = MaxDamageAdd;
        }
        if (Input.GetKeyUp(KeyCode.U))
        {
            Skill1Atk();
        }

    }

    void PunchAtk()
    {
        PlayerMovement.AnimationStart = true;
        animator.SetBool("IsRunning", false);
        animator.SetTrigger("Punch");
    }

    void CounterAtk()
    {
        PlayerMovement.AnimationStart = true;
        animator.SetBool("IsRunning", false);
        animator.SetTrigger("Counter");
    }
    void Skill2Atk()
    {
        PlayerMovement.AnimationStart = true;
        PlayerMovement.Skill2 = true;
        animator.SetBool("IsRunning", false);
        animator.SetTrigger("Skill2");
    }
    void Skill1_Charging()
    {
        PlayerMovement.AnimationStart = true;
        animator.SetBool("IsRunning", false);
        animator.SetTrigger("Skill1");
        animator.SetBool("Skill1Charging", true);
    }
    void Skill1Atk()
    {
        PlayerMovement.AnimationStart = true;
        animator.SetBool("Skill1Charging", false);
        OnceToggle = false;
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
    public void SkillDisable()
    {
        PlayerMovement.Skill2 = false;
    }
}
