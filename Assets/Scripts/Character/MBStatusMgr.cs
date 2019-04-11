using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MBStatusMgr : MonoBehaviour, IStatusTaker
{
    // lists of statuses that exist on this entity
    private List<IStatus> statuses;

    private IAttrModifierTaker attrModifiable;
    private ICombater combater;

    private System.Action Updater;

    /// <summary>
    /// <see cref="IStatusTaker.TakeStatus(IStatus, int, float)"/>
    /// </summary>
    /// <param name="status"></param>
    /// <param name="sourceId"></param>
    /// <param name="stack"></param>
    /// <param name="timePercent"></param>
    public bool TakeStatus(IStatus status)
    {
        // search for the same status
        int _index = SearchFor(status.SourceId);

        // stack them
        if (_index >= 0)
        {
            statuses[_index].RemainTime += status.RemainTime;
            statuses[_index].Stack += status.Stack;
        } else
        {
            status.OnExpire += OnStatusExpired;
            statuses.Add(status);

        }

        return true;
    }

    /// <summary>
    /// <see cref="IStatusTaker.RemoveStatus(int)"/>
    /// </summary>
    /// <param name="sourceId"></param>
    /// <param name="isForce"></param>
    public void RemoveStatus(int sourceId)
    {
        // look for index of status that is being removed
        int _index = SearchFor(sourceId);

        // found if greater than 0
        if (_index >= 0)
        {
            statuses[_index].SetExpired();
        }
    }

    private void Awake()
    {
        statuses = new List<IStatus>(10);
        Updater = NormalUpdate;

        attrModifiable = GetComponent<IAttrModifierTaker>();
        combater = GetComponent<ICombater>();

        Reporter.ComponentMissingCheck(attrModifiable);
        Reporter.ComponentMissingCheck(combater);
    }

    private void Update()
    {
        // updater will switch flows depending on different cases
        Updater();
    }

    /// <summary>
    /// Return the index of a certain status, otherwise return -1.
    /// </summary>
    /// <param name="sourceId"></param>
    /// <returns></returns>
    private int SearchFor(int sourceId)
    {
        // search for content
        for (int i = 0; i < statuses.Count; ++i)
        {
            if (statuses[i].SourceId == sourceId)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// Be ready to remove expired.
    /// Always remove an expired status in next frame.
    /// </summary>
    private void OnStatusExpired()
    {
        Updater = RemoveExpired;
    }
    
    /// <summary>
    /// Remove expired status and return back to normal update.
    /// Only called on the frame that needs to remove expired status.
    /// </summary>
    private void RemoveExpired()
    {
        // remove all expired 
        statuses.RemoveAll((x) => x.IsExpired);

        // return to normal updates
        Updater = NormalUpdate;

        // invoke normal update in current frame
        Updater();
    }

    /// <summary>
    /// Update all statues.
    /// </summary>
    private void NormalUpdate()
    {
        for (int i = 0; i < statuses.Count; ++i)
        {
            statuses[i].OnUpdate();
        }
    }
}
