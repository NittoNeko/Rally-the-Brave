using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttrCollection
{
    float GetAttr(EAttrType type);

    bool ExistAttr(EAttrType type);
}
