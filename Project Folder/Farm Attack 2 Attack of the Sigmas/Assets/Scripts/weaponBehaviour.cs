using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponBehaviour : MonoBehaviour
{
    public enum AmmoType
    {
        Carrot,
        Potato,
        Mint,
        Chilli,
        Popcorn,
        Beans

    }

    GameManager gameManager;
    Animator myAnim;
    public Animator handsAnim;

    public GameObject currentChosenAmmoType;
    public Transform gunEnd;

    public AmmoType selectedAmmo;
    public GameObject[] ammoTypeList;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        selectedAmmo = AmmoType.Carrot;
        gameManager = FindObjectOfType<GameManager>();
        myAnim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager._CanAttack)
        {
            myAnim.enabled = true;

            if (Input.GetMouseButtonDown(0))
            {
                myAnim.Play("Shoot");
                handsAnim.SetBool("PlayerIsShooting", true);

                Camera.main.GetComponent<cameraShakeBehaviour>().ShakeCamera();

            }
            if (!Input.GetMouseButton(0))
            {
                handsAnim.SetBool("PlayerIsShooting", false);

                myAnim.Play("Idle");
            }
        }

        if (!gameManager._CanAttack)
        {
            myAnim.enabled = false;
        }

    }

    public void SetAmmoType(string newAmmoType)
    {
        if (System.Enum.TryParse(newAmmoType, out AmmoType parsedAmmoType))
        {
            selectedAmmo = parsedAmmoType;

            index = (int)selectedAmmo;
            currentChosenAmmoType = ammoTypeList[index];


        }
    }


    public void FireGun()
    {

        Instantiate(currentChosenAmmoType, gunEnd.position, transform.rotation);

    }

    
}
