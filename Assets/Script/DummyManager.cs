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
    [Header("Hitted Position")]
    public bool HitHead = false;
    public bool HitBody = false;
    public bool HitFoot = false;
    [Header("INFO")]
    public TMP_Text Damageinfo;
    public TMP_Text Stateinfo;
    public TMP_Text Hitinfo;
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
        UpdateHit();
        UpdateState();
    }
    public void UpdateDamage()
    {
        Damageinfo.text = "Damage : " + Damaged.ToString();
    }
    public void UpdateHit()
    {
        Hitinfo.text = "HitHead : " + HitHead + "\n"
                    + "HitBody : " + HitBody + "\n"
                    + "HitFoot : " + HitFoot;
    }
    public void UpdateState()
    {
        Stateinfo.text = "State : " + Damaged.ToString();
    }
}
