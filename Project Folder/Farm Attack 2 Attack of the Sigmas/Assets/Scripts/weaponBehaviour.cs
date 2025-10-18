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

    [Header("Setup")]
    public AmmoType selectedAmmo;      // Active ammo type
    public GameObject[] ammoTypeList;  // Prefabs for each ammo, index must match enum
    public Transform gunEnd;

    [Header("FX")]
    public Animator myAnim;
    public Animator handsAnim;
    public ParticleSystem chilliParticles;
    public AudioSource soundSource;
    public AudioClip soundClip;

    private GameObject currentChosenAmmoType;
    private GameManager gameManager;
    private cameraShakeBehaviour camShake;

    void Start()
    {
        camShake = FindObjectOfType<cameraShakeBehaviour>();
        gameManager = FindObjectOfType<GameManager>();
        myAnim = GetComponent<Animator>();

        UpdateAmmoPrefab(); // set prefab on start
    }

    void Update()
    {
        if (!gameManager._CanAttack) return;

        if (Input.GetMouseButtonDown(0))
        {
            HandleShootAnim();
            handsAnim.SetBool("PlayerIsShooting", true);
        }

        if (!Input.GetMouseButton(0))
        {
            chilliParticles.enableEmission = false;
            handsAnim.SetBool("PlayerIsShooting", false);
            ResetShootBools();
        }

        if (Input.GetMouseButtonUp(0))
        {
            chilliParticles.enableEmission = false;
            handsAnim.SetBool("PlayerIsShooting", false);
            ResetShootBools();
        }
    }

    private void HandleShootAnim()
    {
        switch (selectedAmmo)
        {
            case AmmoType.Popcorn:
                myAnim.Play("ShootPopCorn");
                myAnim.SetBool("isShootingPopcorn", true);
                break;

            case AmmoType.Potato:
                myAnim.Play("ShootPotato");
                myAnim.SetBool("isShootingPotato", true);
                break;

            case AmmoType.Carrot:
                myAnim.Play("Shoot");
                myAnim.SetBool("isShooting", true);
                break;

            case AmmoType.Chilli:
                chilliParticles.enableEmission = true;
                break;
        }
    }

    private void ResetShootBools()
    {
        myAnim.SetBool("isShooting", false);
        myAnim.SetBool("isShootingPopcorn", false);
        myAnim.SetBool("isShootingPotato", false);
    }

    public void FireGun()
    {
        if (currentChosenAmmoType == null) UpdateAmmoPrefab();

        camShake.ShakeCamera(.0175f);
        soundSource.PlayOneShot(soundClip);

        Instantiate(currentChosenAmmoType, gunEnd.position, transform.rotation);
    }

    /// <summary>
    /// Call this whenever selectedAmmo changes.
    /// </summary>
    public void UpdateAmmoPrefab()
    {
        int index = (int)selectedAmmo;
        if (index >= 0 && index < ammoTypeList.Length)
        {
            currentChosenAmmoType = ammoTypeList[index];
        }
    }
}

