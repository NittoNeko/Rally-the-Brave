using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillCollisionCombatInfo
{
    void SetCombatInfo(ECombaterFaction casterFaction, IAttrHolder casterAttr);
}
