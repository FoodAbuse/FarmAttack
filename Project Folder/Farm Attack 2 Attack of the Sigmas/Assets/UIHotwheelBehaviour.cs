using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHotwheelBehaviour : MonoBehaviour
{
    Animator anim;
    public weaponBehaviour WeaponBehaviour;
    public string myAmmoName;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        WeaponBehaviour = FindObjectOfType<weaponBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonPress()
    {
        WeaponBehaviour.SetAmmoType(myAmmoName);
    }


}
