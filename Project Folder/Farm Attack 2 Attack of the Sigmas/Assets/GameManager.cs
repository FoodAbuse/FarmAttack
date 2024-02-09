using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject hotBarGO;

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

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inHotBarInventory();
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
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
    public void ExitHotBarInventory()
    {
        _CanAttack = true;

        Time.timeScale = 1;
        _InHotBar = false;
        hotBarGO.SetActive(false);
        LockMouse();
    }
}
