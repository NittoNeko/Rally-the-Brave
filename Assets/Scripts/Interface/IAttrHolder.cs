using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Trigger when attribute changes
public delegate void AttrChange(EAttrType type, float previous, float current);

public interface IAttrHolder
{
    event AttrChange OnAttrChange;

    float GetAttr(EAttrType type);
}
