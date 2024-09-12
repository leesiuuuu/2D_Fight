using UnityEngine;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using System;

public class DummyCOde : MonoBehaviour
{
    [Header("Damage Force")]
    public float Force;
    public float PunchedForce;
    public float DamagedForce;
    [Header("Damage Obj")]
    public GameObject DamageText;
    public PhysicsMaterial2D PhysicsMaterial;

    private Animator animator_Dummy;
    private Rigidbody2D rb;
    private bool HitBoxEmptyCheck = false;
    void Start()
    {
        animator_Dummy = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gameObject.layer = 7;
    }
    private void Update()
    {
        if(!DummyManager.instance.HitFoot && !DummyManager.instance.HitBody && !DummyManager.instance.HitHead) HitBoxEmptyCheck =true;
        else HitBoxEmptyCheck =false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && DummyManager.instance.isAirboned && !DummyManager.instance.isInvin)
        {
            DummyManager.instance.isInvin = true;
            DummyManager.instance.isAirboned = false;
            Invoke("InvinReset", 1f);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "AttackCol" && !DummyManager.instance.isInvin && HitBoxEmptyCheck)
        {
            Animator animator = other.gameObject.transform.parent.GetComponent<Animator>();
            if (DummyManager.instance.HitBody)
            {
                animator_Dummy.SetTrigger("HitFront");
            }
            else if (DummyManager.instance.HitHead)
            {
                animator_Dummy.SetTrigger("HitFront");
            }
            else if (DummyManager.instance.HitFoot)
            {
                animator_Dummy.SetTrigger("HitFront");
            }
            DamageManager(other, animator);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "AttackCol" && !DummyManager.instance.isInvin)
        {
            DummyManager.instance.isAttacking = false;
        }
    }
    void Damaged(float Damage, float Strangth, Vector2 TextPos)
    {
        GameObject clone = Instantiate(DamageText, TextPos, Quaternion.identity);
        clone.GetComponent<TextMeshPro>().text = Damage.ToString();
        DummyManager.instance.isAttacking = true;
        gameObject.layer = 8;
        gameObject.transform.DOShakePosition(0.5f, Strangth, 10, 50);
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
        DummyManager.instance.Damaged += Damage;
    }
    void Damaged_Push(float Damage, Vector3 direction, float Force , Vector2 TextPos, bool isAirbone, bool Bounce = false)
    {
        GameObject clone = Instantiate(DamageText, TextPos, Quaternion.identity);
        clone.GetComponent<TextMeshPro>().text = Damage.ToString();
        DummyManager.instance.isAttacking = true;
        gameObject.layer = 8;
        rb.AddForce(direction * Force, ForceMode2D.Impulse);
        DummyManager.instance.isAirboned = isAirbone;
        if (!Bounce) BounceReset();
        else PhysicsMaterial.bounciness = 0.4f;
        rb.sharedMaterial = PhysicsMaterial;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
        DummyManager.instance.Damaged += Damage;
    }
    void InvinReset()
    {
        PhysicsMaterial.bounciness = 0f;
        rb.sharedMaterial = PhysicsMaterial;
        DummyManager.instance.isInvin = false;
    }
    void BounceReset()
    {
        PhysicsMaterial.bounciness = 0f;
    }
    void DamageManager(Collider2D other, Animator animator)
    {
        Vector2 ConflictPos = (other.gameObject.transform.position + transform.position) / 2;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Punch") && !DummyManager.instance.isAttacking)
        {
            Damaged_Push(DummyManager.instance.NormalDamage, (other.gameObject.transform.parent.transform.localScale == new Vector3(1, 1, 1)) ? Vector2.right : Vector2.left, DamagedForce, ConflictPos, false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Counter"))
        {
            if (!DummyManager.instance.isAttacking && !DummyManager.instance.isAirboned)
            {
                Damaged_Push(DummyManager.instance.CounterDamage, (other.gameObject.transform.parent.transform.localScale == new Vector3(1, 1, 1)) ? Vector2.right : Vector2.left, DamagedForce * 1.3f, ConflictPos, false);
            }
            else if (!DummyManager.instance.isAttacking && DummyManager.instance.isAirboned)
            {
                Damaged_Push(DummyManager.instance.CounterDamage * 2f, (other.gameObject.transform.parent.transform.localScale == new Vector3(1, 1, 1)) ? Vector2.right : Vector2.left, PunchedForce, ConflictPos, true);
            }
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Skill2") && !DummyManager.instance.isAttacking)
        {
            Damaged_Push(DummyManager.instance.Skill2Damage, Vector2.up, Force, ConflictPos, true, true);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Skill1Atk") && !DummyManager.instance.isAttacking)
        {
            Damaged_Push(DummyManager.instance.Skill1Damage, (other.gameObject.transform.parent.transform.localScale == new Vector3(1, 1, 1)) ? Vector2.right : Vector2.left, DamagedForce * 2.5f, ConflictPos, false);
        }
    }
}
