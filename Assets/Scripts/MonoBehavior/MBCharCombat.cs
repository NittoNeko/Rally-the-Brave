using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBCharCombat : MonoBehaviour, IDamagable, IHealable, IThornable, IVitalityHolder, IPriResHolder
{
    // Remove self when dead
    private IRemovable removable;

    public event VitalityChange OnVitalityChange;
    public event VitalityChange OnVitalityZero;
    public event PriResChange OnPriResChange;
    public event CombatEvent OnDeath;
    public event CombatEvent OnDamaged;
    public event CombatEvent OnHealed;

    private IAttrModifiable attrModifiable;
    private IAttrCollection attrCollection;

    // Every character should have health
    private float vitality;

    // General character should depend on
    //   certain resources to cast spells
    private float priRes;

    public float Vitality
    {
        get => vitality;
        set
        {
            float previous = Vitality;
            float upper = attrCollection.GetAttr(EAttrType.MaxVitality);
            float lower = 0;
            float actual = value;

            // Range check
            if (actual > upper)
            {
                actual = upper;
            }
            else if (actual <= lower)
            {
                actual = lower;
            }

            // Assign health after all checking
            vitality = actual;

            // The ordering of events are
            //     OnVitalityZero -> OnDeath -> OnVitalityChange
            // OnDeath could prevent OnVitalityChange from happening

            // On health zero before other events
            if (Vitality <= 0)
            {
                OnVitalityZero(transform, previous, Vitality, upper);
            }

            // After triggering on health zero events
            //     expect some resurrection effect happens
            // If not, then trigger death and start to remove self
            if (vitality <= 0)
            {
                OnDeath(transform);
                removable.Remove();
                return;
            }

            // Trigger health change event
            if (previous != Vitality)
            {
                OnVitalityChange(transform, previous, Vitality, upper);
            }
        }
    }
    public float PriRes
    {
        get => priRes;
        set
        {
            float previous = PriRes;
            float upper = attrCollection.GetAttr(EAttrType.MaxPriRes);
            float lower = 0;
            float actual = value;

            // Range check
            if (actual > upper)
            {
                actual = upper;
            }
            else if (actual <= lower)
            {
                actual = lower;
            }

            // Assign Mana after all checking
            priRes = actual;

            // Trigger mana change event
            if (previous != PriRes)
            {
                OnPriResChange(transform, previous, PriRes, upper);
            }
        }
    }

    // Modify actual health
    public void ModifyHealth(float offset)
    {
        Vitality = Vitality + offset;
    }

    // Modify actual mana
    public void ModifyMana(float offset)
    {
        PriRes = PriRes + offset;
    }

    private void Awake()
    {
        attrCollection = GetComponent<IAttrCollection>();
        attrModifiable = GetComponent<IAttrModifiable>();
    }

    private void OnEnable()
    {
        attrModifiable.OnAttrChange += MaxValueCheck;
    }

    private void OnDisable()
    {
        attrModifiable.OnAttrChange -= MaxValueCheck;
    }

    private void MaxValueCheck(Transform transform, EAttrType type, float previous, float actual)
    {
        // Max health and max mana would affect actual health and mana
        if (type == EAttrType.MaxVitality)
        {
            if (Vitality >= actual)
            {
                Vitality = actual;
            }
        }
        else if (type == EAttrType.MaxPriRes)
        {
            if (PriRes >= actual)
            {
                PriRes = actual;
            }
        }
    }

    public void TakeDamage(float value)
    {
        OnDamaged(transform);
    }

    public void TakeThorn(float value)
    {
        throw new System.NotImplementedException();
    }

    public void TakeHeal(float value)
    {
        throw new System.NotImplementedException();
    }
}
