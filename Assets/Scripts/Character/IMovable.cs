using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    /// <summary>
    /// Move this object.
    /// </summary>
    /// <param name="horizontal">Normalized</param>
    /// <param name="vertical">Normalized</param>
    void Move(float horizontal, float vertical);


}
