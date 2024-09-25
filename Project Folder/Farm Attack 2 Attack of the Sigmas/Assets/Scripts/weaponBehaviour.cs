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
    public Animator myAnim;
    public Animator handsAnim;

    public GameObject currentChosenAmmoType;
    public Transform gunEnd;

    public AmmoType selectedAmmo;
    public GameObject[] ammoTypeList;
    int index;

    public AudioSource soundSource;
    public AudioClip soundClip;

    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
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

            if (Input.GetMouseButtonDown(0) && !playerController._isRunning) // is stationary and shooting
            {
                if (index == 4)
                {
                    myAnim.Play("ShootPopCorn");

                    myAnim.SetBool("isShootingPopcorn", true);
                    myAnim.SetBool("isRun", false);
                }

                if (index != 4)
                {
                    myAnim.Play("Shoot");

                    myAnim.SetBool("isShooting", true);
                    myAnim.SetBool("isRun", false);
                }

                handsAnim.SetBool("PlayerIsShooting", true);

                Camera.main.GetComponent<cameraShakeBehaviour>().ShakeCamera();

            }

            if (!Input.GetMouseButton(0) && !playerController._isRunning && myAnim.GetBool("isShootingPopcorn") == false) // not running or shooting
            {
                handsAnim.SetBool("PlayerIsShooting", false);

                myAnim.SetBool("isShooting", false);

                myAnim.SetBool("isRun", false);

            }
            if (!Input.GetMouseButton(0) && playerController._isRunning && myAnim.GetBool("isShootingPopcorn") == false) // is running
            {
                handsAnim.SetBool("PlayerIsShooting", false);

                myAnim.SetBool("isShooting", false);
                myAnim.SetBool("isShootingPopcorn", false);

                myAnim.SetBool("isRun", true);
            }
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
            selectedAmmo = parsedAmmoType;

            index = (int)selectedAmmo;
            currentChosenAmmoType = ammoTypeList[index];

        }
    }


    public void FireGun()
    {
        Debug.Log("FireGun called");
        soundSource.PlayOneShot(soundClip);

        Instantiate(currentChosenAmmoType, gunEnd.position, transform.rotation);
    }

    public void selectedAmmoBehaviour()
    {
        myAnim.Play("Idle");
    }

   public void TurnOffPopCorn()
    {
        myAnim.SetBool("isShootingPopcorn", false);
    }
}
