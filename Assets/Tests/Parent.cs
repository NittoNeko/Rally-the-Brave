using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using Zenject;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System.Diagnostics;
using System.Collections.ObjectModel;

public class Parent : MonoBehaviour
{
    public MBCharAttr attr;
    private int loop = 10000;
    // Test get components and getcomponent
    // Test compare null and for loop
    private void Awake()
    {
        //IAttrHolder single = GetComponent<IAttrHolder>();
        //IAttrHolder[] mult = GetComponents<IAttrHolder>();
        attr = GetComponent<MBCharAttr>();
        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();
        int x = 1;
        for (int i = 0; i < loop; ++i)
        {
            attr.GetAttr(EAttrType.Armor);
        }
        stopwatch.Stop();
        UnityEngine.Debug.Log("GetComponent costs " + stopwatch.Elapsed);
        stopwatch.Reset();

        //stopwatch.Start();
        //byte a = 1;
        //byte b = 1;
        //for (int i = 0; i < loop; ++i)
        //{
        //    if (single != null)
        //    {
        //        a += b;
        //    }
        //}

        //int d = 999999;
        //byte c = (byte)d;
        //print(c);

        //stopwatch.Stop();
        //UnityEngine.Debug.Log("GetComponents costs " + stopwatch.Elapsed);
        //stopwatch.Reset();

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


}
