#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GradientGenerator))]
public class CustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GradientGenerator gScript = (GradientGenerator)target;

        // Generate Button
        if (GUILayout.Button("Generate"))
        {
            gScript.GenR8();
        }

        // Texture Preview (only if we have a generated texture)
        if (gScript.generatedGradient != null)
        {
            GUILayout.Label("Preview:");

            // Keep preview square
            float previewSize = Mathf.Min(EditorGUIUtility.currentViewWidth - 40, 128);
            Rect previewRect = GUILayoutUtility.GetRect(previewSize, previewSize, GUILayout.ExpandWidth(false));
            EditorGUI.DrawPreviewTexture(previewRect, gScript.generatedGradient);
        }

        
    }
}
#endif