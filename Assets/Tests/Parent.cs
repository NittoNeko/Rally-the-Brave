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
    public int n = 0;
    public Test t;
    private int loop = 10000;
    // Test get components and getcomponent
    // Test compare null and for loop
    private void Awake()
    {

        //IAttrHolder single = GetComponent<IAttrHolder>();
        //IAttrHolder[] mult = GetComponents<IAttrHolder>();

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

       
    }

    public void G()
    {

    }

    private void FixedUpdate()
    {
        n += 1;

        print(n);
    }


    private void Update()
    {
        if (n == 10)
        {
            print("win");
        }
    }
}
