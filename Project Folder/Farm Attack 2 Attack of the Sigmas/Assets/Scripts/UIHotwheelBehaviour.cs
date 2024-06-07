using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHotwheelBehaviour : MonoBehaviour
{
    Animator anim;
    public weaponBehaviour WeaponBehaviour;
    public playerSeedsBehaviour SeedsBehaviour;
    public string myAmmoName;
    public bool _isForSeeds;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        WeaponBehaviour = FindObjectOfType<weaponBehaviour>();
        SeedsBehaviour = FindObjectOfType<playerSeedsBehaviour>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonPress()
    {
        if (!_isForSeeds)
        {
            WeaponBehaviour.SetAmmoType(myAmmoName);
            FindObjectOfType<weaponBehaviour>().myAnim.Play("PlayerSwapAmmo");

        }
        if (_isForSeeds)
        {
            SeedsBehaviour.SetSeedType(myAmmoName);

        }

    }


}
