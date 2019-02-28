using UnityEngine;
using Zenject;

public class MIChar : MonoInstaller
{
    public SOGlobalSetting globalSetting;

    public override void InstallBindings()
    {
        Container.Bind<SOGlobalSetting>().FromInstance(globalSetting);
        Container.BindFactory<GameObject, Vector3, Quaternion, Transform, Transform, FPrefab>().FromFactory<FPrefab.Factory>();
    }   
}