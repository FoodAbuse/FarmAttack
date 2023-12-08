using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GradientGenerator))]
public class CustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
       
        DrawDefaultInspector();

        GradientGenerator gScript = (GradientGenerator) target;
        {
            if(GUILayout.Button("Generate"))
            {
                gScript.GenR8();



                int y = gScript.generatedGradient.height;
                int x = gScript.generatedGradient.width;

                /*
                GUILayout.Label("Preview", GUILayout.Height(y), GUILayout.Width(x));
                GUI.DrawTexture(GUILayoutUtility.GetLastRect(), gScript.generatedGradient);
                */

                GUILayout.Label(gScript.generatedGradient);

                EditorGUI.PrefixLabel(new Rect(25, 45, 100, 15), 0, new GUIContent("Preview:"));
                EditorGUI.DrawPreviewTexture(new Rect(25, 60, 100, 100), gScript.generatedGradient);

            }

            if (GUILayout.Button("Export"))
            {
                gScript.Export();

            }
        }

        
    }
    

 
}
