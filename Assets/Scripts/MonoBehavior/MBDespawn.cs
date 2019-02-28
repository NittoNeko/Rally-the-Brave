using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// This behavior is for general creatures
public class MBDespawn : MonoBehaviour, IRemovable
{
    // Global settings
    private SOGlobalSetting globalSetting;

    // If use global time to despawn
    [SerializeField]
    private bool useGlobalSetting = true;

    // If useGlobalSetting is false, use this time to despawn
    private float despawnTime = 10f;

    public bool UseGlobalSetting { get => useGlobalSetting; }
    public float DespawnTime { get => despawnTime; set => despawnTime = value; }

    public event RemoveHandler OnRemove;

    [Zenject.Inject]
    private void Construct(SOGlobalSetting globalSetting)
    {
        this.globalSetting = globalSetting;
    }

    public void Remove()
    {
        OnRemove(transform);

        float seconds = globalSetting ? globalSetting.CorpseRemain : despawnTime;

        // Destroy this after seconds
        Destroy(gameObject, seconds);
    }
}


[CustomEditor(typeof(MBDespawn))]
public class MBDespawnEditor : Editor
{
    private MBDespawn despawn;

    void OnEnable()
    {
        despawn = target as MBDespawn;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (!despawn.UseGlobalSetting)
        {
            despawn.DespawnTime = EditorGUILayout.FloatField("Depsawn Time", despawn.DespawnTime);
        }

    }
}