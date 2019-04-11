using UnityEngine;
using UnityEditor;
using System.Collections;

namespace PolyNav{

    [CustomEditor(typeof(PolyNav2D))]
    public class PolyNav2DInspector : Editor {

        private PolyNav2D polyNav{
            get {return target as PolyNav2D;}
        }

        public override void OnInspectorGUI(){

            base.OnInspectorGUI();

            if (GUILayout.Button("Generate Maps"))
            {
                polyNav.GenerateMap();
            }

            if (Application.isPlaying){
                EditorGUILayout.LabelField("Nodes Count", polyNav.nodesCount.ToString());
            }
        }
    }
}