using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public static GameMenu instance;

    public GameObject menu;

    [Header("Tabs")]
    public GameObject[] windows;

    [Header("Character Stats")]
    private CharStats[] charStats;

    public Text[] nameTextInput;
    public Text[] hpTextInput;
    public Text[] mpTextInput;
    public Text[] lvlTextInput;
    public Text[] expTextInput;

    public Slider[] expSliderInput;
    public Image[] charImage;
    public GameObject[] charStatHolder;

    [Header("Character Details Stats")]
    public GameObject[] statusButtons;

    public Text statusName;
    public Text statusHP;
    public Text statusMP;
    public Text statusSTR;
    public Text statusDEF;
    public Text statusWeaponEquiped;
    public Text statusWeaponPower;
    public Text statusArmorEquiped;
    public Text statusArmorPower;
    public Text statusExp;
    public Image statusImage;

    [Header("Select Item")]
    public ItemButtom[] itemButtoms;
    public string selectedItem;
    public Item activeItem;
    public Text itemName;
    public Text itemDescription;
    public Text useButtonText;

    [Header("Chose Char for Item")]
    public GameObject itemCharChoiceMenu;
    public Text[] itemCharChoiceNames;

    [Header("Gold")]
    public Text goldText;

    [Header("Quest")]
    public GameObject[] questPainels;
    public Text[] questTitle;
    public Text[] questDescription;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
        DisableTabs();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && !GameManager.instance.shopActive)
        {
            if (menu.activeInHierarchy)
            {
                DisableTabs();
                GameManager.instance.menuOpen = false;
            }
            else
            {
                menu.SetActive(true);
                GameManager.instance.menuOpen = true;
                charStats = GameManager.instance.charStats;
                UpdateMainInformation();
            }
        }
    }

    public void ToggleWindow(int windowNumber)
    {
        for (int i = 0; i < windows.Length; i++)
        {
            if (i == windowNumber)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            }
            else
            {
                windows[i].SetActive(false);
            }
        }

        itemCharChoiceMenu.SetActive(false);
    }

    public void OpenStats()
    {
        StatusChar(0);
        for (int i = 0; i < statusButtons.Length; i++)
        {
            statusButtons[i].SetActive(charStats[i].gameObject.activeInHierarchy);
            statusButtons[i].GetComponentInChildren<Text>().text = charStats[i].name;
        }
    }

    public void OpenQuests()
    {
        for (int i = 0; i < QuestManager.instance.quests.Count; i++)
        {
            questTitle[i].text = QuestManager.instance.quests[i].title;
            questDescription[i].text = QuestManager.instance.quests[i].description;
            questPainels[i].SetActive(true);
        }
    }

    public void StatusChar(int selected)
    {
        statusName.text = charStats[selected].name;
        statusHP.text = "" + charStats[selected].currentHP + "/" + charStats[selected].maxHP;
        statusMP.text = "" + charStats[selected].currentMP + "/" + charStats[selected].maxMP;
        statusSTR.text = charStats[selected].strength.ToString();
        statusDEF.text = charStats[selected].defence.ToString();
        statusArmorPower.text = charStats[selected].armorPWR.ToString();
        statusWeaponPower.text = charStats[selected].weaponPWR.ToString();
        statusExp.text = (
            charStats[selected].expToNextLevel[charStats[selected].level]
            - charStats[selected].currentEXP
        ).ToString();
        statusImage.sprite = charStats[selected].charImage;

        if (charStats[selected].equippedArmor != "")
        {
            statusArmorEquiped.text = charStats[selected].equippedArmor;
        }
        else
        {
            statusArmorEquiped.text = "None";
        }

        if (charStats[selected].equippedWeapon != "")
        {
            statusWeaponEquiped.text = charStats[selected].equippedWeapon;
        }
        else
        {
            statusWeaponEquiped.text = "None";
        }
    }

    private void UpdateMainInformation()
    {
        for (int i = 0; i < charStats.Length; i++)
        {
            if (charStats[i].gameObject.activeInHierarchy)
            {
                charStatHolder[i].SetActive(true);

                nameTextInput[i].text = charStats[i].name;
                hpTextInput[i].text = "HP: " + charStats[i].currentHP + "/" + charStats[i].maxHP;
                mpTextInput[i].text = "MP: " + charStats[i].currentMP + "/" + charStats[i].maxMP;
                lvlTextInput[i].text = "Lvl: " + charStats[i].level;
                expTextInput[i].text =
                    ""
                    + charStats[i].currentEXP
                    + "/"
                    + charStats[i].expToNextLevel[charStats[i].level];
                expSliderInput[i].maxValue = charStats[i].expToNextLevel[charStats[i].level];
                expSliderInput[i].value = charStats[i].currentEXP;
                charImage[i].sprite = charStats[i].charImage;
            }
            else
            {
                charStatHolder[i].SetActive(false);
            }
        }

        goldText.text = GameManager.instance.currentGold.ToString() + "g";
    }

    public void ShowItems()
    {
        DeselectItem();
        for (int i = 0; i < itemButtoms.Length; i++)
        {
            itemButtoms[i].buttomValue = i;

            if (GameManager.instance.itemsHeld[i] != "")
            {
                itemButtoms[i].buttomImage.gameObject.SetActive(true);
                itemButtoms[i].buttomImage.sprite = GameManager.instance
                    .GetItemInformation(GameManager.instance.itemsHeld[i])
                    .sprite;
                itemButtoms[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                itemButtoms[i].buttomImage.gameObject.SetActive(false);
                itemButtoms[i].amountText.text = "";
            }
        }
    }

    public void SortItems()
    {
        GameManager.instance.SortItems();
        ShowItems();
    }

    public void SelectItem(Item item)
    {
        activeItem = item;

        if (activeItem.isItem)
        {
            useButtonText.text = "Use";
        }
        else if (activeItem.isArmor || activeItem.isWeapon)
        {
            useButtonText.text = "Equip";
        }

        itemName.text = activeItem.name;
        itemDescription.text = activeItem.description;
    }

    public void DeselectItem()
    {
        activeItem = null;
        useButtonText.text = "Use";
        itemName.text = "Item Name";
        itemDescription.text = "Description";
    }

    public void DiscartItem()
    {
        if (activeItem != null)
        {
            GameManager.instance.RemoveItem(activeItem.name);
        }
    }

    public void OpenItemCharChoise()
    {
        itemCharChoiceMenu.SetActive(true);

        for (int i = 0; i < itemCharChoiceNames.Length; i++)
        {
            itemCharChoiceNames[i].text = GameManager.instance.charStats[i].name;
            itemCharChoiceNames[i].transform.parent.gameObject.SetActive(
                GameManager.instance.charStats[i].gameObject.activeInHierarchy
            );
        }
    }

    public void CloseItemCharChoise()
    {
        DeselectItem();
        itemCharChoiceMenu.SetActive(false);
    }

    public void UseItem(int selectedChar)
    {
        activeItem.Use(selectedChar);
        CloseItemCharChoise();
        UpdateMainInformation();
    }

    public void DisableTabs()
    {
        menu.SetActive(false);
        foreach (var statHolder in charStatHolder)
        {
            statHolder.SetActive(false);
        }

        foreach (var window in windows)
        {
            window.SetActive(false);
        }
        foreach (var butt in statusButtons)
        {
            butt.SetActive(false);
        }
        foreach (var painel in questPainels)
        {
            painel.SetActive(false);
        }

        itemCharChoiceMenu.SetActive(false);
        itemCharChoiceNames[0].transform.parent.gameObject.SetActive(false);
        itemCharChoiceNames[1].transform.parent.gameObject.SetActive(false);
        itemCharChoiceNames[2].transform.parent.gameObject.SetActive(false);
    }
}
