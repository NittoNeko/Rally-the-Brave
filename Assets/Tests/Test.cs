using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using Zenject;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class Test : MonoBehaviour
{
    public Boo bo;
    [SerializeField]
    public int[] nums;

    private void Awake()
    {
        if (nums == null)
        {
            print("null");
        }

        print(nums.Length);
    }

    private void Update()
    {
        
    }
}

public class Boo : ITest
{
    public int getNum()
    {
        throw new System.NotImplementedException();
    }
}