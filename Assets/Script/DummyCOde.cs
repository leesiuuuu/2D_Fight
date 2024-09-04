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
        if(other.gameObject.name == "AttackCol")
        {
            Damaged(2f);
        }
    }
    void Damaged(float Damage)
    {
        gameObject.layer = 8;
        gameObject.transform.DOShakePosition(0.5f, 0.1f, 10, 50);
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
        Damaged1 += Damage;
        Debug.Log("Damaged! : " + Damaged1);
    }
}
