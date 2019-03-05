using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatusTaker
{
    void TakeStatus(SOCharStatus content, byte stack, float timePercent);
}
