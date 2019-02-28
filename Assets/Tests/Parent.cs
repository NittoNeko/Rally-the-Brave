using Zenject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Whatever();

[System.Serializable]
public class Parent : ITest
{
    public Whatever w;
    public int a = 0;
    [SerializeField]
    protected int b = 1;
    [SerializeField]
    private int c = 2;

    int ITest.getNum()
    {
        throw new System.NotImplementedException();
    }

    public void call()
    {
        w();
    }

    public class Factory : PlaceholderFactory<UnityEngine.Object, Parent> { }
}
