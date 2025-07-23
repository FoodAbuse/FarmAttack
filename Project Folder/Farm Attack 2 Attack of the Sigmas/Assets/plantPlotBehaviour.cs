using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class plantPlotBehaviour : MonoBehaviour
{
  

    float myGrowTime; // Has to be set to the animation length of the growth
    float timer;

    public GameObject chosenPlantPrefab; // I choose you! To be set by when player clicks on the plot with the specific seeds in hand
    public GameObject[] PlantPrefabs; // Put those suckers in here
    public GameObject TempPrefabPlant;
    public GameObject myCanvas;
    public Image showcaseImage;

    public bool _finishedGrowing;
    public bool _startGrowing;
    public bool _spawnedPrefab;
    public bool _hasBeenHarvested;

    public float activationDistance = 5f; // Detecting if player is close this is the max distance
    public Transform playerPos;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(playerPos.transform.position, transform.position);

        // Enable if within range, disable if not
        myCanvas.SetActive(distance <= activationDistance);

        if(myCanvas != null)
        {
            myCanvas.transform.LookAt(playerPos);
        }


        if (chosenPlantPrefab != null) // should stop this always being called or even being called if nothing is in here
        {
            myGrowTime = chosenPlantPrefab.GetComponent<CropPrefabBehaviour>().myGrowthTime; // just grabs the other copmonent info so this needs to be set in CropPrefabBehaviour
        }

        if(_startGrowing && !_finishedGrowing)
        {
            if(!_spawnedPrefab)
            {

                TempPrefabPlant = Instantiate(chosenPlantPrefab, transform.position, transform.rotation); // this is the thing in the world
                TempPrefabPlant.transform.parent = transform;
                
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
            Destroy(TempPrefabPlant);
            TempPrefabPlant = null;
            _spawnedPrefab = false;
            _finishedGrowing = false;
            chosenPlantPrefab = null;
            _startGrowing = false;
            timer = 0;
            _hasBeenHarvested = false;
            showcaseImage = null;
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
