using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Characters Status")]
    public CharStats[] charStats;

    [Header("Conditionals")]
    public bool menuOpen;
    public bool shopActive;
    public bool dialogActive;
    public bool fadingBetweenAreas;

    [Header("Items Config")]
    public string[] itemsHeld;
    public int[] numberOfItems;
    public Item[] referenceItems;

    [Header("Gold")]
    public int currentGold;

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
        SortItems();
    }

    // Update is called once per frame
    void Update()
    {
        if (menuOpen || dialogActive || fadingBetweenAreas || shopActive)
        {
            PlayerController.instance.canMove = false;
        }
        else
        {
            PlayerController.instance.canMove = true;
        }
    }

    public Item GetItemInformation(string item)
    {
        foreach (var referenceItem in referenceItems)
        {
            if (referenceItem.name == item)
            {
                return referenceItem;
            }
        }
        return null;
    }

    public void SortItems()
    {
        bool itemAfterSpace = true;

        while (itemAfterSpace)
        {
            itemAfterSpace = false;
            for (int i = 0; i < itemsHeld.Length - 1; i++)
            {
                if (itemsHeld[i] == "")
                {
                    itemsHeld[i] = itemsHeld[i + 1];
                    itemsHeld[i + 1] = "";

                    numberOfItems[i] = numberOfItems[i + 1];
                    numberOfItems[i + 1] = 0;

                    if (itemsHeld[i] != "")
                    {
                        itemAfterSpace = true;
                    }
                }
            }
        }
    }

    public void AddItem(string item)
    {
        int newItemPosition = 0;
        bool foundSpace = false;

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == "" || itemsHeld[i] == item)
            {
                newItemPosition = i;
                i = itemsHeld.Length;
                foundSpace = true;
            }
        }

        if (foundSpace)
        {
            bool itemExists = false;
            for (int i = 0; i < referenceItems.Length; i++)
            {
                if (referenceItems[i].name == item)
                {
                    itemExists = true;
                    i = itemsHeld.Length;
                }
            }

            if (itemExists)
            {
                itemsHeld[newItemPosition] = item;
                numberOfItems[newItemPosition]++;
            }
            else
            {
                Debug.LogError(item + " Does not exists !");
            }
        }

        GameMenu.instance.ShowItems();
    }

    public void RemoveItem(string item)
    {
        int itemPosition = 0;
        bool foundItem = false;

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == item)
            {
                foundItem = true;
                itemPosition = i;
                i = itemsHeld.Length;
            }
        }

        if (foundItem)
        {
            numberOfItems[itemPosition]--;
            if (numberOfItems[itemPosition] <= 0)
            {
                itemsHeld[itemPosition] = "";
            }
            GameMenu.instance.ShowItems();
        }
        else
        {
            Debug.LogError("Couldn't find " + item + " !");
        }
    }

    public void SaveData()
    {
        SavePlayerPosition();
        SavePlayerInfo();
        SaveInventoryData();
    }

    public void LoadData()
    {
        LoadPlayerPosition();
        LoadPlayerInfo();
        LoadInventoryData();
        GameMenu.instance.CloseMenu();
    }

    private void SavePlayerPosition()
    {
        PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("Player_Position_x", PlayerController.instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_y", PlayerController.instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_z", PlayerController.instance.transform.position.z);
    }

    private void SavePlayerInfo()
    {
        foreach (var stats in charStats)
        {
            if (stats != null && stats.gameObject.activeInHierarchy)
            {
                if (stats.gameObject.activeInHierarchy)
                {
                    PlayerPrefs.SetInt("Player_" + stats.name + "_active", 1);
                }
                else
                {
                    PlayerPrefs.SetInt("Player_" + stats.name + "_active", 0);
                }

                PlayerPrefs.SetInt("Player_" + stats.name + "_Level", stats.level);
                PlayerPrefs.SetInt("Player_" + stats.name + "_CurrentExp", stats.currentEXP);
                PlayerPrefs.SetInt("Player_" + stats.name + "_CurrentHP", stats.currentHP);
                PlayerPrefs.SetInt("Player_" + stats.name + "_MaxHP", stats.maxHP);
                PlayerPrefs.SetInt("Player_" + stats.name + "_CurrentMP", stats.currentMP);
                PlayerPrefs.SetInt("Player_" + stats.name + "_MaxMP", stats.maxMP);
                PlayerPrefs.SetInt("Player_" + stats.name + "_Strength", stats.strength);
                PlayerPrefs.SetInt("Player_" + stats.name + "_Defence", stats.defence);
                PlayerPrefs.SetInt("Player_" + stats.name + "_WeaponPower", stats.weaponPWR);
                PlayerPrefs.SetInt("Player_" + stats.name + "_ArmorPower", stats.armorPWR);
                PlayerPrefs.SetString(
                    "Player_" + stats.name + "_EquippedWeapon",
                    stats.equippedWeapon
                );
                PlayerPrefs.SetString(
                    "Player_" + stats.name + "_EquippedArmor",
                    stats.equippedArmor
                );
            }
        }
        PlayerPrefs.SetInt("Player_Current_Gold", GameManager.instance.currentGold);
    }

    private void SaveInventoryData()
    {
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            PlayerPrefs.SetString("ItemInInventory_" + i, itemsHeld[i]);
            PlayerPrefs.SetInt("ItemAmount_" + i, numberOfItems[i]);
        }
    }

    private void LoadPlayerPosition()
    {
        AreaExit.instance.LoadSceneFromLoadButton();
        PlayerController.instance.transform.position = new Vector3(
            PlayerPrefs.GetFloat("Player_Position_x"),
            PlayerPrefs.GetFloat("Player_Position_y"),
            PlayerPrefs.GetFloat("Player_Position_z")
        );
    }

    private void LoadPlayerInfo()
    {
        foreach (var stats in charStats)
        {
            if (stats != null && stats.gameObject.activeInHierarchy)
            {
                if (PlayerPrefs.GetInt("Player_" + stats.name + "_active") == 0)
                {
                    stats.gameObject.SetActive(false);
                }
                else
                {
                    stats.gameObject.SetActive(true);
                }

                stats.level = PlayerPrefs.GetInt("Player_" + stats.name + "_Level");
                stats.currentEXP = PlayerPrefs.GetInt("Player_" + stats.name + "_CurrentExp");
                stats.currentHP = PlayerPrefs.GetInt("Player_" + stats.name + "_CurrentHP");
                stats.maxHP = PlayerPrefs.GetInt("Player_" + stats.name + "_MaxHP");
                stats.currentMP = PlayerPrefs.GetInt("Player_" + stats.name + "_CurrentMP");
                stats.maxMP = PlayerPrefs.GetInt("Player_" + stats.name + "_MaxMP");
                stats.strength = PlayerPrefs.GetInt("Player_" + stats.name + "_Strength");
                stats.defence = PlayerPrefs.GetInt("Player_" + stats.name + "_Defence");
                stats.weaponPWR = PlayerPrefs.GetInt("Player_" + stats.name + "_WeaponPower");
                stats.armorPWR = PlayerPrefs.GetInt("Player_" + stats.name + "_ArmorPower");
                stats.equippedWeapon = PlayerPrefs.GetString(
                    "Player_" + stats.name + "_EquippedWeapon"
                );
                stats.equippedArmor = PlayerPrefs.GetString(
                    "Player_" + stats.name + "_EquippedArmor"
                );
            }
        }
        GameManager.instance.currentGold = PlayerPrefs.GetInt("Player_Current_Gold");
    }

    private void LoadInventoryData()
    {
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            itemsHeld[i] = PlayerPrefs.GetString("ItemInInventory_" + i);
            numberOfItems[i] = PlayerPrefs.GetInt("ItemAmount_" + i);
        }
    }
}
