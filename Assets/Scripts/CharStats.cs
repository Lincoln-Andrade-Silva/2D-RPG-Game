using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    public string name;

    public int level;
    public int currentEXP;
    public int[] expToNextLevel;
    private int maxLevel = 100;
    private int baseEXP = 1000;

    public int currentHP;
    public int maxHP = 100;
    public int[] hpLevelBonus;
    private int baseHP = 10;

    public int currentMP;
    public int maxMP = 10;
    public int[] mpLevelBonus;
    private int baseMP = 7;

    public int strength;
    public int defence;
    public int weaponPWR;
    public int armorPWR;

    public string equippedArmor;
    public string equippedWeapon;
    public Sprite charImage;

    // Start is called before the first frame update
    void Start()
    {
        SetMpLevelBonus();
        SetExpToNextLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddEXP(1000);
        }
    }

    public void AddEXP(int EXP)
    {
        if (level < maxLevel)
        {
            currentEXP += EXP;

            if (currentEXP >= expToNextLevel[level + 1])
            {
                level++;
                currentEXP = 0;
                if (level % 5 == 0)
                {
                    maxMP = maxMP + mpLevelBonus[level - 1];
                    maxHP = maxHP + hpLevelBonus[level - 1];
                }
                currentMP = maxMP;
                currentHP = maxHP;
            }
        }
        else
        {
            level = 100;
            currentEXP = 0;
        }
    }

    private void SetExpToNextLevel()
    {
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseEXP;
        for (int i = 2; i < expToNextLevel.Length; i++)
        {
            if (i < 20)
            {
                expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f);
            }
            else if (i > 20 && i < 40)
            {
                expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.06f);
            }
            else if (i > 40 && i < 50)
            {
                expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.07f);
            }
            else if (i > 50 && i < 60)
            {
                expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.08f);
            }
            else if (i > 60 && i < 70)
            {
                expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.09f);
            }
            else if (i > 70 && i < 80)
            {
                expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.1f);
            }
            else if (i > 80 && i < 90)
            {
                expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.11f);
            }
            else
            {
                expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.12f);
            }
        }
    }

    private void SetMpLevelBonus()
    {
        hpLevelBonus = new int[maxLevel];
        hpLevelBonus[1] = baseHP;

        mpLevelBonus = new int[maxLevel];
        mpLevelBonus[1] = baseMP;

        int lastMpBonus = 0;
        int lastHpBonus = 0;
        bool firstInteration = true;

        for (int i = 0; i < mpLevelBonus.Length; i++)
        {
            if ((i + 1) % 5 == 0)
            {
                if (firstInteration)
                {
                    mpLevelBonus[i] = baseMP;
                    hpLevelBonus[i] = baseHP;
                    firstInteration = false;
                }
                else
                {
                    mpLevelBonus[i] = Mathf.FloorToInt(lastMpBonus * 1.2f);
                    hpLevelBonus[i] = Mathf.FloorToInt(lastHpBonus * 1.2f);
                }

                lastMpBonus = mpLevelBonus[i];
                lastHpBonus = hpLevelBonus[i];
            }
            else
            {
                mpLevelBonus[i] = 0;
                hpLevelBonus[i] = 0;
            }
        }
    }
}
