using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject monsterObject;
    public GameObject MainMenu;
    public GameObject AfterBattleReport;

    public GameObject[] LevelButtons;

    public GameObject InventoryObject;
    public GameObject LevelSelectObject;
    public GameObject MoveSelectObject;

    public class UsedMove
    {
        public string Name = "";
        public string Type = "";
        public float Damage = 10f;
        public int CoolDown = 0;
        public int TurnsTillUse = 0;
        public string StatusEffect = "";
        public int StatusEffectTurns = 0;
        public float StatusDamage = 0f;
    }

    public Dictionary<int, UsedMove> MoveStats = new Dictionary<int, UsedMove>() {
        { 1, new UsedMove { Name = "", Type = "", Damage = 0 } },
        { 2, new UsedMove { Name = "", Type = "", Damage = 0 } }
    };

    public int LevelID = 0;
    public int Level = 0;
    public float gold = 0;
    public TextMeshProUGUI GoldText;
    public bool InBattle = false;

    public int currentMonster = 0;

    public string[] MonsterNames;
    public int[] MonsterLevels;
    // Start is called before the first frame update
    void Start()
    {
        Level = 1;
        StartCoroutine(InitializeInventory());
        InitializeLevelButtons();
    }

    // Update is called once per frame
    void Update()
    {
        GoldText.text = "Gold: " + gold;
    }

    IEnumerator InitializeInventory()
    {
        InventoryObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        InventoryObject.SetActive(false);
        MoveSelectObject.transform.Find("Moves Editor Panel").gameObject.SetActive(false);
        MoveSelectObject.SetActive(false);
    }

    public void startbattle()
    {
        InBattle = true;
        playerObject.GetComponent<PlayerScript>().Moves[1].TurnsTillUse = 0;
        playerObject.GetComponent<PlayerScript>().Moves[2].TurnsTillUse = 0;
        playerObject.GetComponent<PlayerScript>().Moves[3].TurnsTillUse = 0;
        playerObject.GetComponent<PlayerScript>().Moves[4].TurnsTillUse = 0;
        playerObject.GetComponent<PlayerScript>().Moves[5].TurnsTillUse = 0;
        //Level 1
        currentMonster = 0;
        monsterObject.GetComponent<MonsterScript>().TotalGoldDropped = 0;
        monsterObject.GetComponent<MonsterScript>().TotalExpDropped = 0;
    }

    public void CreateMonsterRoster(int ID)
    {
        startbattle();
        LevelID = ID;
        if (ID == 1)
        {
            MonsterNames = new string[1];
            MonsterNames[0] = "Salamander";
            MonsterLevels = new int[1];
            MonsterLevels[0] = 1;
        }
        if (ID == 2)
        {
            MonsterNames = new string[2];
            MonsterNames[0] = "Salamander";
            MonsterNames[1] = "Salamander";
            MonsterLevels = new int[2];
            MonsterLevels[0] = 2;
            MonsterLevels[1] = 2;
        }
        if (ID == 3)
        {
            MonsterNames = new string[2];
            MonsterNames[0] = "Salamander";
            MonsterNames[1] = "Salamander";
            MonsterLevels = new int[2];
            MonsterLevels[0] = 3;
            MonsterLevels[1] = 3;
        }
        if (ID == 4)
        {
            MonsterNames = new string[2];
            MonsterNames[0] = "Salamander";
            MonsterNames[1] = "Salamander";
            MonsterLevels = new int[2];
            MonsterLevels[0] = 2;
            MonsterLevels[1] = 2;
        }
        if (ID == 5)
        {
            MonsterNames = new string[2];
            MonsterNames[0] = "Salamander";
            MonsterNames[1] = "Salamander";
            MonsterLevels = new int[2];
            MonsterLevels[0] = 4;
            MonsterLevels[1] = 4;
        }
        if (ID == 6)
        {
            MonsterNames = new string[4];
            MonsterNames[0] = "Salamander";
            MonsterNames[1] = "Salamander";
            MonsterNames[2] = "Salamander";
            MonsterNames[3] = "Slime";
            MonsterLevels = new int[4];
            MonsterLevels[0] = 4;
            MonsterLevels[1] = 4;
            MonsterLevels[2] = 4;
            MonsterLevels[3] = 5;
        }
        if (ID == 7)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Salamander";
            MonsterNames[1] = "Salamander";
            MonsterNames[2] = "Salamander";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 4;
            MonsterLevels[1] = 4;
            MonsterLevels[2] = 5;
        }
        if (ID == 8)
        {
            MonsterNames = new string[1];
            MonsterNames[0] = "Salamangreat";
            MonsterLevels = new int[1];
            MonsterLevels[0] = 5;
        }//BOSS ROUND
        if (ID == 9)
        {
            MonsterNames = new string[1];
            MonsterNames[0] = "Slime";
            MonsterLevels = new int[1];
            MonsterLevels[0] = 7;
        }
        if (ID == 10)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Salamander";
            MonsterNames[1] = "Salamander";
            MonsterNames[2] = "Slime";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 10;
            MonsterLevels[1] = 10;
            MonsterLevels[2] = 7;
        }
        if (ID == 11)
        {
            MonsterNames = new string[2];
            MonsterNames[0] = "Slime";
            MonsterNames[1] = "Slime";
            MonsterLevels = new int[2];
            MonsterLevels[0] = 8;
            MonsterLevels[1] = 8;
        }
        if (ID == 12)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Slime";
            MonsterNames[1] = "Slime";
            MonsterNames[2] = "Slime";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 9;
            MonsterLevels[1] = 9;
            MonsterLevels[2] = 9;
        }
        if (ID == 13)
        {
            MonsterNames = new string[1];
            MonsterNames[0] = "Wraith";
            MonsterLevels = new int[1];
            MonsterLevels[0] = 10;
        }
        if (ID == 14)
        {
            MonsterNames = new string[2];
            MonsterNames[0] = "Wraith";
            MonsterNames[1] = "Wraith";
            MonsterLevels = new int[2];
            MonsterLevels[0] = 12;
            MonsterLevels[1] = 12;
        }
        if (ID == 15)
        {
            MonsterNames = new string[1];
            MonsterNames[0] = "Wraith";
            MonsterLevels = new int[1];
            MonsterLevels[0] = 15;
        }
        if (ID == 16)
        {
            MonsterNames = new string[1];
            MonsterNames[0] = "Wraith";
            MonsterLevels = new int[1];
            MonsterLevels[0] = 20;
        }
        if (ID == 17)
        {
            MonsterNames = new string[1];
            MonsterNames[0] = "Giant_Slime";
            MonsterLevels = new int[1];
            MonsterLevels[0] = 10;
        } //BOSS ROUND
        if (ID == 18)
        {
            MonsterNames = new string[2];
            MonsterNames[0] = "Slime";
            MonsterNames[1] = "Slime";
            MonsterLevels = new int[2];
            MonsterLevels[0] = 10;
            MonsterLevels[1] = 10;
        }
        if (ID == 19)
        {
            MonsterNames = new string[4];
            MonsterNames[0] = "Salamander";
            MonsterNames[1] = "Slime";
            MonsterNames[2] = "Salamander";
            MonsterNames[3] = "Slime";
            MonsterLevels = new int[4];
            MonsterLevels[0] = 12;
            MonsterLevels[1] = 10;
            MonsterLevels[2] = 12;
            MonsterLevels[3] = 10;
        }
        if (ID == 20)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Slime";
            MonsterNames[1] = "Slime";
            MonsterNames[2] = "Slime";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 12;
            MonsterLevels[1] = 12;
            MonsterLevels[2] = 12;

        }
        if (ID == 21)
        {
            MonsterNames = new string[1];
            MonsterNames[0] = "Salamander";
            MonsterLevels = new int[1];
            MonsterLevels[0] = 20;
        }
        if (ID == 22)
        {
            MonsterNames = new string[2];
            MonsterNames[0] = "Salamander";
            MonsterNames[1] = "Salamander";
            MonsterLevels = new int[2];
            MonsterLevels[0] = 20;
            MonsterLevels[1] = 20;
        }
        if (ID == 23)
        {
            MonsterNames = new string[1];
            MonsterNames[0] = "Slime";
            MonsterLevels = new int[1];
            MonsterLevels[0] = 20;
        }
        if (ID == 24)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Slime";
            MonsterNames[1] = "Slime";
            MonsterNames[2] = "Yeti";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 15;
            MonsterLevels[1] = 15;
            MonsterLevels[2] = 10;
        }
        if (ID == 25)
        {
            MonsterNames = new string[2];
            MonsterNames[0] = "Salamangreat";
            MonsterNames[1] = "Giant_Slime";
            MonsterLevels = new int[2];
            MonsterLevels[0] = 15;
            MonsterLevels[1] = 15;

        } //BOSS ROUND
        if (ID == 26)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Skeleton";
            MonsterNames[1] = "Skeleton";
            MonsterNames[2] = "Skeleton";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 15;
            MonsterLevels[1] = 15;
            MonsterLevels[2] = 15;
        }
        if (ID == 27)
        {
            MonsterNames = new string[1];
            MonsterNames[0] = "Pirate_Skeleton";
            MonsterLevels = new int[1];
            MonsterLevels[0] = 15;
        }
        if (ID == 28)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Skeleton";
            MonsterNames[1] = "Skeleton";
            MonsterNames[2] = "Pirate_Skeleton";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 17;
            MonsterLevels[1] = 17;
            MonsterLevels[2] = 15;
        }
        if (ID == 29)
        {
            MonsterNames = new string[1];
            MonsterNames[0] = "Wraith";
            MonsterLevels = new int[1];
            MonsterLevels[0] = 15;
        }
        if (ID == 30)
        {
            MonsterNames = new string[1];
            MonsterNames[0] = "Pirate_King";
            MonsterLevels = new int[1];
            MonsterLevels[0] = 20;
        } //BOSS ROUND
        if (ID == 31)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Pirate_Skeleton";
            MonsterNames[1] = "Pirate_Skeleton";
            MonsterNames[2] = "Pirate_Skeleton";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 20;
            MonsterLevels[1] = 20;
            MonsterLevels[2] = 20;
        }
        if (ID == 32)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Skeleton";
            MonsterNames[1] = "Skeleton";
            MonsterNames[2] = "Pirate_Skeleton";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 25;
            MonsterLevels[1] = 25;
            MonsterLevels[2] = 20;
        }
        if (ID == 33)
        {
            MonsterNames = new string[1];
            MonsterNames[0] = "Pirate_Skeleton";
            MonsterLevels = new int[1];
            MonsterLevels[0] = 25;
        }
        if (ID == 34)
        {
            MonsterNames = new string[1];
            MonsterNames[0] = "Pirate_Skeleton";
            MonsterLevels = new int[1];
            MonsterLevels[0] = 27;
        }
        if (ID == 35)
        {
            MonsterNames = new string[1];
            MonsterNames[0] = "Pirate_Skeleton";
            MonsterLevels = new int[1];
            MonsterLevels[0] = 30;
        }
        if (ID == 36)
        {
            MonsterNames = new string[2];
            MonsterNames[0] = "Lightning_Rabbit";
            MonsterNames[1] = "Lightning_Rabbit";
            MonsterLevels = new int[2];
            MonsterLevels[0] = 22;
            MonsterLevels[1] = 22;
        }
        if (ID == 37)
        {
            MonsterNames = new string[2];
            MonsterNames[0] = "Fire_wolf";
            MonsterNames[1] = "Fire_wolf";
            MonsterLevels = new int[2];
            MonsterLevels[0] = 22;
            MonsterLevels[1] = 22;
        }
        if (ID == 38)
        {
            MonsterNames = new string[4];
            MonsterNames[0] = "Fire_wolf";
            MonsterNames[1] = "Lightning_Rabbit";
            MonsterNames[2] = "Fire_Wolf";
            MonsterNames[3] = "Lightning_Rabbit";
            MonsterLevels = new int[4];
            MonsterLevels[0] = 23;
            MonsterLevels[1] = 23;
            MonsterLevels[2] = 23;
            MonsterLevels[3] = 23;
        }
        if (ID == 39)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Fire_wolf";
            MonsterNames[1] = "Lightning_Rabbit";
            MonsterNames[2] = "Yeti";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 25;
            MonsterLevels[1] = 25;
            MonsterLevels[2] = 25;
        }
        if (ID == 40)
        {
            MonsterNames = new string[1];
            MonsterNames[0] = "Beast_Yeti";
            MonsterLevels = new int[1];
            MonsterLevels[0] = 25;
        }
        if (ID == 41)
        {
            MonsterNames = new string[1];
            MonsterNames[0] = "Fire_Golem";
            MonsterLevels = new int[1];
            MonsterLevels[0] = 27;
        }
        if (ID == 42)
        {
            MonsterNames = new string[2];
            MonsterNames[0] = "Fire_Golem";
            MonsterNames[1] = "Fire_Golem";
            MonsterLevels = new int[2];
            MonsterLevels[0] = 30;
            MonsterLevels[1] = 30;
        }
        if (ID == 43)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Fire_Golem";
            MonsterNames[1] = "Fire_Golem";
            MonsterNames[2] = "Fire_Golem";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 30;
            MonsterLevels[1] = 30;
            MonsterLevels[2] = 30;
        }
        if (ID == 44)
        {
            MonsterNames = new string[6];
            MonsterNames[0] = "Fire_Golem";
            MonsterNames[1] = "Ice_Golem";
            MonsterNames[2] = "Stone_Golem";
            MonsterNames[3] = "Fire_Golem";
            MonsterNames[4] = "Ice_Golem";
            MonsterNames[5] = "Stone_Golem";
            MonsterLevels = new int[6];
            MonsterLevels[0] = 33;
            MonsterLevels[1] = 33;
            MonsterLevels[2] = 33;
            MonsterLevels[3] = 33;
            MonsterLevels[4] = 33;
            MonsterLevels[5] = 33;
        }
        if (ID == 45)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Stone_Golem";
            MonsterNames[1] = "Ice_Golem";
            MonsterNames[2] = "Fire_Golem";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 40;
            MonsterLevels[1] = 40;
            MonsterLevels[2] = 41;
        }
        if (ID == 46)
        {
            MonsterNames = new string[2];
            MonsterNames[0] = "Stone_Golem";
            MonsterNames[1] = "Stone_Golem";
            MonsterLevels = new int[2];
            MonsterLevels[0] = 25;
            MonsterLevels[1] = 25;
        }
        if (ID == 47)
        {
            MonsterNames = new string[2];
            MonsterNames[0] = "Stone_Golem";
            MonsterNames[1] = "Stone_Golem";
            MonsterLevels = new int[2];
            MonsterLevels[0] = 27;
            MonsterLevels[1] = 27;
        }
        if (ID == 48)
        {
            MonsterNames = new string[2];
            MonsterNames[0] = "Stone_Golem";
            MonsterNames[1] = "Stone_Golem";
            MonsterLevels = new int[2];
            MonsterLevels[0] = 27;
            MonsterLevels[1] = 27;
        }
        if (ID == 49)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Stone_Golem";
            MonsterNames[1] = "Stone_Golem";
             MonsterNames[2] = "Stone_Golem";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 28;
            MonsterLevels[1] = 28;
            MonsterLevels[2] = 28;
        }
        if (ID == 50)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Stone_Golem";
            MonsterNames[1] = "Stone_Golem";
            MonsterNames[2] = "Stone_Golem";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 30;
            MonsterLevels[1] = 30;
            MonsterLevels[2] = 30;
        }
        if (ID == 51)
        {
            MonsterNames = new string[4];
            MonsterNames[0] = "Stone_Golem";
            MonsterNames[1] = "Stone_Golem";
            MonsterNames[2] = "Stone_Golem";
            MonsterNames[3] = "Stone_Golem";
            MonsterLevels = new int[4];
            MonsterLevels[0] = 30;
            MonsterLevels[1] = 30;
            MonsterLevels[2] = 30;
            MonsterLevels[3] = 30;
        }
        if (ID == 52)
        {
            MonsterNames = new string[5];
            MonsterNames[0] = "Stone_Golem";
            MonsterNames[1] = "Stone_Golem";
            MonsterNames[2] = "Stone_Golem";
            MonsterNames[3] = "Stone_Golem";
            MonsterNames[4] = "Stone_Golem";
            MonsterLevels = new int[5];
            MonsterLevels[0] = 30;
            MonsterLevels[1] = 30;
            MonsterLevels[2] = 32;
            MonsterLevels[3] = 32;
            MonsterLevels[4] = 34;
        }
        if (ID == 53)
        {
            MonsterNames = new string[2];
            MonsterNames[0] = "Ice_Golem";
            MonsterNames[1] = "Ice_Golem";
            MonsterLevels = new int[2];
            MonsterLevels[0] = 25;
            MonsterLevels[1] = 25;
        }
        if (ID == 54)
        {
            MonsterNames = new string[2];
            MonsterNames[0] = "Ice_Golem";
            MonsterNames[1] = "Ice_Golem";
            MonsterLevels = new int[2];
            MonsterLevels[0] = 27;
            MonsterLevels[1] = 27;
        }
        if (ID == 55)
        {
            MonsterNames = new string[2];
            MonsterNames[0] = "Ice_Golem";
            MonsterNames[1] = "Ice_Golem";
            MonsterLevels = new int[2];
            MonsterLevels[0] = 27;
            MonsterLevels[1] = 27;
        }
        if (ID == 56)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Ice_Golem";
            MonsterNames[1] = "Ice_Golem";
            MonsterNames[2] = "Ice_Golem";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 28;
            MonsterLevels[1] = 28;
            MonsterLevels[2] = 28;
        }
        if (ID == 57)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Ice_Golem";
            MonsterNames[1] = "Ice_Golem";
            MonsterNames[2] = "Ice_Golem";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 30;
            MonsterLevels[1] = 30;
            MonsterLevels[2] = 30;
        }
        if (ID == 58)
        {
            MonsterNames = new string[4];
            MonsterNames[0] = "Ice_Golem";
            MonsterNames[1] = "Ice_Golem";
            MonsterNames[2] = "Ice_Golem";
            MonsterNames[3] = "Ice_Golem";
            MonsterLevels = new int[4];
            MonsterLevels[0] = 30;
            MonsterLevels[1] = 30;
            MonsterLevels[2] = 30;
            MonsterLevels[3] = 30;
        }
        if (ID == 59)
        {
            MonsterNames = new string[5];
            MonsterNames[0] = "Ice_Golem";
            MonsterNames[1] = "Ice_Golem";
            MonsterNames[2] = "Ice_Golem";
            MonsterNames[3] = "Ice_Golem";
            MonsterNames[4] = "Ice_Golem";
            MonsterLevels = new int[5];
            MonsterLevels[0] = 30;
            MonsterLevels[1] = 30;
            MonsterLevels[2] = 32;
            MonsterLevels[3] = 32;
            MonsterLevels[4] = 34;
        }
        if (ID == 60)
        {
            MonsterNames = new string[5];
            MonsterNames[0] = "Skeleton";
            MonsterNames[1] = "Skeleton";
            MonsterNames[2] = "Skeleton";
            MonsterNames[3] = "Skeleton";
            MonsterNames[4] = "Pirate_Skeleton";
            MonsterLevels = new int[5];
            MonsterLevels[0] = 35;
            MonsterLevels[1] = 35;
            MonsterLevels[2] = 35;
            MonsterLevels[3] = 35;
            MonsterLevels[4] = 40;
        }
        if (ID == 61)
        {
            MonsterNames = new string[5];
            MonsterNames[0] = "Skeleton";
            MonsterNames[1] = "Skeleton";
            MonsterNames[2] = "Pirate_Skeleton";
            MonsterNames[3] = "Pirate_Skeleton";
            MonsterNames[4] = "Wraith";
            MonsterLevels = new int[5];
            MonsterLevels[0] = 35;
            MonsterLevels[1] = 35;
            MonsterLevels[2] = 35;
            MonsterLevels[3] = 35;
            MonsterLevels[4] = 35;
        }
        if (ID == 62)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Skeleton";
            MonsterNames[1] = "Pirate_Skeleton";
            MonsterNames[2] = "Wraith";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 37;
            MonsterLevels[1] = 37;
            MonsterLevels[2] = 37;
        }
        if (ID == 63)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Wraith";
            MonsterNames[1] = "Wraith";
            MonsterNames[2] = "Wraith";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 40;
            MonsterLevels[1] = 40;
            MonsterLevels[2] = 40;
        }
        if (ID == 64)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Wraith";
            MonsterNames[1] = "Wraith";
            MonsterNames[2] = "Wraith";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 40;
            MonsterLevels[1] = 40;
            MonsterLevels[2] = 40;
        }
        if (ID == 65)
        {
            MonsterNames = new string[5];
            MonsterNames[0] = "Pirate_Skeleton";
            MonsterNames[1] = "Wraith";
            MonsterNames[2] = "Pirate_Skeleton";
            MonsterNames[3] = "Wraith";
            MonsterNames[4] = "Ghost_Wizard";
            MonsterLevels = new int[5];
            MonsterLevels[0] = 40;
            MonsterLevels[1] = 40;
            MonsterLevels[2] = 40;
            MonsterLevels[3] = 40;
            MonsterLevels[4] = 45;
        }
        if (ID == 66)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Wraith";
            MonsterNames[1] = "Wraith";
            MonsterNames[2] = "Ghost_Wizard";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 42;
            MonsterLevels[1] = 42;
            MonsterLevels[2] = 42;
        }
        if (ID == 67)
        {
            MonsterNames = new string[5];
            MonsterNames[0] = "Wraith";
            MonsterNames[1] = "Ghost_Wizard";
            MonsterNames[2] = "Ghost_Wizard";
            MonsterNames[3] = "Wraith";
            MonsterNames[4] = "Red_Rune_Wraith";
            MonsterLevels = new int[5];
            MonsterLevels[0] = 42;
            MonsterLevels[1] = 42;
            MonsterLevels[2] = 42;
            MonsterLevels[3] = 42;
            MonsterLevels[4] = 45;
        }
        if (ID == 68)
        {
            MonsterNames = new string[5];
            MonsterNames[0] = "Wraith";
            MonsterNames[1] = "Ghost_Wizard";
            MonsterNames[2] = "Ghost_Wizard";
            MonsterNames[3] = "Wraith";
            MonsterNames[4] = "Blue_Rune_Wraith";
            MonsterLevels = new int[5];
            MonsterLevels[0] = 42;
            MonsterLevels[1] = 42;
            MonsterLevels[2] = 42;
            MonsterLevels[3] = 42;
            MonsterLevels[4] = 45;
        }
        if (ID == 69)
        {
            MonsterNames = new string[2];
            MonsterNames[0] = "Red_Rune_Wraith";
            MonsterNames[1] = "Blue_Rune_Wraith";
            MonsterLevels = new int[2];
            MonsterLevels[0] = 50;
            MonsterLevels[1] = 50;
        }
        if (ID == 70)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Flower";
            MonsterNames[1] = "Flower";
            MonsterNames[2] = "Yeti";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 45;
            MonsterLevels[1] = 45;
            MonsterLevels[2] = 50;
        }
        if (ID == 71)
        {
            MonsterNames = new string[1];
            MonsterNames[0] = "Tree_Giant";
            MonsterLevels = new int[1];
            MonsterLevels[0] = 60;
        }
        if (ID == 72)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Flower";
            MonsterNames[1] = "Flower";
            MonsterNames[2] = "Tree_Giant";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 52;
            MonsterLevels[1] = 52;
            MonsterLevels[2] = 55;
        }
        if (ID == 73)
        {
            MonsterNames = new string[10];
            MonsterNames[0] = "Flower";
            MonsterNames[1] = "Flower";
            MonsterNames[2] = "Flower";
            MonsterNames[3] = "Yeti";
            MonsterNames[4] = "Yeti";
            MonsterNames[5] = "Tree_Giant";
            MonsterNames[6] = "Flower";
            MonsterNames[7] = "Tree_Giant";
            MonsterNames[8] = "Yeti";
            MonsterNames[9] = "Chain_Yeti";
            MonsterLevels = new int[10];
            MonsterLevels[0] = 55;
            MonsterLevels[1] = 55;
            MonsterLevels[2] = 55;
            MonsterLevels[3] = 55;
            MonsterLevels[4] = 55;
            MonsterLevels[5] = 55;
            MonsterLevels[6] = 57;
            MonsterLevels[7] = 57;
            MonsterLevels[8] = 57;
            MonsterLevels[9] = 60;
        }
        if (ID == 74)
        {
            MonsterNames = new string[5];
            MonsterNames[0] = "Salamander";
            MonsterNames[1] = "Salamander";
            MonsterNames[2] = "Slime";
            MonsterNames[3] = "Slime";
            MonsterNames[4] = "Yeti";
            MonsterLevels = new int[5];
            MonsterLevels[0] = 70;
            MonsterLevels[1] = 70;
            MonsterLevels[2] = 65;
            MonsterLevels[3] = 65;
            MonsterLevels[4] = 60;
        }
        if (ID == 75)
        {
            MonsterNames = new string[5];
            MonsterNames[0] = "Salamander";
            MonsterNames[1] = "Slime";
            MonsterNames[2] = "Fire_Wolf";
            MonsterNames[3] = "Lightning_Rabbit";
            MonsterNames[4] = "Yeti";
            MonsterLevels = new int[5];
            MonsterLevels[0] = 65;
            MonsterLevels[1] = 65;
            MonsterLevels[2] = 65;
            MonsterLevels[3] = 65;
            MonsterLevels[4] = 65;
        }
        if (ID == 76)
        {
            MonsterNames = new string[10];
            MonsterNames[0] = "Yeti";
            MonsterNames[1] = "Slime";
            MonsterNames[2] = "Fire_Wolf";
            MonsterNames[3] = "Lightning_Rabbit";
            MonsterNames[4] = "Salamander";
            MonsterNames[5] = "Salamander";
            MonsterNames[6] = "Lightning_Rabbit";
            MonsterNames[7] = "Fire_Wolf";
            MonsterNames[8] = "Slime";
            MonsterNames[9] = "Chain_Yeti";
            MonsterLevels = new int[10];
            MonsterLevels[0] = 65;
            MonsterLevels[1] = 65;
            MonsterLevels[2] = 65;
            MonsterLevels[3] = 65;
            MonsterLevels[4] = 65;
            MonsterLevels[5] = 65;
            MonsterLevels[6] = 65;
            MonsterLevels[7] = 65;
            MonsterLevels[8] = 65;
            MonsterLevels[9] = 70;
        }
        if (ID == 77)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Tree_Giant";
            MonsterNames[1] = "Yeti";
            MonsterNames[2] = "Chain_Yeti";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 70;
            MonsterLevels[1] = 70;
            MonsterLevels[2] = 70;
        }
        if (ID == 78)
        {
            MonsterNames = new string[1];
            MonsterNames[0] = "Rune_Crab";
            MonsterLevels = new int[1];
            MonsterLevels[0] = 75;
        }
        if (ID == 79)
        {
            MonsterNames = new string[2];
            MonsterNames[0] = "Rune_Crab";
            MonsterNames[1] = "Rune_Crab";
            MonsterLevels = new int[2];
            MonsterLevels[0] = 75;
            MonsterLevels[1] = 75;
        }
        if (ID == 80)
        {
            MonsterNames = new string[5];
            MonsterNames[0] = "Rune_Crab";
            MonsterNames[1] = "Rune_Crab";
            MonsterNames[2] = "Rune_Crab";
            MonsterNames[3] = "Rune_Crab";
            MonsterNames[4] = "Rune_Crab";
            MonsterLevels = new int[5];
            MonsterLevels[0] = 72;
            MonsterLevels[1] = 72;
            MonsterLevels[2] = 72;
            MonsterLevels[3] = 72;
            MonsterLevels[4] = 75;
        }
        if (ID == 81)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Rune_Crab";
            MonsterNames[1] = "Rune_Crab";
            MonsterNames[2] = "Boss_Crab";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 77;
            MonsterLevels[1] = 77;
            MonsterLevels[2] = 80;
        }
        if (ID == 82)
        {
            MonsterNames = new string[2];
            MonsterNames[0] = "Red_Block";
            MonsterNames[1] = "Red_Block";
            MonsterLevels = new int[2];
            MonsterLevels[0] = 82;
            MonsterLevels[1] = 82;
        }
        if (ID == 83)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Red_Block";
            MonsterNames[1] = "Red_Block";
            MonsterNames[2] = "Red_Block";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 83;
            MonsterLevels[1] = 83;
            MonsterLevels[2] = 83;
        }
        if (ID == 84)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Red_Block";
            MonsterNames[1] = "Red_Block";
            MonsterNames[2] = "Red_Block";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 85;
            MonsterLevels[1] = 85;
            MonsterLevels[2] = 85;
        }
        if (ID == 85)
        {
            MonsterNames = new string[5];
            MonsterNames[0] = "Salamangreat";
            MonsterNames[1] = "Salamangreat";
            MonsterNames[2] = "Giant_Slime";
            MonsterNames[3] = "Giant_Slime";
            MonsterNames[4] = "Fire_Dragon";
            MonsterLevels = new int[5];
            MonsterLevels[0] = 85;
            MonsterLevels[1] = 85;
            MonsterLevels[2] = 85;
            MonsterLevels[3] = 85;
            MonsterLevels[4] = 85;
        }
        if (ID == 86)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Pirate_King";
            MonsterNames[1] = "Pirate_King";
            MonsterNames[2] = "Fire_Dragon";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 86;
            MonsterLevels[1] = 86;
            MonsterLevels[2] = 86;
        }
        if (ID == 87)
        {
            MonsterNames = new string[2];
            MonsterNames[0] = "Fire_Dragon";
            MonsterNames[1] = "Beast_Yeti";
            MonsterLevels = new int[2];
            MonsterLevels[0] = 87;
            MonsterLevels[1] = 87;
        }
        if (ID == 88)
        {
            MonsterNames = new string[2];
            MonsterNames[0] = "Blue_Rune_Wraith";
            MonsterNames[1] = "Red_Rune_Wraith";
            MonsterLevels = new int[2];
            MonsterLevels[0] = 90;
            MonsterLevels[1] = 90;
        }
        if (ID == 89)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Fire_Dragon";
            MonsterNames[1] = "Fire_Dragon";
            MonsterNames[2] = "Boss_Crab";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 90;
            MonsterLevels[1] = 90;
            MonsterLevels[2] = 90;
        }
        if (ID == 90)
        {
            MonsterNames = new string[3];
            MonsterNames[0] = "Fire_Dragon";
            MonsterNames[1] = "Fire_Dragon";
            MonsterNames[2] = "Fire_Dragon";
            MonsterLevels = new int[3];
            MonsterLevels[0] = 90;
            MonsterLevels[1] = 90;
            MonsterLevels[2] = 92;
        }
        if (ID == 91)
        {
            MonsterNames = new string[10];
            MonsterNames[0] = "Ice_Golem";
            MonsterNames[1] = "Ice_Golem";
            MonsterNames[2] = "Ice_Golem";
            MonsterNames[3] = "Stone_Golem";
            MonsterNames[4] = "Stone_Golem";
            MonsterNames[5] = "Stone_Golem";
            MonsterNames[6] = "Fire_Golem";
            MonsterNames[7] = "Fire_Golem";
            MonsterNames[8] = "Fire_Golem";
            MonsterNames[9] = "Fire_Dragon";
            MonsterLevels = new int[10];
            MonsterLevels[0] = 92;
            MonsterLevels[1] = 92;
            MonsterLevels[2] = 92;
            MonsterLevels[3] = 92;
            MonsterLevels[4] = 92;
            MonsterLevels[5] = 92;
            MonsterLevels[6] = 92;
            MonsterLevels[7] = 92;
            MonsterLevels[8] = 92;
            MonsterLevels[9] = 93;
        }
        if (ID == 92)
        {
            MonsterNames = new string[12];
            MonsterNames[0] = "Fire_Dragon";
            MonsterNames[1] = "Fire_Wolf";
            MonsterNames[2] = "Lightning_Rabbit";
            MonsterNames[3] = "Lightning_Rabbit";
            MonsterNames[4] = "Yeti";
            MonsterNames[5] = "Chain_Yeti";
            MonsterNames[6] = "Fire_Wolf";
            MonsterNames[7] = "Yeti";
            MonsterNames[8] = "Yeti";
            MonsterNames[9] = "Tree_Giant";
            MonsterNames[10] = "Chain_Yeti";
            MonsterNames[11] = "Fire_Dragon";
            MonsterLevels = new int[12];
            MonsterLevels[0] = 90;
            MonsterLevels[1] = 90;
            MonsterLevels[2] = 90;
            MonsterLevels[3] = 90;
            MonsterLevels[4] = 92;
            MonsterLevels[5] = 92;
            MonsterLevels[6] = 92;
            MonsterLevels[7] = 92;
            MonsterLevels[8] = 94;
            MonsterLevels[9] = 94;
            MonsterLevels[10] = 94;
            MonsterLevels[11] = 95;
        }
        if (ID == 93)
        {
            MonsterNames = new string[5];
            MonsterNames[0] = "Rune_Crab";
            MonsterNames[1] = "Rune_Crab";
            MonsterNames[2] = "Red_Block";
            MonsterNames[3] = "Red_Block";
            MonsterNames[4] = "Fire_Dragon";
            MonsterLevels = new int[5];
            MonsterLevels[0] = 95;
            MonsterLevels[1] = 95;
            MonsterLevels[2] = 95;
            MonsterLevels[3] = 95;
            MonsterLevels[4] = 97;
        }
        if (ID == 94)
        {
            MonsterNames = new string[20];
            MonsterNames[0] = "Salamander";
            MonsterNames[1] = "Slime";
            MonsterNames[2] = "Yeti";
            MonsterNames[3] = "Skeleton";
            MonsterNames[4] = "Wraith";
            MonsterNames[5] = "Floating_Ice";
            MonsterNames[6] = "Lightning_Rabbit";
            MonsterNames[7] = "Fire_Wolf";
            MonsterNames[8] = "Ghost_Wizard";
            MonsterNames[9] = "Tree_Giant"; 
            MonsterNames[10] = "Chain_Yeti";
            MonsterNames[11] = "Ice_Golem";
            MonsterNames[12] = "Stone_Golem";
            MonsterNames[13] = "Fire_Golem";
            MonsterNames[14] = "Pirate_Skeleton";
            MonsterNames[15] = "Poison_Bat";
            MonsterNames[16] = "Flower";
            MonsterNames[17] = "Rune_Crab";
            MonsterNames[18] = "Red_Block";
            MonsterNames[19] = "Fire_dragon";
            MonsterLevels = new int[20];
            MonsterLevels[0] = 97;
            MonsterLevels[1] = 97;
            MonsterLevels[2] = 97;
            MonsterLevels[3] = 97;
            MonsterLevels[4] = 97;
            MonsterLevels[5] = 97;
            MonsterLevels[6] = 97;
            MonsterLevels[7] = 97;
            MonsterLevels[8] = 97;
            MonsterLevels[9] = 97;
            MonsterLevels[10] = 97;
            MonsterLevels[11] = 97;
            MonsterLevels[12] = 97;
            MonsterLevels[13] = 97;
            MonsterLevels[14] = 97;
            MonsterLevels[15] = 97;
            MonsterLevels[16] = 97;
            MonsterLevels[17] = 97;
            MonsterLevels[18] = 97;
            MonsterLevels[19] = 97;
        }
        if (ID == 95)
        {
            MonsterNames = new string[7];
            MonsterNames[0] = "Salamangreat";
            MonsterNames[1] = "Giant_Slime";
            MonsterNames[2] = "Pirate_King";
            MonsterNames[3] = "Beast_Yeti";
            MonsterNames[4] = "Red_Rune_Wraith";
            MonsterNames[5] = "Blue_Rune_Wraith";
            MonsterNames[6] = "Boss_Crab";
            MonsterLevels = new int[7];
            MonsterLevels[0] = 99;
            MonsterLevels[1] = 99;
            MonsterLevels[2] = 99;
            MonsterLevels[3] = 99;
            MonsterLevels[4] = 99;
            MonsterLevels[5] = 99;
            MonsterLevels[6] = 99;
        }
        if (ID == 96)
        {
            MonsterNames = new string[28];
            MonsterNames[0] = "Salamander";
            MonsterNames[1] = "Slime";
            MonsterNames[2] = "Yeti";
            MonsterNames[3] = "Skeleton";
            MonsterNames[4] = "Wraith";
            MonsterNames[5] = "Floating_Ice";
            MonsterNames[6] = "Lightning_Rabbit";
            MonsterNames[7] = "Fire_Wolf";
            MonsterNames[8] = "Ghost_Wizard";
            MonsterNames[9] = "Tree_Giant";
            MonsterNames[10] = "Chain_Yeti";
            MonsterNames[11] = "Ice_Golem";
            MonsterNames[12] = "Stone_Golem";
            MonsterNames[13] = "Fire_Golem";
            MonsterNames[14] = "Pirate_Skeleton";
            MonsterNames[15] = "Poison_Bat";
            MonsterNames[16] = "Flower";
            MonsterNames[17] = "Rune_Crab";
            MonsterNames[18] = "Red_Block";
            MonsterNames[19] = "Fire_dragon";
            MonsterNames[20] = "Salamangreat";
            MonsterNames[21] = "Giant_Slime";
            MonsterNames[22] = "Pirate_King";
            MonsterNames[23] = "Beast_Yeti";
            MonsterNames[24] = "Red_Rune_Wraith";
            MonsterNames[25] = "Blue_Rune_Wraith";
            MonsterNames[26] = "Boss_Crab";
            MonsterNames[27] = "Black_Dragon";
            MonsterLevels = new int[28];
            MonsterLevels[0] = 100;
            MonsterLevels[1] = 100;
            MonsterLevels[2] = 100;
            MonsterLevels[3] = 100;
            MonsterLevels[4] = 100;
            MonsterLevels[5] = 100;
            MonsterLevels[6] = 100;
            MonsterLevels[7] = 100;
            MonsterLevels[8] = 100;
            MonsterLevels[9] = 100;
            MonsterLevels[10] = 100;
            MonsterLevels[11] = 100;
            MonsterLevels[12] = 100;
            MonsterLevels[13] = 100;
            MonsterLevels[14] = 100;
            MonsterLevels[15] = 100;
            MonsterLevels[16] = 100;
            MonsterLevels[17] = 100;
            MonsterLevels[18] = 100;
            MonsterLevels[19] = 100;
            MonsterLevels[20] = 100;
            MonsterLevels[21] = 100;
            MonsterLevels[22] = 100;
            MonsterLevels[23] = 100;
            MonsterLevels[24] = 100;
            MonsterLevels[25] = 100;
            MonsterLevels[26] = 100;
            MonsterLevels[27] = 100;
        }
        NextMonster();
    }

    public void NextMonster()
    {
        if (currentMonster < MonsterNames.Length)
        {
            monsterObject.GetComponent<MonsterScript>().SpawnMonster(MonsterNames[currentMonster], MonsterLevels[currentMonster]);
        }
        else { LevelButtons[LevelID - 1].GetComponent<Button>().interactable = false; LevelButtons[LevelID - 1].GetComponent<QuestButtonScript>().UnlockLevels(); StartCoroutine(monsterObject.GetComponent<MonsterScript>().AfterBattleReport()); AfterBattleReport.SetActive(true); MonsterNames = new string[0]; InBattle = false; }
        currentMonster += 1;
    }

    public void UpdateShop()
    {

    }

    public void SetMove()
    {

    }

    public void Inventory(int x)
    {
        if (x == 1) { playerObject.GetComponent<PlayerScript>().transform.position = new Vector3(-75, -20, 0); }
        else if (x == 2) { playerObject.GetComponent<PlayerScript>().transform.position = new Vector3(0, -30, 0); }
        else if (x == 3) { monsterObject.SetActive(false); playerObject.GetComponent<PlayerScript>().transform.position = new Vector3(-75, -20, 0); }
        else if (x == 4) { if (InBattle) { monsterObject.SetActive(true); } playerObject.GetComponent<PlayerScript>().transform.position = new Vector3(0, -30, 0); }
    }

    public void InitializeLevelButtons()
    {
        LevelSelectObject.SetActive(true);
        GameObject LevelContainer = GameObject.Find("PlayableLevels");
        //int children = LevelContainer.transform.childCount;
        LevelButtons = new GameObject[LevelContainer.transform.childCount];

        for (int i = 0; i < LevelButtons.Length; i++)
        {
            LevelButtons[i] = LevelContainer.transform.GetChild(i).gameObject;
        }
        LevelSelectObject.SetActive(false);
    }
}
