using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[ExecuteInEditMode]
public class GradientGenerator : MonoBehaviour
{
    [SerializeField]
    private int layers;

    [SerializeField]
    public Texture2D generatedGradient;

    [SerializeField]
    [Range(0f, 1f)]
    private List<float> steps;

    private byte[] bytesToExport;
    [SerializeField]
    private string desiredFilePath;
    [SerializeField]
    private string desiredFileName;
    private string exportPath;
    

    





    public void GenR8()
    {
        generatedGradient = new Texture2D(100, layers, TextureFormat.RGBA32, false);
        float adjustedLength = steps.Count - 1;
        Debug.Log(adjustedLength);
        float colourSplit = (1 / adjustedLength);
        Debug.Log(colourSplit);

        for (int i = 0; i < steps.Count; i++)
        {
            int currentPos = (Mathf.RoundToInt(steps[i] * generatedGradient.width));

            int lastPos;
            int endPos;


            //int cVal = Mathf.RoundToInt(steps[i] * 255);
            //Debug.Log((byte)cVal);

            int cVal = Mathf.RoundToInt((i * colourSplit) * 255);
            //Debug.Log(cVal);

            if (i == 0 || i == (steps.Count-1))
            {
               if(i == 0) //If we are first in line
                {
                    lastPos = 0;
                    endPos = Mathf.RoundToInt(steps[i + 1] * generatedGradient.width);
                    
                }
               else //If we are last in line
                {
                    lastPos = Mathf.RoundToInt(steps[i - 1] * generatedGradient.width);
                }
            }
            else
            {
                lastPos = Mathf.RoundToInt(steps[i - 1] * generatedGradient.width);
                endPos = Mathf.RoundToInt(steps[i + 1] * generatedGradient.width);
            }
            Color32 myC = new Color32((byte)cVal, (byte)cVal, (byte)cVal, 255);

            int wAffected = currentPos - lastPos;
            Color32[] newVal = new Color32[wAffected];
            for (int ii = 0; ii<newVal.Length; ii++)
            {
                newVal[ii] = myC;
            }
            generatedGradient.SetPixels32(lastPos, 0, wAffected, generatedGradient.height, newVal);
        }
        generatedGradient.Apply();
    }

    public void Export()
    {
        exportPath = Application.dataPath+desiredFilePath+desiredFileName;
        Debug.Log(exportPath);

        bytesToExport = ImageConversion.EncodeToPNG(generatedGradient);


        File.WriteAllBytes(exportPath, bytesToExport);

    }
}
