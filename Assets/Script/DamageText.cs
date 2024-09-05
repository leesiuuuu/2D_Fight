using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using DG.Tweening;

public class DamageText : MonoBehaviour
{
    private TextMeshPro text;
    private Sequence Sequence;
    private void Start()
    {
        text = GetComponent<TextMeshPro>();
        Sequence.Append(text.transform.DOMoveY(text.transform.position.y + 0.8f, 0.5f));
        Sequence.Append(text.DOColor(new Color(255, 255, 255, 0), 0.5f).OnComplete(()=>
        {
            Destroy(gameObject, 0.2f);
        }));
    }
}
