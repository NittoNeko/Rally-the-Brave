using UnityEngine;
using System.Collections.Generic;

public class MBCharStatus : MonoBehaviour, IAttrModifier
{
    private SOSpecialStatusResist specialStatusResist;
    private SpecialStatusGroup<SpecialStatus> specialStatus;

    private List<Status> statuses = new List<Status>(10);

    public event AttrModChange OnAttrModChange;

    // Avoid duplicate initialization of array
    private float[] modHolder = new float[EnumArray.AttrModLayerCount];

    public float[] GetModifiers(EAttrType type)
    {
        // Reset all modification to 0
        for (int i = 0; i < modHolder.Length; ++i)
        {
            modHolder[i] = 0;
        }

        // Base multiplicative should be 1
        modHolder[(int)EAttrModLayer.Independent] = 1;

        // Iterate through all modifications
        foreach (Status s in statuses)
        {
            foreach (AttrModifier modifier in s.Content.AttrModifiers)
            {
                int index = (int) modifier.Layer;
                if (modifier.Layer == EAttrModLayer.Independent)
                {
                    modHolder[index] *= modifier.Value;
                } else
                {
                    modHolder[index] += modifier.Value;
                }
            }
        }

        return modHolder;
    }

    public void TakeStatus(SOStatus content, ushort stack, ushort time)
    {
        // Find if this status has already existed and stack them
        foreach(Status status in statuses)
        {
            if (status.Content == content)
            {
                status.RemainTime += time;
                status.Stack += stack;
                TriggerAttrChange(status);
                return;
            }
        }

        // Here no same status found
        // So add new status
        Status s = new Status(stack, time, content);
        statuses.Add(s);
        TriggerAttrChange(s);
    }



    private void Update()
    {
        
    }

    private void TriggerAttrChange(Status s)
    {
        // If no attributes modifiers, skip triggering
        if (s.Content.AttrModifiers.Length == 0) { return; }

        // For every changed attributes, recalculate them
        foreach (AttrModifier am in s.Content.AttrModifiers)
        {
            OnAttrModChange(am.AttrType);
        }
    }

    private void TriggerSpecialStatus(Status s)
    {
        // If no special status, skip triggering
        if (s.Content.SpecialStatus.Length == 0) { return; }

        foreach(ESpecialStatusType type in s.Content.SpecialStatus)
        {

        }
    }

    private void ApplySpecialStatus(ESpecialStatusType type)
    {
        SpecialStatus ss = specialStatus.Get(type);
        SpecialStatusResist ssr = specialStatusResist.Get(type);

        // Check if immune to this effect
        if (ss.CurrentStack >= ssr.MaxStack)
        {

        }
    }

    private void UpdateStatus()
    {
        // Reduce the remaining time of statuses
        foreach(Status status in statuses)
        {

        }
    }

    


    private class Status
    {
        private ushort stack;
        private float remainTime;
        private readonly SOStatus content;

        public ushort Stack { get => stack;
            set
            {
                if (value > content.MaxStack)
                {
                    stack = content.MaxStack;
                } else if (value < 0)
                {
                    stack = 0;
                } else
                {
                    stack = value;
                }
            }
        }
        public float RemainTime { get => remainTime;
            set
            {        
                if (value > content.MaxTime)
                {
                    remainTime = content.MaxTime;
                } else if (value < 0)
                {
                    remainTime = 0;
                } else
                {
                    remainTime = value;
                }
            }
        }
        public SOStatus Content { get => content;}

        public Status(ushort stack, ushort remainTime, SOStatus content)
        {
            this.Stack = stack;
            this.RemainTime = remainTime;
            this.content = content;
        }
    }

    private class SpecialStatus
    {
        private ushort remainStack; // If > 0, this effect exists
        private ushort currentStack; // How many stacks have been applied within reset time
        private float currentTime; // Start counting down from reset time once this effect has first time applied

        public ushort RemainStack { get => remainStack; set => remainStack = value; }
        public ushort CurrentStack { get => currentStack; set => currentStack = value; }
        public float CurrentTime { get => currentTime; set => currentTime = value; }

    }

    [System.Serializable]
    private class SpecialStatusResist
    {
        [SerializeField]
        private ushort maxStack; // Once current statck reaches max stack, immune to this effect until resetTime reaches zero
        [SerializeField]
        private float resetTime;

        public ushort MaxStack { get => maxStack; set => maxStack = value; }
        public float ResetTime { get => resetTime; set => resetTime = value; }
    }

    // T can only be SpecialStatus or SpecialStatusResist
    [System.Serializable]
    private class SpecialStatusGroup<T>
    {
        private T snared; // Unable to move
        private T unwise; // Unable to use items
        private T coward; // Unable to use skills
        private T stunned; // Unable to do any action
        private T purified; // Immnue to debuff
        private T unstoppable; // Immnue to special debuffs
        private T immortal; // Immnue to damage

        public T Get(ESpecialStatusType type)
        {
            switch (type)
            {
                case ESpecialStatusType.Snared:
                    return snared;
                case ESpecialStatusType.Unwise:
                    return unwise;
                case ESpecialStatusType.Coward:
                    return coward;
                case ESpecialStatusType.Stunned:
                    return stunned;
                case ESpecialStatusType.Purified:
                    return purified;
                case ESpecialStatusType.Unstoppable:
                    return unstoppable;
                case ESpecialStatusType.Immortal:
                    return immortal;
                default:
                    return default;
            }
        }
    }

    [CreateAssetMenu(fileName = "StatusResist", menuName = "ScriptableObject/StatusResist")]
    private class SOSpecialStatusResist : ScriptableObject
    {
        [SerializeField]
        private readonly SpecialStatusGroup<SpecialStatusResist> specialStatusResist;

        public SpecialStatusResist Get(ESpecialStatusType type)
        {
            return specialStatusResist.Get(type);
        }
    }
}
