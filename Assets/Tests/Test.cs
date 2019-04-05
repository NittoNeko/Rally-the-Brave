using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using Zenject;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System.Diagnostics;
using System.Collections.ObjectModel;
using UnityEngine.SceneManagement;

public delegate void TTT(float a ,float b, float c , float d);

public class Test : MonoBehaviour
{
    public bool IsDestroy = false;
    public SOStatusTpl status;
    public Test[] test;
    private int loop = 100;
    private bool iii = true;
    public QWE qwe;
    private string s = "asd";
    Stopwatch stopwatch = new Stopwatch();
    private List<int> ints = new List<int>();

    // Test get components and getcomponent
    // Test compare null and for loop
    private void Awake()
    {
        //for(int i = 0; i < attr.Length; ++i)
        //{
        //    attr[i] = new harAttr();
        //    t += attr[i].NT;
        //}

        dsa
    }

    public void testSpeed()
    {
        stopwatch.Start();

        for (int i = 0; i < loop; ++i)
        {
            print("asd");
        }

        stopwatch.Stop();
        UnityEngine.Debug.Log("empty loop " + stopwatch.Elapsed);
        stopwatch.Reset();

        stopwatch.Start();

        for (int i = 0; i < loop; ++i)
        {
            print(s);
        }

        stopwatch.Stop();
        UnityEngine.Debug.Log("empty loop " + stopwatch.Elapsed);
        stopwatch.Reset();
    }

}

[System.Serializable]
public class QWE
{
    public int i = 0;

    public int n = Initial();

    public QWE()
    {

    }

    public static int Initial()
    {

        return 1;
    }
}

public class TestB : MBAttrMgr
{
    private Attribute attr;
}