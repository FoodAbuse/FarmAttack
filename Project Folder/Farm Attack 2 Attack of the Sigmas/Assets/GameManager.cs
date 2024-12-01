using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int IndexNumber;

    public GameObject hotBarGO;
    public GameObject seedsHotBarGO;

    public bool _InHotBar;
    public bool _CanAttack;

    [SerializeField]
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        LockMouse();
        _CanAttack = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        IndexNumber = FindObjectOfType<playerItemSelector>().currentItemIndex;

        if (Input.GetKeyDown(KeyCode.Tab))
        {

            if (IndexNumber == 0)
            {
                inHotBarInventory();

            }
            if (IndexNumber == 2)
            {
                inSeedsInventory();

            }
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            ExitSeedsInventory();
            ExitHotBarInventory();
        }

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

    public void inHotBarInventory()
    {
        // Hugo Addition - locking head rotation whilst selecting ammo type
        if (player != null)
        {
            player.cameraFrozen = true;
        }


        _CanAttack = false; // stops player from shooting
        Time.timeScale = .25f; // slows game speed
        _InHotBar = true;
        hotBarGO.SetActive(true); // enables the inventory screen
        UnlockMouse();
    }
    public void inSeedsInventory()
    {
        // Hugo Addition - locking head rotation whilst selecting seed type
        if (player != null)
        {
            player.cameraFrozen = true;
        }

        _CanAttack = false; // stops player from shooting
        Time.timeScale = .25f; // slows game speed
        _InHotBar = true;
        seedsHotBarGO.SetActive(true); // enables the inventory screen
        UnlockMouse();
    }
    public void ExitHotBarInventory()
    {
        // Hugo Addition - unlocking head rotation after ammo is selected
        if (player != null)
        {
            player.cameraFrozen = false;
        }

        _CanAttack = true;

        Time.timeScale = 1;
        _InHotBar = false;
        hotBarGO.SetActive(false);
        LockMouse();
    }
    public void ExitSeedsInventory()
    {
        // Hugo Addition - unlocking head rotation after seed type is selected
        if (player != null)
        {
            player.cameraFrozen = false;
        }

        _CanAttack = true;

        Time.timeScale = 1;
        _InHotBar = false;
        seedsHotBarGO.SetActive(false);
        LockMouse();
    }
}
