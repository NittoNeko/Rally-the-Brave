using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MBDespawnMgr : MonoBehaviour, IDespawnable
{
    // global settings
    private SOGlobalSetting globalSetting;


    [Zenject.Inject]
    private void Construct(SOGlobalSetting globalSetting)
    {
        this.globalSetting = globalSetting;
    }

    public void Remove()
    {



        // destroy this after seconds
    }

    public void Despawn()
    {
        throw new System.NotImplementedException();
    }
}