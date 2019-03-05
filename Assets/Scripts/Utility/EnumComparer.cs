using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TestComparer : IEqualityComparer<EAttrType>
{
    public bool Equals(EAttrType x, EAttrType y)
    {
        return x == y;
    }

    public int GetHashCode(EAttrType obj)
    {
        // you need to do some thinking here,
        return (int)obj;
    }
}