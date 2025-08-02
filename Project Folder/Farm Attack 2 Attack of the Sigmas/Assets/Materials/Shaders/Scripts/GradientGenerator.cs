using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GradientGenerator : MonoBehaviour
{
    public int layers = 1;
    public List<float> steps = new List<float>() { 0f, 1f };
    public Texture2D generatedGradient;

    void Start()
    {
        GenR8();
        SaveGradientRuntime();
    }

    public void GenR8()
    {
        if (steps.Count < 2) return;

        generatedGradient = new Texture2D(100, layers, TextureFormat.RGBA32, false);
        float adjustedLength = steps.Count - 1;
        float colourSplit = 1f / adjustedLength;

        for (int i = 0; i < steps.Count; i++)
        {
            int currentPos = Mathf.RoundToInt(steps[i] * generatedGradient.width);
            int lastPos = (i == 0) ? 0 : Mathf.RoundToInt(steps[i - 1] * generatedGradient.width);
            int cVal = Mathf.RoundToInt((i * colourSplit) * 255);
            Color32 myC = new Color32((byte)cVal, (byte)cVal, (byte)cVal, 255);

            int wAffected = Mathf.Clamp(currentPos - lastPos, 1, generatedGradient.width - lastPos);
            Color32[] newVal = new Color32[wAffected];
            for (int ii = 0; ii < newVal.Length; ii++)
                newVal[ii] = myC;

            generatedGradient.SetPixels32(lastPos, 0, wAffected, generatedGradient.height, newVal);
        }
        generatedGradient.Apply();
    }

    public void SaveGradientRuntime()
    {
        byte[] bytes = generatedGradient.EncodeToPNG();
        string path = Path.Combine(Application.persistentDataPath, "Gradient.png");
        File.WriteAllBytes(path, bytes);
        Debug.Log("Gradient saved to: " + path);
    }
}