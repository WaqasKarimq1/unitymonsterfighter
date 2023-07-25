using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySystemScript : MonoBehaviour
{
    public GameObject[] InventorySlots = new GameObject[15];

    public GameObject MoveSelectionObject;

    public int EquippedWeapon = 0;
    public int EquippedShield = 0;

    public GameObject GameControllerObject;
    public GameObject playerObject;
    public Sprite Background;

    public string Name = "";
    public Sprite Icon;
    public Image InfoPic;
    public TextMeshProUGUI ItemNameText;
    public TextMeshProUGUI ItemEffectText;
    public TextMeshProUGUI ItemCostText;
    public GameObject BuyButton;
    public GameObject EquipUseButton;
    public TextMeshProUGUI EquipUseText;
    public bool ConsumableItem = false;
    public string Type = "";
    public float Cost = 0f;
    public int SlotSelected = 0;

    public Sprite[] ItemIcons = new Sprite[25];
    // Start is called before the first frame update
    void Start()
    {
        CreateInventory();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameControllerObject.GetComponent<GameController>().InBattle) { BuyButton.SetActive(false); }
    }

    public void AddItem(string Name, Sprite Icon)
    {
        int x = 0;
        while ((InventorySlots[x].GetComponent<ItemSlotScript>().Name != null && InventorySlots[x].GetComponent<ItemSlotScript>().Name != "NULL") && x < InventorySlots.Length - 1){ x += 1;}
        if (InventorySlots[x].GetComponent<ItemSlotScript>().Name == null || InventorySlots[x].GetComponent<ItemSlotScript>().Name == "NULL") {
            InventorySlots[x].GetComponent<ItemSlotScript>().Name = Name;
            InventorySlots[x].transform.Find("Item_Icon").GetComponent<Image>().sprite = Icon;
            InventorySlots[x].transform.Find("Item_Icon").gameObject.SetActive(true);
            Debug.Log("Item In Inventory");
        }
        
        //if(Name == "Small_Health_Potion") { InventorySlots[x].transform.Find("Item_Icon").GetComponent<Image>().sprite = ItemIcons[0]; }
        //if (Name == "Medium_Health_Potion") { InventorySlots[x].transform.Find("Item_Icon").GetComponent<Image>().sprite = ItemIcons[1]; }
        //if (Name == "Large_Health_Potion") { InventorySlots[x].transform.Find("Item_Icon").GetComponent<Image>().sprite = ItemIcons[2]; }
    }

    public void Sell()
    {
        InventorySlots[SlotSelected].GetComponent<ItemSlotScript>().Name = null;
        InventorySlots[SlotSelected].GetComponent<ItemSlotScript>().Icon = Background;
        InventorySlots[SlotSelected].transform.Find("Item_Icon").GetComponent<Image>().sprite = Background;
        GameControllerObject.GetComponent<GameController>().gold += (Cost / 2);
        ItemNameText.text = "";
        ItemEffectText.text = "";
        ItemCostText.text = "";
        BuyButton.SetActive(false);
        EquipUseButton.SetActive(false);
        EquipUseText.text = "";
    }

    public void UseOrEquipItem()
    {
        if (Name == "Small_Health_Potion")
        {
            playerObject.GetComponent<PlayerScript>().hitpoints += 100;
        }
        if (Name == "Medium_Health_Potion")
        {
            playerObject.GetComponent<PlayerScript>().hitpoints += 250;
        }
        if (Name == "Large_Health_Potion")
        {
            playerObject.GetComponent<PlayerScript>().hitpoints += 500;
        }

        //SkillBooks
        if (Name == "SKILL_BOOK_Take_Down") { playerObject.GetComponent<PlayerScript>().MovesUnlocked[2] = true; }
        if (Name == "SKILL_BOOK_Shield_Bash") { playerObject.GetComponent<PlayerScript>().MovesUnlocked[3] = true; }
        if (Name == "SKILL_BOOK_Giga_Strike") { playerObject.GetComponent<PlayerScript>().MovesUnlocked[4] = true; }
        if (Name == "SKILL_BOOK_Fire_Ball") { playerObject.GetComponent<PlayerScript>().MovesUnlocked[5] = true; }
        if (Name == "SKILL_BOOK_Flamethrower") { playerObject.GetComponent<PlayerScript>().MovesUnlocked[6] = true; }
        if (Name == "SKILL_BOOK_Explosion") { playerObject.GetComponent<PlayerScript>().MovesUnlocked[7] = true; }
        if (Name == "SKILL_BOOK_Ignite") { playerObject.GetComponent<PlayerScript>().MovesUnlocked[8] = true; }
        if (Name == "SKILL_BOOK_Solar_Flare") { playerObject.GetComponent<PlayerScript>().MovesUnlocked[9] = true; }
        if (Name == "SKILL_BOOK_Spark") { playerObject.GetComponent<PlayerScript>().MovesUnlocked[10] = true; }
        if (Name == "SKILL_BOOK_Stun_Shot") { playerObject.GetComponent<PlayerScript>().MovesUnlocked[11] = true; }
        if (Name == "SKILL_BOOK_Bolt") { playerObject.GetComponent<PlayerScript>().MovesUnlocked[12] = true; }
        if (Name == "SKILL_BOOK_Electrocution") { playerObject.GetComponent<PlayerScript>().MovesUnlocked[13] = true; }
        if (Name == "SKILL_BOOK_Lightning_Strike") { playerObject.GetComponent<PlayerScript>().MovesUnlocked[14] = true; }
        if (Name == "SKILL_BOOK_Icicle_Throw") { playerObject.GetComponent<PlayerScript>().MovesUnlocked[15] = true; }
        if (Name == "SKILL_BOOK_Ice_Ball") { playerObject.GetComponent<PlayerScript>().MovesUnlocked[16] = true; }
        if (Name == "SKILL_BOOK_Zero") { playerObject.GetComponent<PlayerScript>().MovesUnlocked[17] = true; }
        if (Name == "SKILL_BOOK_Ice_Spike") { playerObject.GetComponent<PlayerScript>().MovesUnlocked[18] = true; }
        if (Name == "SKILL_BOOK_Blizzard") { playerObject.GetComponent<PlayerScript>().MovesUnlocked[19] = true; }
        if (Name == "SKILL_BOOK_Poison_Dart") { playerObject.GetComponent<PlayerScript>().MovesUnlocked[20] = true; }
        if (Name == "SKILL_BOOK_Numbing_Gas") { playerObject.GetComponent<PlayerScript>().MovesUnlocked[21] = true; }
        if (Name == "SKILL_BOOK_Poison_Gas") { playerObject.GetComponent<PlayerScript>().MovesUnlocked[22] = true; }
        if (Name == "SKILL_BOOK_Venom_Bite") { playerObject.GetComponent<PlayerScript>().MovesUnlocked[23] = true; }
        if (Name == "SKILL_BOOK_Radiation") { playerObject.GetComponent<PlayerScript>().MovesUnlocked[24] = true; }

        //Weapons
        if (Name == "Rusty_Sword")
        {
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].HitpointsIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalAttackIncrease = 1;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicAttackIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].SpeedIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].sprite = ItemIcons[0];
            InventorySlots[EquippedWeapon].transform.Find("EquipHighlight").gameObject.SetActive(false);
            EquippedWeapon = SlotSelected;
        }
        if (Name == "Basic_Sword")
        {
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].HitpointsIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalAttackIncrease = 2;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicAttackIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].SpeedIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].sprite = ItemIcons[1];
            InventorySlots[EquippedWeapon].transform.Find("EquipHighlight").gameObject.SetActive(false);
            EquippedWeapon = SlotSelected;
        }
        if (Name == "Iron_Sword")
        {
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].HitpointsIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalAttackIncrease = 3;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicAttackIncrease = 1;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].SpeedIncrease = 1;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].sprite = ItemIcons[2];
            InventorySlots[EquippedWeapon].transform.Find("EquipHighlight").gameObject.SetActive(false);
            EquippedWeapon = SlotSelected;
        }
        if (Name == "Katana")
        {
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].HitpointsIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalAttackIncrease = 5;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicAttackIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].SpeedIncrease = 5;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].sprite = ItemIcons[3];
            InventorySlots[EquippedWeapon].transform.Find("EquipHighlight").gameObject.SetActive(false);
            EquippedWeapon = SlotSelected;
        }
        if (Name == "Knight_Sword")
        {
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].HitpointsIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalAttackIncrease = 7;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicAttackIncrease = 7;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].SpeedIncrease = 2;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].sprite = ItemIcons[4];
            InventorySlots[EquippedWeapon].transform.Find("EquipHighlight").gameObject.SetActive(false);
            EquippedWeapon = SlotSelected;
        }
        if (Name == "Staff")
        {
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].HitpointsIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalAttackIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicAttackIncrease = 3;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].SpeedIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].sprite = ItemIcons[5];
            InventorySlots[EquippedWeapon].transform.Find("EquipHighlight").gameObject.SetActive(false);
            EquippedWeapon = SlotSelected;
        }
        if (Name == "Druid_Wand")
        {
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].HitpointsIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalAttackIncrease = 2;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicAttackIncrease = 5;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].SpeedIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].sprite = ItemIcons[6];
            InventorySlots[EquippedWeapon].transform.Find("EquipHighlight").gameObject.SetActive(false);
            EquippedWeapon = SlotSelected;
        }
        if (Name == "Golden_Staff")
        {
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].HitpointsIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalAttackIncrease = 3;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicAttackIncrease = 9;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].SpeedIncrease = -5;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].sprite = ItemIcons[7];
            InventorySlots[EquippedWeapon].transform.Find("EquipHighlight").gameObject.SetActive(false);
            EquippedWeapon = SlotSelected;
        }
        if (Name == "Steel_Axe")
        {
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].HitpointsIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalAttackIncrease = 15;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicAttackIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].SpeedIncrease = -5;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].sprite = ItemIcons[8];
            InventorySlots[EquippedWeapon].transform.Find("EquipHighlight").gameObject.SetActive(false);
            EquippedWeapon = SlotSelected;
        }
        if (Name == "Kings_Blade")
        {
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].HitpointsIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalAttackIncrease = 10;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicAttackIncrease = 10;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].SpeedIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].sprite = ItemIcons[9];
            InventorySlots[EquippedWeapon].transform.Find("EquipHighlight").gameObject.SetActive(false);
            EquippedWeapon = SlotSelected;
        }
        if (Name == "Steel_Mace")
        {
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].HitpointsIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalAttackIncrease = 11;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicAttackIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].SpeedIncrease = 5;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].sprite = ItemIcons[10];
            InventorySlots[EquippedWeapon].transform.Find("EquipHighlight").gameObject.SetActive(false);
            EquippedWeapon = SlotSelected;
        }
        if (Name == "Stone_Hammer")
        {
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].HitpointsIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalAttackIncrease = 20;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicAttackIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].SpeedIncrease = -10;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].sprite = ItemIcons[11];
            InventorySlots[EquippedWeapon].transform.Find("EquipHighlight").gameObject.SetActive(false);
            EquippedWeapon = SlotSelected;
        }
        if (Name == "War_Hammer")
        {
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].HitpointsIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalAttackIncrease = 30;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicAttackIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].SpeedIncrease = -15;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].sprite = ItemIcons[12];
            InventorySlots[EquippedWeapon].transform.Find("EquipHighlight").gameObject.SetActive(false);
            EquippedWeapon = SlotSelected;
        }
        if (Name == "Magic_Blade")
        {
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].HitpointsIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalAttackIncrease = 50;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicAttackIncrease = 50;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].SpeedIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].sprite = ItemIcons[13];
            InventorySlots[EquippedWeapon].transform.Find("EquipHighlight").gameObject.SetActive(false);
            EquippedWeapon = SlotSelected;
        }
        if (Name == "Sword_Of_Heroes")
        {
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].HitpointsIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalAttackIncrease = 40;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].PhysicalDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicAttackIncrease = 40;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].MagicDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].SpeedIncrease = 40;
            playerObject.GetComponent<PlayerScript>().Equipment["Sword"].sprite = ItemIcons[14];
            InventorySlots[EquippedWeapon].transform.Find("EquipHighlight").gameObject.SetActive(false);
            EquippedWeapon = SlotSelected;
        }
        

        //Shields
        if (Name == "Wooden_Shield")
        {
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].HitpointsIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].PhysicalAttackIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].PhysicalDefenceIncrease = 1;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].MagicAttackIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].MagicDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].SpeedIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].sprite = ItemIcons[15];
            InventorySlots[EquippedShield].transform.Find("EquipHighlight").gameObject.SetActive(false);
            EquippedShield = SlotSelected;
        }
        if (Name == "Basic_Shield")
        {
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].HitpointsIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].PhysicalAttackIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].PhysicalDefenceIncrease = 3;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].MagicAttackIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].MagicDefenceIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].SpeedIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].sprite = ItemIcons[16];
            InventorySlots[EquippedShield].transform.Find("EquipHighlight").gameObject.SetActive(false);
            EquippedShield = SlotSelected;
        }
        if (Name == "Iron_Shield")
        {
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].HitpointsIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].PhysicalAttackIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].PhysicalDefenceIncrease = 3;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].MagicAttackIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].MagicDefenceIncrease = 3;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].SpeedIncrease = 1;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].sprite = ItemIcons[17];
            InventorySlots[EquippedShield].transform.Find("EquipHighlight").gameObject.SetActive(false);
            EquippedShield = SlotSelected;
        }
        if (Name == "Magic_Shield")
        {
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].HitpointsIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].PhysicalAttackIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].PhysicalDefenceIncrease = 3;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].MagicAttackIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].MagicDefenceIncrease = 10;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].SpeedIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].sprite = ItemIcons[18];
            InventorySlots[EquippedShield].transform.Find("EquipHighlight").gameObject.SetActive(false);
            EquippedShield = SlotSelected;
        }
        if (Name == "Knight_Shield")
        {
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].HitpointsIncrease = 3;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].PhysicalAttackIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].PhysicalDefenceIncrease = 7;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].MagicAttackIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].MagicDefenceIncrease = 7;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].SpeedIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].sprite = ItemIcons[19];
            InventorySlots[EquippedShield].transform.Find("EquipHighlight").gameObject.SetActive(false);
            EquippedShield = SlotSelected;
        }
        if (Name == "Heavy_Shield")
        {
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].HitpointsIncrease = 5;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].PhysicalAttackIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].PhysicalDefenceIncrease = 15;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].MagicAttackIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].MagicDefenceIncrease = 5;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].SpeedIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].sprite = ItemIcons[20];
            InventorySlots[EquippedShield].transform.Find("EquipHighlight").gameObject.SetActive(false);
            EquippedShield = SlotSelected;
        }
        if (Name == "Dark_Knight_Shield")
        {
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].HitpointsIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].PhysicalAttackIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].PhysicalDefenceIncrease = 12;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].MagicAttackIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].MagicDefenceIncrease = 12;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].SpeedIncrease = 10;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].sprite = ItemIcons[21];
            InventorySlots[EquippedShield].transform.Find("EquipHighlight").gameObject.SetActive(false);
            EquippedShield = SlotSelected;
        }
        if (Name == "Gem_Shield")
        {
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].HitpointsIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].PhysicalAttackIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].PhysicalDefenceIncrease = 5;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].MagicAttackIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].MagicDefenceIncrease = 25;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].SpeedIncrease = -5;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].sprite = ItemIcons[22];
            InventorySlots[EquippedShield].transform.Find("EquipHighlight").gameObject.SetActive(false);
            EquippedShield = SlotSelected;
        }
        if (Name == "All_Purpose_Defense")
        {
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].HitpointsIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].PhysicalAttackIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].PhysicalDefenceIncrease = 50;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].MagicAttackIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].MagicDefenceIncrease = 50;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].SpeedIncrease = -20;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].sprite = ItemIcons[23];
            InventorySlots[EquippedShield].transform.Find("EquipHighlight").gameObject.SetActive(false);
            EquippedShield = SlotSelected;
        }
        if (Name == "Heroes_Shield")
        {
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].HitpointsIncrease = 30;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].PhysicalAttackIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].PhysicalDefenceIncrease = 35;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].MagicAttackIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].MagicDefenceIncrease = 35;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].SpeedIncrease = 0;
            playerObject.GetComponent<PlayerScript>().Equipment["Shield"].sprite = ItemIcons[24];
            InventorySlots[EquippedShield].transform.Find("EquipHighlight").gameObject.SetActive(false);
            EquippedShield = SlotSelected;
        }

        InventorySlots[SlotSelected].transform.Find("EquipHighlight").gameObject.SetActive(true);

        if (ConsumableItem)
        {
            InventorySlots[SlotSelected].GetComponent<ItemSlotScript>().Name = "NULL";
            InventorySlots[SlotSelected].transform.Find("Item_Icon").gameObject.SetActive(false);
            InventorySlots[SlotSelected].transform.Find("Item_Icon").GetComponent<Image>().sprite = Background;
            ItemNameText.text = "";
            ItemEffectText.text = "";
            ItemCostText.text = "";
            BuyButton.SetActive(false);
            EquipUseButton.SetActive(false);
            EquipUseText.text = "";
            InventorySlots[SlotSelected].transform.Find("EquipHighlight").gameObject.SetActive(false);
        }
        playerObject.GetComponent<PlayerScript>().Equips();

        if (GameControllerObject.GetComponent<GameController>().InBattle) { playerObject.GetComponent<PlayerScript>().DealDamage(0); GameControllerObject.GetComponent<GameController>().Inventory(4); gameObject.SetActive(false); }
        //for (int i = 0; i < InventorySlots.Length; i++) { InventorySlots[i].transform.Find("EquipHighlight").gameObject.SetActive(false); }
        
        
    }


    public void ChangeInfo(string NameOfItem, Sprite sprite)
    {
        BuyButton.SetActive(true);
        EquipUseButton.SetActive(true);
        if(NameOfItem == "Small_Health_Potion" || NameOfItem == "Medium_Health_Potion" || NameOfItem == "Large_Health_Potion")
        {
            ConsumableItem = true;
            EquipUseText.text = "USE";
        }
        else { EquipUseText.text = "EQUIP"; ConsumableItem = false; }
        //Potions
        if (NameOfItem == "Small_Health_Potion")
        {
            Name = NameOfItem;
            ItemNameText.text = "Small Health Potion";
            ItemEffectText.text = "Heals player by 100 points.";
            ItemCostText.text = "100 gold";
            Cost = 100f;
            ConsumableItem = true;
            EquipUseText.text = "USE";
        }
        else if (NameOfItem == "Medium_Health_Potion")
        {
            Name = NameOfItem;
            ItemNameText.text = "Medium Health Potion";
            ItemEffectText.text = "Heals player by 1000 points.";
            ItemCostText.text = "1 000 gold";
            Cost = 1000f;
            ConsumableItem = true;
            EquipUseText.text = "USE";
        }
        else if (NameOfItem == "Large_Health_Potion")
        {
            Name = NameOfItem;
            ItemNameText.text = "Large Health Potion";
            ItemEffectText.text = "Heals player by 5000 points.";
            ItemCostText.text = "5 000 gold";
            Cost = 5000f;
            ConsumableItem = true;
            EquipUseText.text = "USE";
        }

        //Weapons
        else if (NameOfItem == "Rusty_Sword")
        {
            Name = NameOfItem;
            ItemNameText.text = "Rusty Sword";
            ItemEffectText.text = "+1 Physical Attack";
            ItemCostText.text = "100 gold";
            Cost = 100f;
        }
        else if (NameOfItem == "Basic_Sword")
        {
            Name = NameOfItem;
            ItemNameText.text = "Basic Sword";
            ItemEffectText.text = "+2 Physical Attack";
            ItemCostText.text = "500 gold";
            Cost = 500f;
        }
        else if (NameOfItem == "Iron_Sword")
        {
            Name = NameOfItem;
            ItemNameText.text = "Iron Sword";
            ItemEffectText.text = "+3 Physical Attack\n +1 Magic Attack\n +1 Speed";
            ItemCostText.text = "1 000 gold";
            Cost = 1000f;
        }
        else if (NameOfItem == "Katana")
        {
            Name = NameOfItem;
            ItemNameText.text = "Katana";
            ItemEffectText.text = "+5 Physical Attack\n +5 Speed";
            ItemCostText.text = "2 500 gold";
            Cost = 2500f;
        }
        else if (NameOfItem == "Knight_Sword")
        {
            Name = NameOfItem;
            ItemNameText.text = "Knights Sword";
            ItemEffectText.text = "+7 Physical Attack\n +7 Magic Attack\n +2 Speed";
            ItemCostText.text = "5 000 gold";
            Cost = 5000f;
        }
        else if (NameOfItem == "Staff")
        {
            Name = NameOfItem;
            ItemNameText.text = "Staff";
            ItemEffectText.text = "+3 Magic Attack";
            ItemCostText.text = "1 000 gold";
            Cost = 1000f;
        }
        else if (NameOfItem == "Druid_Wand")
        {
            Name = NameOfItem;
            ItemNameText.text = "Druid Wand";
            ItemEffectText.text = "+5 Magic Attack\n +2 Physical Attack";
            ItemCostText.text = "2 000 gold";
            Cost = 2000f;
        }
        else if (NameOfItem == "Golden_Staff")
        {
            Name = NameOfItem;
            ItemNameText.text = "Golden Staff";
            ItemEffectText.text = "+9 Magic Attack\n +3 Physical Attack\n -5 Speed";
            ItemCostText.text = "3 500 gold";
            Cost = 3500f;
        }
        else if (NameOfItem == "Steel_Axe")
        {
            Name = NameOfItem;
            ItemNameText.text = "Steel Axe";
            ItemEffectText.text = "+15 Physical Attack\n -5 Speed";
            ItemCostText.text = "7 500 gold";
            Cost = 7500f;
        }
        else if (NameOfItem == "Kings_Blade")
        {
            Name = NameOfItem;
            ItemNameText.text = "Kings Blade";
            ItemEffectText.text = "+10 Physical Attack\n +10 Magic Attack";
            ItemCostText.text = "20 000 gold";
            Cost = 20000f;
        }
        else if (NameOfItem == "Steel_Mace")
        {
            Name = NameOfItem;
            ItemNameText.text = "Steel Mace";
            ItemEffectText.text = "+11 Physical Attack\n +5 Speed";
            ItemCostText.text = "10 000 gold";
            Cost = 10000;
        }
        else if (NameOfItem == "Stone_Hammer")
        {
            Name = NameOfItem;
            ItemNameText.text = "Stone Hammer";
            ItemEffectText.text = "+20 Physical Attack\n -10 Speed";
            ItemCostText.text = "25 000 gold";
            Cost = 25000f;
        }
        else if (NameOfItem == "War_Hammer")
        {
            Name = NameOfItem;
            ItemNameText.text = "Basic Sword";
            ItemEffectText.text = "+30 Physical Attack\n -15 Speed";
            ItemCostText.text = "50 000 gold";
            Cost = 50000f;
        }
        else if (NameOfItem == "Magic_Blade")
        {
            Name = NameOfItem;
            ItemNameText.text = "Magic Blade";
            ItemEffectText.text = "+50 Physical Attack\n +50 Magic Attack";
            ItemCostText.text = "400 000 gold";
            Cost = 400000;
        }
        else if (NameOfItem == "Sword_Of_Heroes")
        {
            Name = NameOfItem;
            ItemNameText.text = "Sword Of Heroes";
            ItemEffectText.text = "+40 Physical Attack\n +40 Magic Attack\n +40 Speed";
            ItemCostText.text = "1 000 000 gold";
            Cost = 1000000;
        }

        //Shields
        else if (NameOfItem == "Wooden_Shield")
        {
            Name = NameOfItem;
            ItemNameText.text = "Wooden Shield";
            ItemEffectText.text = "+1 Physical Defence";
            ItemCostText.text = "100 gold";
            Cost = 100f;
        }
        else if (NameOfItem == "Basic_Shield")
        {
            Name = NameOfItem;
            ItemNameText.text = "Basic SHield";
            ItemEffectText.text = "+3 Physical Defence";
            ItemCostText.text = "500 gold";
            Cost = 500f;
        }
        else if (NameOfItem == "Iron_Shield")
        {
            Name = NameOfItem;
            ItemNameText.text = "Iron Shield";
            ItemEffectText.text = "+3 Physical Defence\n +3 Magic Defence\n +1 Speed";
            ItemCostText.text = "1 000 gold";
            Cost = 1000f;
        }
        else if (NameOfItem == "Magic_Shield")
        {
            Name = NameOfItem;
            ItemNameText.text = "Magic Shield";
            ItemEffectText.text = "+3 Physical Defence\n +10 Magic Defence";
            ItemCostText.text = "4 500 gold";
            Cost = 4500f;
        }
        else if (NameOfItem == "Knight_Shield")
        {
            Name = NameOfItem;
            ItemNameText.text = "Knights Shield";
            ItemEffectText.text = "+7 Physical Defence\n +7 Magic Defence\n +3 Hitpoints";
            ItemCostText.text = "10 000 gold";
            Cost = 10000f;
        }
        else if (NameOfItem == "Heavy_Shield")
        {
            Name = NameOfItem;
            ItemNameText.text = "Heavy Shield";
            ItemEffectText.text = "+15 Physical Defence\n +5 Magic Defence\n +5 Hitpoints";
            ItemCostText.text = "20 000 gold";
            Cost = 20000f;
        }
        else if (NameOfItem == "Dark_Knight_Shield")
        {
            Name = NameOfItem;
            ItemNameText.text = "Dark Knight Shield";
            ItemEffectText.text = "+12 Physical Defence\n +12 Magic Defence\n +10 Hitpoints";
            ItemCostText.text = "75 000 gold";
            Cost = 75000f;
        }
        else if (NameOfItem == "Gem_Shield")
        {
            Name = NameOfItem;
            ItemNameText.text = "Gem Shield";
            ItemEffectText.text = "+5 Physical Defence\n +25 Magical Defence\n -5 Speed";
            ItemCostText.text = "200 000 gold";
            Cost = 200000f;
        }
        else if (NameOfItem == "All_Purpose_Defense")
        {
            Name = NameOfItem;
            ItemNameText.text = "All Purpose Shield";
            ItemEffectText.text = "+50 Physical Defence\n +50 Magical Defence\n -20 Speed";
            ItemCostText.text = "500 000 gold";
            Cost = 500000f;
        }
        else if (NameOfItem == "Heroes_Shield")
        {
            Name = NameOfItem;
            ItemNameText.text = "Heroes Shield";
            ItemEffectText.text = "+35 Physical Defence\n +35 Magic Defence\n +30 Hitpoints";
            ItemCostText.text = "1 000 000 gold";
            Cost = 1000000f;
        }

        //Skill Books
        else if (NameOfItem == "SKILL_BOOK_Take_Down")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Take Down";
            ItemEffectText.text = "Physical move that deals little damage but knocks down opponent.";
            ItemCostText.text = "5 000 gold";
            Cost = 5000f;
            ConsumableItem = true;
            EquipUseText.text = "USE";
        }
        else if (NameOfItem == "SKILL_BOOK_Shield_Bash")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Shield Bash";
            ItemEffectText.text = "Bash the opponent with your shield knocking them down.";
            ItemCostText.text = "10 000 gold";
            Cost = 1000f;
            ConsumableItem = true;
            EquipUseText.text = "USE";
        }
        else if (NameOfItem == "SKILL_BOOK_Giga_Strike")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Giga Strike";
            ItemEffectText.text = "A powerful strike that deals a lot of damage and also knocks down your opponent.";
            ItemCostText.text = "25 000 gold";
            Cost = 25000f;
            ConsumableItem = true;
            EquipUseText.text = "USE";
        }
        else if (NameOfItem == "SKILL_BOOK_Fire_Ball")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Fire Ball";
            ItemEffectText.text = "Throw a ball of fire at your opponent burning them.";
            ItemCostText.text = "1 000 gold";
            Cost = 1000f;
            ConsumableItem = true;
            EquipUseText.text = "USE";
        }
        else if (NameOfItem == "SKILL_BOOK_Flamethrower")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Flamethrower";
            ItemEffectText.text = "Shoot out flames that deal decent damage and burn the opponent.";
            ItemCostText.text = "2 500 gold";
            Cost = 2500f;
            ConsumableItem = true;
            EquipUseText.text = "USE";
        }
        else if (NameOfItem == "SKILL_BOOK_Explosion")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Explosion";
            ItemEffectText.text = "Explode your opponent dealing high damage.";
            ItemCostText.text = "5 000 gold";
            Cost = 5000f;
            ConsumableItem = true;
            EquipUseText.text = "USE";
        }
        else if (NameOfItem == "SKILL_BOOK_Ignite")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Ignite";
            ItemEffectText.text = "Set your opponent on fire burning them.";
            ItemCostText.text = "10 000 gold";
            Cost = 1000f;
            ConsumableItem = true;
            EquipUseText.text = "USE";
        }
        else if (NameOfItem == "SKILL_BOOK_Solar_Flare")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Solar Flare";
            ItemEffectText.text = "Hit your opponent with a powerful fire leaving them burning.";
            ItemCostText.text = "25 000 gold";
            Cost = 25000f;
            ConsumableItem = true;
            EquipUseText.text = "USE";
        }
        else if (NameOfItem == "SKILL_BOOK_Spark")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Spark";
            ItemEffectText.text = "Hit your opponent with a small spark of lightning.";
            ItemCostText.text = "1 000 gold";
            Cost = 1000f;
            ConsumableItem = true;
            EquipUseText.text = "USE";
        }
        else if (NameOfItem == "SKILL_BOOK_Stun_Shot")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Stun Shot";
            ItemEffectText.text = "Shoot a small bolt of lightning stunning your opponent.";
            ItemCostText.text = "2 500 gold";
            Cost = 2500f;
            ConsumableItem = true;
            EquipUseText.text = "USE";
        }
        else if (NameOfItem == "SKILL_BOOK_Bolt")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Bolt";
            ItemEffectText.text = "Hit your opponent with a bolt of lightning.";
            ItemCostText.text = "5 000 gold";
            Cost = 5000f;
            ConsumableItem = true;
            EquipUseText.text = "USE";
        }
        else if (NameOfItem == "SKILL_BOOK_Electrocution")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Electrocution";
            ItemEffectText.text = "Electrocute your opponent dealing a lot of damage.";
            ItemCostText.text = "10 000 gold";
            Cost = 1000f;
            ConsumableItem = true;
            EquipUseText.text = "USE";
        }
        else if (NameOfItem == "SKILL_BOOK_Lightning_Strike")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Lightning Strike";
            ItemEffectText.text = "Call forth a powerful bolt of lightning stunning your opponent and dealing high damage.";
            ItemCostText.text = "25 000 gold";
            Cost = 25000f;
            ConsumableItem = true;
            EquipUseText.text = "USE";
        }
        else if (NameOfItem == "SKILL_BOOK_Icicle_Throw")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Icicle Throw";
            ItemEffectText.text = "Throw an icicle at your opponent.";
            ItemCostText.text = "1 000 gold";
            Cost = 1000f;
            ConsumableItem = true;
            EquipUseText.text = "USE";
        }
        else if (NameOfItem == "SKILL_BOOK_Ice_Ball")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Ice Ball";
            ItemEffectText.text = "Hit your opponent with a ball of ice.";
            ItemCostText.text = "2 500 gold";
            Cost = 2500f;
            ConsumableItem = true;
            EquipUseText.text = "USE";
        }
        else if (NameOfItem == "SKILL_BOOK_Zero")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Zero";
            ItemEffectText.text = "Freeze your opponent leaving them immobile.";
            ItemCostText.text = "5 000 gold";
            Cost = 5000f;
            ConsumableItem = true;
            EquipUseText.text = "USE";
        }
        else if (NameOfItem == "SKILL_BOOK_Ice_Spike")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Ice Spike";
            ItemEffectText.text = "Launch a sharp piece of ice into your opponent.";
            ItemCostText.text = "10 000 gold";
            Cost = 1000f;
            ConsumableItem = true;
            EquipUseText.text = "USE";
        }
        else if (NameOfItem == "SKILL_BOOK_Blizzard")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Blizzard";
            ItemEffectText.text = "Create a storm of ice freezing and damaging your opponent.";
            ItemCostText.text = "25 000 gold";
            Cost = 25000f;
            ConsumableItem = true;
            EquipUseText.text = "USE";
        }
        else if (NameOfItem == "SKILL_BOOK_Poison_Dart")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Poison Dart";
            ItemEffectText.text = "Throw a poison tipped dart at your opponent.";
            ItemCostText.text = "1 000 gold";
            Cost = 1000f;
            ConsumableItem = true;
            EquipUseText.text = "USE";
        }
        else if (NameOfItem == "SKILL_BOOK_Numbing_Gas")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Numbing Gas";
            ItemEffectText.text = "Release a gas that will leave your opponent paralyzed.";
            ItemCostText.text = "2 500 gold";
            Cost = 2500f;
            ConsumableItem = true;
            EquipUseText.text = "USE";
        }
        else if (NameOfItem == "SKILL_BOOK_Poison_Gas")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Poison Gas";
            ItemEffectText.text = "Release a gas that will leave your opponent poisoned.";
            ItemCostText.text = "5 000 gold";
            Cost = 5000f;
            ConsumableItem = true;
            EquipUseText.text = "USE";
        }
        else if (NameOfItem == "SKILL_BOOK_Venom_Bite")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Venom Bite";
            ItemEffectText.text = "Bute your opponent dealing high damage and poisoning them.";
            ItemCostText.text = "10 000 gold";
            Cost = 1000f;
            ConsumableItem = true;
            EquipUseText.text = "USE";
        }
        else if (NameOfItem == "SKILL_BOOK_Radiation")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Radiation";
            ItemEffectText.text = "Shoot out powerful rays of radiation corroding and poisoning your opponent.";
            ItemCostText.text = "25 000 gold";
            Cost = 25000f;
            ConsumableItem = true;
            EquipUseText.text = "USE";
        }
        else
        {
            Name = NameOfItem;
            ItemNameText.text = " ";
            ItemEffectText.text = " ";
            ItemCostText.text = " ";
            Cost = 0f;
            InfoPic.color = Color.clear;
            ConsumableItem = true;
            EquipUseText.text = "USE";
        }

        Icon = sprite;
        InfoPic.sprite = InventorySlots[SlotSelected].transform.Find("Item_Icon").GetComponent<Image>().sprite;
        if (Cost != 0) { InfoPic.color = Color.white; }
        ItemCostText.text = Cost / 2 + " gold";



        //Icon = gameObject.transform.Find("Page 1").transform.Find(NameOfItem).transform.Find("Item_Icon").GetComponent<Image>().sprite;
    }

    void CreateInventory()
    {
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            InventorySlots[i].GetComponent<ItemSlotScript>().InventorySlotNumber = i;
            InventorySlots[i].GetComponent<ItemSlotScript>().Name = "NULL";
            InventorySlots[i].GetComponent<ItemSlotScript>().Icon = Background;
            InventorySlots[i].transform.Find("Item_Icon").gameObject.SetActive(false);
        }
    }
}
