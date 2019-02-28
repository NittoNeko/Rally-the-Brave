using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPriResHolder
{
    event PriResChange OnPriResChange;

    float PriRes { get; set; }
}
