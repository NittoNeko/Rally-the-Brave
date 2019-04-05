using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatusStackable
{
    void OnStackChange(int current);
}
