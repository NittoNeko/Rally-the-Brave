using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void AttrChange(EAttrType type, float previous, float current);

public interface IAttrHolder
{
    // triggered upon attribute changes
    event AttrChange OnAttrChange;

    /// <summary>
    /// Return an attribute according to give type.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    float GetAttr(EAttrType type);
}
