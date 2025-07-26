using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsBehaviour : MonoBehaviour
{
    public enum AmmoType
    {
       Scythe,
       WateringCan,
       PumpkinTurret,
       Hands,
       StinkyCheese,
    }

    GameManager gameManager;
   // public Animator myAnim;

    public GameObject currentChosenItemType;

    public AmmoType selectedItem;
    public GameObject[] itemTypeList;
    int index;

    PlayerController playerController;

    cameraShakeBehaviour CamShakeControl;

    // Start is called before the first frame update
    void Start()
    {
        selectedItem = AmmoType.Scythe;
        gameManager = FindObjectOfType<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {



        if (gameManager._CanAttack)
        {
           // myAnim.enabled = true;
        }

        if (!gameManager._CanAttack)
        {
            //  myAnim.Play("Idle");
        }

    }

    public void SetAmmoType(string newAmmoType)
    {
        if (System.Enum.TryParse(newAmmoType, out AmmoType parsedAmmoType))
        {
            // Deactivate the previous item if it exists
            if (currentChosenItemType != null)
            {
                currentChosenItemType.SetActive(false);
            }

            selectedItem = parsedAmmoType;

            index = (int)selectedItem;
            currentChosenItemType = itemTypeList[index];

            // Activate the new one
            currentChosenItemType.SetActive(true);
        }
    }

        public void selectedItemBehaviour()
    {
       // myAnim.Play("Idle");
    }

}
