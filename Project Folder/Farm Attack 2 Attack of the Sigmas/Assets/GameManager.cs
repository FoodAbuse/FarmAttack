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
    // Start is called before the first frame update
    void Start()
    {
        LockMouse();
        _CanAttack = true;
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
        _CanAttack = false; // stops player from shooting
        Time.timeScale = .25f; // slows game speed
        _InHotBar = true;
        hotBarGO.SetActive(true); // enables the inventory screen
        UnlockMouse();
    }
    public void inSeedsInventory()
    {
        _CanAttack = false; // stops player from shooting
        Time.timeScale = .25f; // slows game speed
        _InHotBar = true;
        seedsHotBarGO.SetActive(true); // enables the inventory screen
        UnlockMouse();
    }
    public void ExitHotBarInventory()
    {
        _CanAttack = true;

        Time.timeScale = 1;
        _InHotBar = false;
        hotBarGO.SetActive(false);
        LockMouse();
    }
    public void ExitSeedsInventory()
    {
        _CanAttack = true;

        Time.timeScale = 1;
        _InHotBar = false;
        seedsHotBarGO.SetActive(false);
        LockMouse();
    }
}
