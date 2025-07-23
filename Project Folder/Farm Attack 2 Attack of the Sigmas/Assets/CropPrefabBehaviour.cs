using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CropPrefabBehaviour : MonoBehaviour
{
    public float myGrowthTime = 30f;              // seconds until mature
    private bool _isMature = false;

    [Range(0, 100)] public float moisture = 100f;
    public float moistureDecayPerSecond = 1f;     // moisture loss per second
   // public Image moistureBar;                     // optional UI

    public float highMoistureYield = 1.25f;       // >=80%
    public float midMoistureYield = 1.00f;       // >=50%
    public float lowMoistureYield = 0.75f;       // >=20%
    public float dryMoistureYield = 0.50f;       // <20%

    public GameObject[] myChildren;               // spawn positions
    public GameObject PublicAmmoGO;               // prefab to spawn
    public Sprite mySpriteForImage;               // final-stage icon

    void Start()
    {
        // start growth timer
        StartCoroutine(GrowRoutine());
       // if (moistureBar) moistureBar.fillAmount = moisture / 100f;
    }

    void Update()
    {
        // decay moisture over time
        moisture = Mathf.Max(0f, moisture - moistureDecayPerSecond * Time.deltaTime);
        //if (moistureBar) moistureBar.fillAmount = moisture / 100f;
    }

    public void WaterPlant(float amount)
    {
        moisture = Mathf.Min(100f, moisture + amount);
     //   if (moistureBar) moistureBar.fillAmount = moisture / 100f;
    }

    private IEnumerator GrowRoutine()
    {
        yield return new WaitForSeconds(myGrowthTime);
        _isMature = true;
    }

   
    public void HasBeenHarvested()
    {
        if (!_isMature) return;

        // choose yield multiplier
        float mul = dryMoistureYield;
        if (moisture >= 80f) mul = highMoistureYield;
        else if (moisture >= 50f) mul = midMoistureYield;
        else if (moisture >= 20f) mul = lowMoistureYield;

        int total = Mathf.RoundToInt(myChildren.Length * mul);
        for (int i = 0; i < total; i++)
        {
            // choose a random child slot
            var slot = myChildren[Random.Range(0, myChildren.Length)];
            Instantiate(PublicAmmoGO, slot.transform.position, slot.transform.rotation);
            print("YO IMA CARROT");
        }

        Destroy(gameObject);
    }


}
