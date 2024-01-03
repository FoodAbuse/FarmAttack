using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerItemSelector : MonoBehaviour
{
    public GameObject scythe;
    public GameObject gun;
    public GameObject seeds;

    public float itemSwapSpeed = 1f;
    private GameObject[] items;
    private int currentItemIndex = 0; // 0: Scythe, 1: Gun, 2: Seeds
    public Animator anim;

    private bool isSwapping = false;

    void Start()
    {
        items = new GameObject[] { scythe, gun, seeds };
        SelectItem(scythe); 

    }

    void Update()
    {
        // Input for item selection
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwapItem(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwapItem(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwapItem(2);
        }
    }

    void SwapItem(int newIndex)
    {
        if (newIndex != currentItemIndex && !isSwapping)
        {
            anim.SetBool("ItemSwapping", true);

            StartCoroutine(ActivateItemAfterDelay(items[newIndex]));
            currentItemIndex = newIndex;
        }
    }

    IEnumerator ActivateItemAfterDelay(GameObject item)
    {
        isSwapping = true;
        yield return new WaitForSeconds(itemSwapSpeed);

        foreach (GameObject i in items)
        {
            if (i == item)
            {
                i.SetActive(true);
            }
            else
            {
                i.SetActive(false);
            }
        }

        anim.SetBool("ItemSwapping", false);
        isSwapping = false;
    }

    void SelectItem(GameObject item)
    {
        foreach (GameObject i in items)
        {
            if (i == item)
            {
                i.SetActive(true);
            }
            else
            {
                i.SetActive(false);
            }
        }
    }
}
