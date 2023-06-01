using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Type")]
    public bool isItem;
    public bool isWeapon;
    public bool isArmor;

    [Header("Item Details")]
    public string name;
    public string description;
    public int value;
    public Sprite sprite;

    [Header("Item Effects")]
    public int amoutToChange;
    public bool affectHP,
        affectMP,
        affectStr;

    [Header("Weapon/Armor Details")]
    public int weaponStrength;
    public int armorDefense;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void Use(int character)
    {
        CharStats selected = GameManager.instance.charStats[character];

        if (isItem)
        {
            if (affectHP)
            {
                selected.currentHP += amoutToChange;

                if (selected.currentHP > selected.maxHP)
                {
                    selected.currentHP = selected.maxHP;
                }
            }

            if (affectMP)
            {
                selected.currentMP += amoutToChange;

                if (selected.currentMP > selected.maxMP)
                {
                    selected.currentMP = selected.maxMP;
                }
            }

            if (affectStr)
            {
                selected.strength += amoutToChange;
            }
        }

        if (isWeapon)
        {
            if (selected.equippedWeapon != "")
            {
                GameManager.instance.AddItem(selected.equippedWeapon);
            }

            selected.equippedWeapon = name;
            selected.weaponPWR = weaponStrength;
        }

        if (isArmor)
        {
            if (selected.equippedArmor != "")
            {
                GameManager.instance.AddItem(selected.equippedArmor);
            }

            selected.equippedArmor = name;
            selected.armorPWR = armorDefense;
        }

        GameManager.instance.RemoveItem(name);
    }
}
