using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class MBCharacter : MonoBehaviour
{
    // triggered when an attribute changes
    public event AttrChange OnAttrChange;

    // fixed base, maximum and minimum attributes
    [SerializeField]
    private SOAttrTpl presetAttr;

    // final attributes
    private float[] attr;

    public SOAttrTpl PresetAttr => presetAttr;













    private void Awake()
    {
        attr = new float[EnumArray.AttrType.Length];
    }

    public float GetAttr(EAttrType type)
    {
        return attr[(int)type];
    }

    public void SetAttr(EAttrType type, float value)
    {
        float _max = PresetAttr.GetMax(type);
        float _min = PresetAttr.GetMin(type);
        float _previous = attr[(int)type];
        float result = Mathf.Max(Mathf.Min(value, _max), _min); // range check

        // assign result to attribute
        attr[(int)type] = result;

        // trigger attribute change event
        OnAttrChange?.Invoke(this, type, _previous, result);
    }
}
