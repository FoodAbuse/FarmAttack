using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopBehaviour : MonoBehaviour
{
    public int indexNumbers;

    public GameObject carrotShopGO;
    public GameObject chilliShopGO;
    public GameObject potatoShopGO;
    public GameObject popcornShopGO;
    public GameObject cucumberShopGO;
    public GameObject beansShopGO;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        carrotShopGO.SetActive(indexNumbers == 1);
        chilliShopGO.SetActive(indexNumbers == 2);
        potatoShopGO.SetActive(indexNumbers == 3);
        popcornShopGO.SetActive(indexNumbers == 4);
        cucumberShopGO.SetActive(indexNumbers == 5);
        beansShopGO.SetActive(indexNumbers == 6);

    }


}

