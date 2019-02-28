using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    event CombatEvent OnDamaged;

    void TakeDamage(float value);
}
