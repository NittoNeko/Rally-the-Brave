using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Sirenix.OdinInspector;

public class MBDirectorControl : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector director;
    [SerializeField, ListDrawerSettings(Expanded = true)]
    private float[] timePoints;

    private void Awake()
    {
        // disable this if timeline stopped
        director.stopped += (x) => { this.enabled = false; };
    }

    private void Update()
    {
        // if user press skip
        if (Input.GetButtonDown("Skip"))
        {
            // find next skip point
            for(int i = 0; i < timePoints.Length; ++i)
            {
                if (timePoints[i] > director.time)
                {
                    director.time = timePoints[i];
                    return;
                }
            }
        }
    }

    [Button(Name ="Reorder")]
    private void Reorder()
    {
        System.Array.Sort<float>(timePoints);
    }
}
