using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealable
{
    event CombatEvent OnHealed;

    void TakeHeal(float value);
}
