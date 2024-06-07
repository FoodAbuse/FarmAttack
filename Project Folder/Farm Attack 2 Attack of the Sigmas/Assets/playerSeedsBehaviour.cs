using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSeedsBehaviour : MonoBehaviour
{
    public enum SeedType
    {
        Carrot,
        Potato,
        Mint,
        Chilli,
        Popcorn,
        Beans

    }

    GameManager gameManager;
   // public Animator myAnim;
   // public Animator handsAnim;

    public GameObject currentChosenSeedType;

    public SeedType selectedSeed;
    public GameObject[] ammoTypeList;
    int index;


    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        selectedSeed = SeedType.Carrot;
        gameManager = FindObjectOfType<GameManager>();
       // myAnim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, 5))
            {
                if(hit.transform.gameObject.tag == "Planter")
                {
                    hit.transform.gameObject.GetComponent<plantPlotBehaviour>().RecieveInformation(index);
                }
            }
        }
    }

    public void SetSeedType(string newSeedType)
    {
        if (System.Enum.TryParse(newSeedType, out SeedType parsedAmmoType))
        {
            selectedSeed = parsedAmmoType;

            index = (int)selectedSeed;
            currentChosenSeedType = ammoTypeList[index];

        }
    }
}
