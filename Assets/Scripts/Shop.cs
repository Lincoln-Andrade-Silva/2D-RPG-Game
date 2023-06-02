using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    [Header("Menu's")]
    public GameObject shopMenu;
    public GameObject buyMenu;
    public GameObject sellMenu;

    [Header("Gold Text")]
    public Text goldText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        shopMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update() { }

    public void OpenShop()
    {
        OpenBuyMenu();
        shopMenu.SetActive(true);
        GameManager.instance.shopActive = true;

        goldText.text = GameManager.instance.currentGold.ToString() + "g";
    }

    public void CloseShop()
    {
        shopMenu.SetActive(false);
        GameManager.instance.shopActive = false;
    }

    public void OpenBuyMenu()
    {
        buyMenu.SetActive(true);
        sellMenu.SetActive(false);
    }

    public void OpenSellMenu()
    {
        sellMenu.SetActive(true);
        buyMenu.SetActive(false);
    }
}
