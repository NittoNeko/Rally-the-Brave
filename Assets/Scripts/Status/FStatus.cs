using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FStatus
{
    private static IStatusUpdatable[] emptyUpdatable = new IStatusUpdatable[0];
    private static IStatusStackable[] emptyStackable = new IStatusStackable[0];
    private static List<IStatusUpdatable> updatables = new List<IStatusUpdatable>();
    private static List<IStatusStackable> stackables = new List<IStatusStackable>();

    /// <summary>
    /// Initialize a status based on StatusTpl, PeriodCombatResouceEffect and AttrModifierEffect.
    /// Then bind the status to the taker.
    /// </summary>
    /// <param name="_source"></param>
    /// <param name="stack"></param>
    /// <param name="timePercent"></param>
    /// <returns></returns>
    public static IStatus CreateAndBind(SOStatusTpl template,
        IAttrModifierTaker attrModifierTaker,
        ICombater combatTaker,
        IAttrHolder applierAttr,
        int stack = 1, float timePercent = 1)
    {

        // retrieve from scriptable object
        StatusTpl _source = template.StatusTpl;

        // clear lists
        updatables.Clear();
        stackables.Clear();

        // initialize combat resource effect processor
        if (combatTaker != null && _source.CombatPeriodEffects.Length != 0)
        {
            PeriodicCombatResourceEffect _combatResourceEffect = new PeriodicCombatResourceEffect(_source.CombatPeriodEffects, combatTaker, applierAttr, applierAttr != null);
            updatables.Add(_combatResourceEffect);
            stackables.Add(_combatResourceEffect);
        }

        // initialize attr modifier effect processor
        if (attrModifierTaker != null && _source.AttrModifiers.Length != 0)
        {
            AttrModifierEffect _attrModifierEffect = new AttrModifierEffect(_source.AttrModifiers, attrModifierTaker);
            stackables.Add(_attrModifierEffect);
        }

        // build constructor parameters
        IStatusUpdatable[] _updatables = updatables.Count == 0 ? emptyUpdatable : updatables.ToArray();
        IStatusStackable[] _stackables = stackables.Count == 0 ? emptyStackable : stackables.ToArray();

        // build status
        Status _status = new Status(_source, template.GetInstanceID(), _updatables, _stackables, stack, Mathf.Max(0, timePercent));

        return _status;
    }
}
