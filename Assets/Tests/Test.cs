using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using Zenject;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System.Diagnostics;
using System.Collections.ObjectModel;

public class Test : SerializedMonoBehaviour
{
    private int loop = 10000;
    private int i = 0;
    Stopwatch stopwatch = new Stopwatch();
    // Test get components and getcomponent
    // Test compare null and for loop
    private void Awake()
    {
        //IAttrHolder single = GetComponent<IAttrHolder>();
        //IAttrHolder[] mult = GetComponents<IAttrHolder>();


        //stopwatch.Start();
        //int z = 0;
        //for (int i = 0; i < loop; ++i)
        //{
        //    z += a.num;
        //}

        //stopwatch.Stop();
        //UnityEngine.Debug.Log("GetComponentInChildren costs " + stopwatch.Elapsed);
        //stopwatch.Reset();

        //stopwatch.Start();
        //EAttrType q;
        //for (int i = 0; i < loop; ++i)
        //{
        //}

        //stopwatch.Stop();
        //UnityEngine.Debug.Log("GetComponentsInChildren costs " + stopwatch.Elapsed);
        //stopwatch.Reset();
    }

    private void Update()
    {
    }
}


