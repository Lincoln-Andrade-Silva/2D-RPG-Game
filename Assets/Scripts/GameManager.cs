using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if (Input.GetKeyDown(KeyCode.J))
        {
            AddItem("Wood Sword");
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
}
