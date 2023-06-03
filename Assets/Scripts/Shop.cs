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

    [Header("Items")]
    public string[] itemsForSale = new string[44];
    public ItemButtom[] buyItemButtons;
    public ItemButtom[] sellItemButtons;
    public Item selectedItem;

    [Header("Item Buy Details")]
    public Text buyItemName;
    public Text buyItemDescription;
    public Text buyItemValue;

    [Header("Item Sell Details")]
    public Text sellItemName;
    public Text sellItemDescription;
    public Text sellItemValue;

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

        for (int i = 0; i < buyItemButtons.Length; i++)
        {
            buyItemButtons[i].buttomValue = i;

            if (itemsForSale[i] != "")
            {
                buyItemButtons[i].buttomImage.gameObject.SetActive(true);
                buyItemButtons[i].buttomImage.sprite = GameManager.instance
                    .GetItemInformation(itemsForSale[i])
                    .sprite;
                buyItemButtons[i].amountText.text = "";
            }
            else
            {
                buyItemButtons[i].buttomImage.gameObject.SetActive(false);
                buyItemButtons[i].amountText.text = "";
            }
        }
    }

    public void OpenSellMenu()
    {
        buyMenu.SetActive(false);
        sellMenu.SetActive(true);

        GameManager.instance.SortItems();
        ShowSellItems();
    }

    public void SelectBuyItem(Item item)
    {
        selectedItem = item;
        buyItemName.text = selectedItem.name;
        buyItemDescription.text = selectedItem.description;
        buyItemValue.text = "Value: " + selectedItem.value + "g";
    }

    public void SelectSellItem(Item item)
    {
        selectedItem = item;
        sellItemName.text = selectedItem.name;
        sellItemDescription.text = selectedItem.description;
        sellItemValue.text =
            "Value: " + Mathf.FloorToInt(selectedItem.value * .5f).ToString() + "g";
    }

    public void BuyItem()
    {
        if (selectedItem != null)
        {
            if (GameManager.instance.currentGold >= selectedItem.value)
            {
                GameManager.instance.currentGold -= selectedItem.value;

                GameManager.instance.AddItem(selectedItem.name);
            }

            goldText.text = GameManager.instance.currentGold.ToString() + "g";
        }
    }

    public void SellItem()
    {
        if (selectedItem != null)
        {
            int quantityItem = 0;
            for (int i = 0; i < GameManager.instance.itemsHeld.Length; i++)
            {
                if (
                    GameManager.instance.itemsHeld[i] == selectedItem.name
                    && GameManager.instance.numberOfItems[i] != 0
                )
                {
                    GameManager.instance.currentGold += Mathf.FloorToInt(selectedItem.value * .5f);

                    GameManager.instance.RemoveItem(selectedItem.name);
                    goldText.text = GameManager.instance.currentGold.ToString() + "g";
                    ShowSellItems();

                    if ((GameManager.instance.numberOfItems[i]) <= 0)
                    {
                        selectedItem = null;
                        sellItemName.text = "Item Name";
                        sellItemDescription.text = "Item Description";
                        sellItemValue.text = "Value: ";
                    }

                    i = GameManager.instance.itemsHeld.Length;
                }
            }
        }
    }

    private void ShowSellItems()
    {
        for (int i = 0; i < sellItemButtons.Length; i++)
        {
            sellItemButtons[i].buttomValue = i;

            if (GameManager.instance.itemsHeld[i] != "")
            {
                sellItemButtons[i].buttomImage.gameObject.SetActive(true);
                sellItemButtons[i].buttomImage.sprite = GameManager.instance
                    .GetItemInformation(GameManager.instance.itemsHeld[i])
                    .sprite;
                sellItemButtons[i].amountText.text = GameManager.instance.numberOfItems[
                    i
                ].ToString();
            }
            else
            {
                sellItemButtons[i].buttomImage.gameObject.SetActive(false);
                sellItemButtons[i].amountText.text = "";
            }
        }
    }
}
