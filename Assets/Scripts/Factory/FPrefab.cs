using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


// Just a wrapper for FPrefabBind
public class FPrefab : PlaceholderFactory<GameObject, Vector3, Quaternion, Transform, Transform>
{
    public class Factory : IFactory<GameObject, Vector3, Quaternion, Transform, Transform>
    {
        readonly DiContainer container;

        public Factory(DiContainer container)
        {
            this.container = container;
        }

        public Transform Create(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            GameObject go;
            //if (parent == null)
            //{
            //    go = container.InstantiatePrefab(prefab, parent);
            //    go.transform.position = position;
            //    go.transform.rotation = rotation;
            //} else
            //{
            //    go = container.InstantiatePrefab(prefab, position, rot, parent);
            //}
            go = container.InstantiatePrefab(prefab, position, rotation, parent);
            return go.transform;
            
        }
    }
}