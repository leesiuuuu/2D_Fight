using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DummyCOde : MonoBehaviour
{
    public float Damaged1 = 0f;
    void Start()
    {
        gameObject.layer = 7;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Animator animator = other.gameObject.transform.parent.GetComponent<Animator>();
        if (other.gameObject.name == "AttackCol")
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
            {
                Damaged(2f, 0.1f);
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Counter"))
            {
                Damaged(3f, 0.3f);
            }

        }
    }
    void Damaged(float Damage, float Strangth)
    {
        gameObject.layer = 8;
        gameObject.transform.DOShakePosition(0.5f, Strangth, 10, 50);
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
        Damaged1 += Damage;
        Debug.Log("Damaged! : " + Damaged1);
    }
}
