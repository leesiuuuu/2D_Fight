using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class DummyCOde : MonoBehaviour
{
    public bool isAttacking = false;
    public GameObject DamageText;
    void Start()
    {
        gameObject.layer = 7;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Animator animator = other.gameObject.transform.parent.GetComponent<Animator>();
        if (other.gameObject.name == "AttackCol")
        {
            Vector2 ConflictPos = (other.gameObject.transform.position + transform.position) / 2;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Punch") && !isAttacking)
            {
                Damaged(DummyManager.instance.NormalDamage, 0.1f, ConflictPos);
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Counter") && !isAttacking)
            {
                Damaged(DummyManager.instance.CounterDamage, 0.3f, ConflictPos);
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "AttackCol")
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
}
