using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtom : MonoBehaviour
{
    public Image buttomImage;
    public Text amountText;
    public int buttomValue;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void Press()
    {
        if (GameMenu.instance.menu.activeInHierarchy)
        {
            if (GameManager.instance.itemsHeld[buttomValue] != "")
            {
                GameMenu.instance.SelectItem(
                    GameManager.instance.GetItemInformation(
                        GameManager.instance.itemsHeld[buttomValue]
                    )
                );
            }
        }

        if (Shop.instance.shopMenu.activeInHierarchy)
        {
            if (Shop.instance.buyMenu.activeInHierarchy)
            {
                Shop.instance.SelectBuyItem(
                    GameManager.instance.GetItemInformation(Shop.instance.itemsForSale[buttomValue])
                );
            }
            if (Shop.instance.sellMenu.activeInHierarchy)
            {
                Shop.instance.SelectSellItem(
                    GameManager.instance.GetItemInformation(GameManager.instance.itemsHeld[buttomValue])
                );
            }
        }
    }
}
