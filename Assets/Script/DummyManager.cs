using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DummyManager : MonoBehaviour
{
    public static DummyManager instance;
    public float Damaged = 0f;
    public float NormalDamage = 2f;
    public float CounterDamage = 3f;
    public float Skill2Damage = 7f;
    public float Skill1Damage = 6f;
    public GameObject Damageinfo;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }  
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDamage();
    }
    public void UpdateDamage()
    {
        Damageinfo.GetComponent<TextMeshPro>().text = "Damage : " + Damaged.ToString();
    }
}
