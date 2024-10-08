using DG.Tweening;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NormalAttack : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement PM;
    private bool CanCombo = false;

    public GameObject Effect;
    public GameObject ChargingEffect;
    public GameObject EffectPos;
    public GameObject DamageFullEffect;

    public float MaxDistance;
    public float MaxDamageAdd;
    public float MaxSpeedModi;

    private GameObject Clone;

    private float Distance;
    private float DamageAdd;
    private float DamageAddPercent = 1.8f;
    private bool OnceToggle = false;
    private bool EffectOnceToggle = false;

    void Start()
    {
        Effect.SetActive(false);
        PM = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanCombo && !PlayerMovement.Skill1 && !PlayerMovement.Skill2)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                PunchAtk();
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                CounterAtk();
            }
            else if(Input.GetKeyDown(KeyCode.I))
            {
                Skill2Atk();
            }
        }
        if (!Input.anyKey)
        {
            PlayerMovement.AnimationStart = false;
        }
        if(Input.GetKey(KeyCode.U) && !PlayerMovement.Skill1 && !PlayerMovement.Skill2)
        {
            if (!OnceToggle)
            {
                Clone = Instantiate(ChargingEffect, EffectPos.transform.position, Quaternion.identity);
                Clone.transform.SetParent(this.transform);
                Skill1_Charging();
                OnceToggle = true;
            }
            EffectModi(0.7f);
            Distance += DamageAddPercent * 5 * Time.deltaTime;
            DamageAdd += DamageAddPercent * Time.deltaTime;
        }
        if(Distance >= MaxDistance)
        {
            Distance = MaxDistance;
        }
        if(DamageAdd >= MaxDamageAdd)
        {
            if (!EffectOnceToggle)
            {
                GameObject clone = Instantiate(DamageFullEffect, transform.position, Quaternion.identity);
                Destroy(clone, 0.3f);
                EffectOnceToggle = true;
            }
            DamageAdd = MaxDamageAdd;

        }
        if (Input.GetKeyUp(KeyCode.U) && CanCombo)
        {
            Destroy(Clone);
            Clone = null;
            Skill1Atk();
        }
        if(!PlayerMovement.AnimationStart && animator.GetBool("Skill1Charging"))
        {
            animator.SetBool("Skill1Charging", false);
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
        if (!animator.GetBool("Skill1Charging")) animator.SetBool("Skill1Charging", true);
        animator.SetBool("Skill1Charging", false);
        gameObject.GetComponent<Rigidbody2D>().AddForce(((transform.localScale == new Vector3(1, 1, 1)) ? Vector2.right : Vector2.left) * Distance, ForceMode2D.Impulse);
        DummyManager.instance.Skill1Damage += Mathf.Round(DamageAdd);
        OnceToggle = false;
        EffectOnceToggle = false;
        Distance = 0;
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
    public void Skill2Disable()
    {
        PlayerMovement.Skill2 = false;
    }
    public void Skill1Disable()
    {
        PlayerMovement.Skill1 = false;
        DummyManager.instance.Skill1Damage = 6f;
        DamageAdd = 0f;
    }
    void EffectModi(float Percent)
    {
        ParticleSystem PS = Clone.GetComponent<ParticleSystem>();
        var Velocity = PS.velocityOverLifetime;
        var SpeedModi = Velocity.speedModifier;
        SpeedModi.constant += Time.deltaTime * Percent;
        if(SpeedModi.constant >= MaxSpeedModi) Velocity.speedModifier = MaxSpeedModi;
        else Velocity.speedModifier = SpeedModi;
    }
}
