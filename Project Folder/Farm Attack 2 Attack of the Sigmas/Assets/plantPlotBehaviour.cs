using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class plantPlotBehaviour : MonoBehaviour
{
  

    int myGrowTime; // Has to be set to the animation length of the growth
    float timer;

    public GameObject chosenPlantPrefab; // I choose you! To be set by when player clicks on the plot with the specific seeds in hand
    public GameObject[] PlantPrefabs; // Put those suckers in here
    public GameObject TempPrefabPlant;

    public TextMeshProUGUI TitleText;
    public bool _finishedGrowing;
    public bool _startGrowing;
    public bool _spawnedPrefab;
    public bool _hasBeenHarvested;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(chosenPlantPrefab != null) // should stop this always being called or even being called if nothing is in here
        {
            myGrowTime = chosenPlantPrefab.GetComponent<CropPrefabBehaviour>().myGrowthTime; // just grabs the other copmonent info so this needs to be set in CropPrefabBehaviour
        }

        if(_startGrowing && !_finishedGrowing)
        {
            if(!_spawnedPrefab)
            {
                TempPrefabPlant = Instantiate(chosenPlantPrefab, transform.position, transform.rotation); // this is the thing in the world
                TitleText.text = chosenPlantPrefab.name.ToString();
                _spawnedPrefab = true;
            }

            timer += Time.deltaTime;

            if (timer >= myGrowTime)
            {
                _finishedGrowing = true;
            }
        }

        if(_hasBeenHarvested) // full reset
        {
            TempPrefabPlant.GetComponent<CropPrefabBehaviour>().HasBeenHarvested();
            TitleText.text = "Empty Plot";
            Destroy(TempPrefabPlant);
            TempPrefabPlant = null;
            _spawnedPrefab = false;
            _finishedGrowing = false;
            chosenPlantPrefab = null;
            _startGrowing = false;
            timer = 0;
            _hasBeenHarvested = false;
        }

    }

    public void RecieveInformation(int index)
    {
        if (!_startGrowing)
        {
            chosenPlantPrefab = PlantPrefabs[index];
            _startGrowing = true;
        }
    }

}
