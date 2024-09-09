using UnityEngine;
using DG.Tweening;
using TMPro;

public class DummyCOde : MonoBehaviour
{
    public bool isAttacking = false;
    public bool isAirboned = false;
    public bool isInvin = false;

    public float Force;
    public float PunchedForce;
    public float DamagedForce;
    public GameObject DamageText;
    public PhysicsMaterial2D PhysicsMaterial;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameObject.layer = 7;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && isAirboned && !isInvin)
        {
            isInvin = true;
            isAirboned = false;
            Invoke("InvinReset", 1f);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        Animator animator = other.gameObject.transform.parent.GetComponent<Animator>();
        if (other.gameObject.name == "AttackCol" && !isInvin)
        {
            Vector2 ConflictPos = (other.gameObject.transform.position + transform.position) / 2;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Punch") && !isAttacking)
            {
                Damaged_Push(DummyManager.instance.NormalDamage, (other.gameObject.transform.parent.transform.localScale == new Vector3(1, 1, 1)) ? Vector2.right : Vector2.left, DamagedForce, ConflictPos, false);
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Counter"))
            {
                if (!isAttacking && !isAirboned)
                {
                    Damaged_Push(DummyManager.instance.CounterDamage, (other.gameObject.transform.parent.transform.localScale == new Vector3(1, 1, 1)) ? Vector2.right : Vector2.left, DamagedForce * 1.3f, ConflictPos, false);
                }
                else if(!isAttacking && isAirboned)
                {
                    Damaged_Push(DummyManager.instance.CounterDamage * 2f, (other.gameObject.transform.parent.transform.localScale == new Vector3(1,1,1)) ? Vector2.right : Vector2.left, PunchedForce, ConflictPos, true);
                }
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Skill2") && !isAttacking)
            {
                Damaged_Push(DummyManager.instance.Skill2Damage, Vector2.up, Force, ConflictPos, true);
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Skill1Atk") && !isAttacking)
            {
                Damaged_Push(DummyManager.instance.Skill1Damage, (other.gameObject.transform.parent.transform.localScale == new Vector3(1, 1, 1)) ? Vector2.right : Vector2.left, DamagedForce * 1.5f, ConflictPos, false);
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "AttackCol" && !isInvin)
        {
            isAttacking = false;
        }
    }
    void Damaged(float Damage, float Strangth, Vector2 TextPos)
    {
        GameObject clone = Instantiate(DamageText, TextPos, Quaternion.identity);
        clone.GetComponent<TextMeshPro>().text = Damage.ToString();
        isAttacking = true;
        gameObject.layer = 8;
        gameObject.transform.DOShakePosition(0.5f, Strangth, 10, 50);
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
        DummyManager.instance.Damaged += Damage;
    }
    void Damaged_Push(float Damage, Vector3 direction, float Force , Vector2 TextPos, bool isAirbone)
    {
        GameObject clone = Instantiate(DamageText, TextPos, Quaternion.identity);
        clone.GetComponent<TextMeshPro>().text = Damage.ToString();
        isAttacking = true;
        gameObject.layer = 8;
        rb.AddForce(direction * Force, ForceMode2D.Impulse);
        isAirboned = isAirbone;
        PhysicsMaterial.bounciness = 0.4f;
        rb.sharedMaterial = PhysicsMaterial;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
        DummyManager.instance.Damaged += Damage;
    }
    void InvinReset()
    {
        PhysicsMaterial.bounciness = 0f;
        rb.sharedMaterial = PhysicsMaterial;
        isInvin = false;
    }
}
