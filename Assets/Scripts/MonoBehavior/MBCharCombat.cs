using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Trigger when vitality changes
public delegate void VitalityChange(Transform target, float previous, float current, float max);

// Trigger when primary resource changes
public delegate void PriResChange(Transform target, float previous, float current, float max);

// Trigger when a constant event happens
public delegate void CombatEvent(Transform target);

//public class MBCharCombat : MonoBehaviour, IDamagable, IHealable, IThornable
//{
//    // Remove self when dead
//    private IRemovable removable;

//    public event VitalityChange OnVitalityChange;
//    public event PriResChange OnPriResChange;
//    public event CombatEvent OnDeath;
//    public event CombatEvent OnDamaged;
//    public event CombatEvent OnHealed;

//    public float Vitality
//    {
//        get => vitality;
//        set
//        {
//            float _previous = Vitality;

//            // Assign and check
//            vitality = value;
//            RangeCheck(EAttrType.MaxPriRes);

//            // Trigger Ondeath event first
//            if (vitality <= 0)
//            {
//                OnDeath(transform);
//                removable.Remove();
//                return;
//            }

//            // Trigger health change event
//            OnVitalityChange(transform, _previous, Vitality, attrHolder.GetAttr(EAttrType.MaxVitality));
//        }
//    }

//    public float PriRes
//    {
//        get => priRes;
//        set
//        {
//            float _previous = PriRes;

//            // Assign and check
//            priRes = value;
//            RangeCheck(EAttrType.MaxPriRes);

//            // Trigger mana change event
//            if (_previous != PriRes)
//            {
//                OnPriResChange(transform, _previous, PriRes, attrHolder.GetAttr(EAttrType.MaxPriRes));
//            }
//        }
//    }

//    public void TakeDamage(float value)
//    {
//        OnDamaged(transform);
//    }

//    public void TakeThorn(float value)
//    {
//        throw new System.NotImplementedException();
//    }

//    public void TakeHeal(float value)
//    {
//        throw new System.NotImplementedException();
//    }

//    private void Awake()
//    {
//        attrHolder = GetComponent<IAttrHolder>();


//    }

//    private void OnEnable()
//    {
//        attrHolder.OnAttrChange += OnRangeCheck;
//    }

//    private void OnDisable()
//    {
//        attrHolder.OnAttrChange -= OnRangeCheck;
//    }

//    private void OnRangeCheck(EAttrType type, float prevMax, float curMax)
//    {
//        if (type != EAttrType.MaxVitality && type != EAttrType.MaxPriRes) return;
//        RangeCheck(type);
//    }

//    private void RangeCheck(EAttrType type)
//    {
//        // Range check
//        if (type == EAttrType.MaxVitality)
//        {
//            vitality = Mathf.Min(Mathf.Max(vitality, 0), attrHolder.GetAttr(EAttrType.MaxVitality));
//        }
//        else if (type == EAttrType.MaxPriRes)
//        {
//            priRes = Mathf.Min(Mathf.Max(priRes, 0), attrHolder.GetAttr(EAttrType.MaxPriRes));
//        }
//    }
//}
