using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data container for a character.
/// </summary>
public class MBCharCont : MonoBehaviour
{
    // Fixed base, maximum and minimum attributes
    [SerializeField]
    private readonly SOCharAttr baseAttr;
    [SerializeField]
    private readonly SOCharAttr maxAttr;
    [SerializeField]
    private readonly SOCharAttr minAttr;

    // Every character should have health
    private float health;

    // Resources used to cast spells
    private float mana;

    // Attributes holder in enum order
    private readonly CharAttr[] actualAttr = InitializeCharAttr();

    /// <summary>
    /// Return an intialized CharAttr[].
    /// </summary>
    /// <returns></returns>
    private static CharAttr[] InitializeCharAttr()
    {
        CharAttr[] _charAttrs = new CharAttr[EnumArray.AttrType.Length];
        for (int i = 0; i < _charAttrs.Length; ++i)
        {
            _charAttrs[i] = new CharAttr((EAttrType)i);
        }
        return _charAttrs;
    }

    private readonly byte[] specialStatus = new byte[EnumArray.SpecialStatusType.Length];

    private readonly List<CharStatus> status = new List<CharStatus>(10);

    public SOCharAttr BaseAttr => baseAttr;

    public SOCharAttr MaxAttr => maxAttr;

    public SOCharAttr MinAttr => minAttr;

    public CharAttr[] ActualAttr => actualAttr;

    public byte[] SpecialStatus => specialStatus;

    public List<CharStatus> Status => status;

    public float Health
    {
        get => health;
        set
        {
            float max = actualAttr[(int)EAttrType.MaxHealth].Value;
            health = Mathf.Max(Mathf.Min(value, max), 0); // Range check
        }
    }
    public float Mana
    {
        get => mana;
        set
        {
            float max = actualAttr[(int)EAttrType.MaxMana].Value;
            mana = Mathf.Max(Mathf.Min(value, max), 0); // Range check
        }
    }

    /// <summary>
    /// Attribute getter by Attribute Type.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public float GetAttr(EAttrType type)
    {
        return actualAttr[(int)type].Value;
    }

    /// <summary>
    /// Attribute setter that does range checking and event handling.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    public void SetAttr(EAttrType type, float value)
    {
        float _previous = GetAttr(type);
        float _upper = MaxAttr.Get(type);
        float _lower = MinAttr.Get(type);
        float _actual = Mathf.Max(Mathf.Min(value, _upper), _lower); // Range check

        ActualAttr[(int)type].Value = _actual;

        // Trigger attribute change event
        OnAttrChange(type, _previous, _actual);
    }
}
