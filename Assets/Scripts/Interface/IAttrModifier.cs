using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttrModifier
{
    event AttrModChange OnAttrModChange;

    float[] GetModifiers(EAttrType type);

}
