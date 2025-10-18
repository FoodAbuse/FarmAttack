using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public int IndexNumber;
    public GameObject seedsHotBarGO;

    public bool _InHotBar;
    public bool _CanAttack;

    [SerializeField] private PlayerController player;
    [SerializeField] private weaponBehaviour weapon;  // reference to the one gun in hand

    // Two ammo slots
    public weaponBehaviour.AmmoType weaponSlotA = weaponBehaviour.AmmoType.Carrot;
    public weaponBehaviour.AmmoType weaponSlotB = weaponBehaviour.AmmoType.Potato;
    private bool usingSlotA = true;

    void Start()
    {
        _CanAttack = true;

        // Start with slot A
        weapon.selectedAmmo = weaponSlotA;
    }

    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            weapon = FindObjectOfType<weaponBehaviour>();
        }
     

        IndexNumber = FindObjectOfType<playerItemSelector>().currentItemIndex;

        // 🔄 Weapon ammo swapping (Index 1)
        if (IndexNumber == 0 && Input.GetKeyDown(KeyCode.Q))
        {
            SwapWeaponAmmo();
        }

        // 🧰 Items (Index 1) - you can do the same logic here later

        // 🌱 Seeds inventory (Index 2)
        if (IndexNumber == 2 && Input.GetKeyDown(KeyCode.Tab)) inSeedsInventory();
        if (Input.GetKeyUp(KeyCode.Tab)) ExitSeedsInventory();

        
    }
    private void SwapWeaponAmmo()
    {
        usingSlotA = !usingSlotA;

        weapon.selectedAmmo = usingSlotA ? weaponSlotA : weaponSlotB;
        weapon.UpdateAmmoPrefab();

        weapon.myAnim.Play("PlayerSwapAmmo");
    }


    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void inSeedsInventory()
    {
        if (player != null) player.cameraFrozen = true;

        _CanAttack = false;
        Time.timeScale = .25f;
        _InHotBar = true;
        seedsHotBarGO.SetActive(true);
        UnlockMouse();
    }

    public void ExitSeedsInventory()
    {
        if (player != null) player.cameraFrozen = false;

        _CanAttack = true;
        Time.timeScale = 1;
        _InHotBar = false;
        seedsHotBarGO.SetActive(false);
        LockMouse();
    }
}
