using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopScript : MonoBehaviour
{
    public string Name = "";
    public GameObject[] ItemSlots = new GameObject[20];
    public string[] ItemNames = new string[20];
    public Sprite[] Icons = new Sprite[28];
    public Image SlotItemIcon;
    public TextMeshProUGUI ItemNameText;
    public TextMeshProUGUI ItemEffectText;
    public TextMeshProUGUI ItemCostText;
    public GameObject BuyButton;
    public float Cost = 0f;

    int Selected = 0;

    public GameObject Inventory;
    public GameObject gameControllerObject;
    // Start is called before the first frame update
    void Start()
    {
       for (int i = 0; i < ItemNames.Length; i++)
        {
            SetUpShop(ItemNames[i], i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SlotClicked(int x)
    {
        ChangeInfo(ItemNames[x]);
        Selected = x;
        if (ItemNames[x] == "NULL") { SlotItemIcon.gameObject.SetActive(false); }
        else { SlotItemIcon.gameObject.SetActive(true); SlotItemIcon.GetComponent<Image>().sprite = ItemSlots[x].transform.Find("Icon").GetComponent<Image>().sprite; }
    }

    public void SetUpShop(string NameOfItem, int Slot)
    {
        ItemNames[Slot] = NameOfItem;
        //Potions
        if (NameOfItem == "Small_Health_Potion")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[0];
        }
        else if (NameOfItem == "Medium_Health_Potion")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[1];
        }
        else if (NameOfItem == "Large_Health_Potion")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[2];
        }

        //Weapons
        else if (NameOfItem == "Rusty_Sword")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[3];
        }
        else if (NameOfItem == "Basic_Sword")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[4];
        }
        else if (NameOfItem == "Iron_Sword")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[5];
        }
        else if (NameOfItem == "Katana")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[6];
        }
        else if (NameOfItem == "Knight_Sword")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[7];
        }
        else if (NameOfItem == "Staff")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[8];
        }
        else if (NameOfItem == "Druid_Wand")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[9];
        }
        else if (NameOfItem == "Golden_Staff")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[10];
        }
        else if (NameOfItem == "Steel_Axe")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[11];
        }
        else if (NameOfItem == "Kings_Blade")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[12];
        }
        else if (NameOfItem == "Steel_Mace")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[13];
        }
        else if (NameOfItem == "Stone_Hammer")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[14];
        }
        else if (NameOfItem == "War_Hammer")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[15];
        }
        else if (NameOfItem == "Magic_Blade")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[16];
        }
        else if (NameOfItem == "Sword_Of_Heroes")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[17];
        }

        //Shields
        else if (NameOfItem == "Wooden_Shield")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[18];
        }
        else if (NameOfItem == "Basic_Shield")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[19];
        }
        else if (NameOfItem == "Iron_Shield")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[20];
        }
        else if (NameOfItem == "Magic_Shield")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[21];
        }
        else if (NameOfItem == "Knight_Shield")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[22];
        }
        else if (NameOfItem == "Heavy_Shield")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[23];
        }
        else if (NameOfItem == "Dark_Knight_Shield")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[24];
        }
        else if (NameOfItem == "Gem_Shield")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[25];
        }
        else if (NameOfItem == "All_Purpose_Defense")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[26];
        }
        else if (NameOfItem == "Heroes_Shield")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[27];
        }
        //SkillBooks
        else if (NameOfItem == "SKILL_BOOK_Take_Down")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[28];
        }
        else if (NameOfItem == "SKILL_BOOK_Shield_Bash")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[28];
        }
        else if (NameOfItem == "SKILL_BOOK_Giga_Strike")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[28];
        }
        else if (NameOfItem == "SKILL_BOOK_Fire_Ball")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[28];
        }
        else if (NameOfItem == "SKILL_BOOK_Flamethrower")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[28];
        }
        else if (NameOfItem == "SKILL_BOOK_Explosion")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[28];
        }
        else if (NameOfItem == "SKILL_BOOK_Ignite")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[28];
        }
        else if (NameOfItem == "SKILL_BOOK_Solar_Flare")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[28];
        }
        else if (NameOfItem == "SKILL_BOOK_Spark")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[28];
        }
        else if (NameOfItem == "SKILL_BOOK_Stun_Shot")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[28];
        }
        else if (NameOfItem == "SKILL_BOOK_Bolt")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[28];
        }
        else if (NameOfItem == "SKILL_BOOK_Electrocution")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[28];
        }
        else if (NameOfItem == "SKILL_BOOK_Lightning_Strike")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[28];
        }
        else if (NameOfItem == "SKILL_BOOK_Icicle_Throw")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[28];
        }
        else if (NameOfItem == "SKILL_BOOK_Ice_Ball") 
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[28];
        }
        else if (NameOfItem == "SKILL_BOOK_Zero")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[28];
        }
        else if (NameOfItem == "SKILL_BOOK_Ice_Spike")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[28];
        }
        else if (NameOfItem == "SKILL_BOOK_Blizzard")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[28];
        }
        else if (NameOfItem == "SKILL_BOOK_Poison_Dart")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[28];
        }
        else if (NameOfItem == "SKILL_BOOK_Numbing_Gas")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[28];
        }
        else if (NameOfItem == "SKILL_BOOK_Poison_Gas")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[28];
        }
        else if (NameOfItem == "SKILL_BOOK_Venom_Bite")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[28];
        }
        else if (NameOfItem == "SKILL_BOOK_Radiation")
        {
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(true);
            ItemSlots[Slot].transform.Find("Icon").GetComponent<Image>().sprite = Icons[28];
        }
        else
        {
            ItemNames[Slot] = "NULL";
            ItemSlots[Slot].transform.Find("Icon").gameObject.SetActive(false);
        }

        
    }

    public void ChangeInfo(string NameOfItem)
    {
        //Potions
        if (NameOfItem == "Small_Health_Potion")
        {
            Name = NameOfItem;
            ItemNameText.text = "Small Health Potion";
            ItemEffectText.text = "Heals player by 100 points.";
            ItemCostText.text = "100 gold";
            Cost = 100f;
        }
        else if (NameOfItem == "Medium_Health_Potion")
        {
            Name = NameOfItem;
            ItemNameText.text = "Medium Health Potion";
            ItemEffectText.text = "Heals player by 250 points.";
            ItemCostText.text = "500 gold";
            Cost = 500f;
        }
        else if (NameOfItem == "Large_Health_Potion")
        {
            Name = NameOfItem;
            ItemNameText.text = "Large Health Potion";
            ItemEffectText.text = "Heals player by 500 points.";
            ItemCostText.text = "1000 gold";
            Cost = 1500f;
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
            ItemCostText.text = "750 gold";
            Cost = 750f;
        }
        else if (NameOfItem == "Druid_Wand")
        {
            Name = NameOfItem;
            ItemNameText.text = "Druid Wand";
            ItemEffectText.text = "+5 Magic Attack\n +2 Physical Attack";
            ItemCostText.text = "1 500 gold";
            Cost = 1500f;
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
            ItemCostText.text = "2 500 gold";
            Cost = 2500f;
        }
        else if (NameOfItem == "SKILL_BOOK_Shield_Bash")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Shield Bash";
            ItemEffectText.text = "Bash the opponent with your shield knocking them down.";
            ItemCostText.text = "5 000 gold";
            Cost = 5000f;
        }
        else if (NameOfItem == "SKILL_BOOK_Giga_Strike")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Giga Strike";
            ItemEffectText.text = "A powerful strike that deals a lot of damage and also knocks down your opponent.";
            ItemCostText.text = "10 000 gold";
            Cost = 10000f;
        }
        else if (NameOfItem == "SKILL_BOOK_Fire_Ball")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Fire Ball";
            ItemEffectText.text = "Throw a ball of fire at your opponent burning them.";
            ItemCostText.text = "500 gold";
            Cost = 500f;
        }
        else if (NameOfItem == "SKILL_BOOK_Flamethrower")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Flamethrower";
            ItemEffectText.text = "Shoot out flames that deal decent damage and burn the opponent.";
            ItemCostText.text = "1 000 gold";
            Cost = 1000f;
        }
        else if (NameOfItem == "SKILL_BOOK_Explosion")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Explosion";
            ItemEffectText.text = "Explode your opponent dealing high damage.";
            ItemCostText.text = "2 500 gold";
            Cost = 2500f;
        }
        else if (NameOfItem == "SKILL_BOOK_Ignite")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Ignite";
            ItemEffectText.text = "Set your opponent on fire burning them.";
            ItemCostText.text = "5 000 gold";
            Cost = 5000f;
        }
        else if (NameOfItem == "SKILL_BOOK_Solar_Flare")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Solar Flare";
            ItemEffectText.text = "Hit your opponent with a powerful fire leaving them burning.";
            ItemCostText.text = "10 000 gold";
            Cost = 10000f;
        }
        else if (NameOfItem == "SKILL_BOOK_Spark")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Spark";
            ItemEffectText.text = "Hit your opponent with a small spark of lightning.";
            ItemCostText.text = "500 gold";
            Cost = 500f;
        }
        else if (NameOfItem == "SKILL_BOOK_Stun_Shot")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Stun Shot";
            ItemEffectText.text = "Shoot a small bolt of lightning stunning your opponent.";
            ItemCostText.text = "1 000 gold";
            Cost = 1000f;
        }
        else if (NameOfItem == "SKILL_BOOK_Bolt")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Bolt";
            ItemEffectText.text = "Hit your opponent with a bolt of lightning.";
            ItemCostText.text = "2 500 gold";
            Cost = 2500f;
        }
        else if (NameOfItem == "SKILL_BOOK_Electrocution")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Electrocution";
            ItemEffectText.text = "Electrocute your opponent dealing a lot of damage.";
            ItemCostText.text = "5 000 gold";
            Cost = 5000f;
        }
        else if (NameOfItem == "SKILL_BOOK_Lightning_Strike")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Lightning Strike";
            ItemEffectText.text = "Call forth a powerful bolt of lightning stunning your opponent and dealing high damage.";
            ItemCostText.text = "10 000 gold";
            Cost = 10000f;
        }
        else if (NameOfItem == "SKILL_BOOK_Icicle_Throw")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Icicle Throw";
            ItemEffectText.text = "Throw an icicle at your opponent.";
            ItemCostText.text = "500 gold";
            Cost = 500f;
        }
        else if (NameOfItem == "SKILL_BOOK_Ice_Ball")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Ice Ball";
            ItemEffectText.text = "Hit your opponent with a ball of ice.";
            ItemCostText.text = "1 000 gold";
            Cost = 1000f;
        }
        else if (NameOfItem == "SKILL_BOOK_Zero")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Zero";
            ItemEffectText.text = "Freeze your opponent leaving them immobile.";
            ItemCostText.text = "2 500 gold";
            Cost = 2500f;
        }
        else if (NameOfItem == "SKILL_BOOK_Ice_Spike")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Ice Spike";
            ItemEffectText.text = "Launch a sharp piece of ice into your opponent.";
            ItemCostText.text = "5 000 gold";
            Cost = 5000f;
        }
        else if (NameOfItem == "SKILL_BOOK_Blizzard")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Blizzard";
            ItemEffectText.text = "Create a storm of ice freezing and damaging your opponent.";
            ItemCostText.text = "10 000 gold";
            Cost = 10000f;
        }
        else if (NameOfItem == "SKILL_BOOK_Poison_Dart")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Poison Dart";
            ItemEffectText.text = "Throw a poison tipped dart at your opponent.";
            ItemCostText.text = "500 gold";
            Cost = 500f;
        }
        else if (NameOfItem == "SKILL_BOOK_Numbing_Gas")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Numbing Gas";
            ItemEffectText.text = "Release a gas that will leave your opponent paralyzed.";
            ItemCostText.text = "1 000 gold";
            Cost = 1000f;
        }
        else if (NameOfItem == "SKILL_BOOK_Poison_Gas")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Poison Gas";
            ItemEffectText.text = "Release a gas that will leave your opponent poisoned.";
            ItemCostText.text = "2 500 gold";
            Cost = 2500f;
        }
        else if (NameOfItem == "SKILL_BOOK_Venom_Bite")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Venom Bite";
            ItemEffectText.text = "Bute your opponent dealing high damage and poisoning them.";
            ItemCostText.text = "5 000 gold";
            Cost = 5000f;
        }
        else if (NameOfItem == "SKILL_BOOK_Radiation")
        {
            Name = NameOfItem;
            ItemNameText.text = "Skill Book: Radiation";
            ItemEffectText.text = "Shoot out powerful rays of radiation corroding and poisoning your opponent.";
            ItemCostText.text = "10 000 gold";
            Cost = 10000f;
        }
        else 
        {
            Name = NameOfItem;
            ItemNameText.text = " ";
            ItemEffectText.text = " ";
            ItemCostText.text = " ";
            Cost = 0f;
        }


        ItemCostText.text = Cost + " gold";
        //Icon = gameObject.transform.Find("Page 1").transform.Find(NameOfItem).transform.Find("Item_Icon").GetComponent<Image>().sprite;
    }

    public void Buy()
    {
        Debug.Log("Item Bought");
        for (int i = 0; i < Inventory.GetComponent<InventorySystemScript>().InventorySlots.Length; i++)
        {
            if (Inventory.GetComponent<InventorySystemScript>().InventorySlots[i].GetComponent<ItemSlotScript>().Name == null || Inventory.GetComponent<InventorySystemScript>().InventorySlots[i].GetComponent<ItemSlotScript>().Name == "NULL")
            {
                if (Name != "NULL" && gameControllerObject.GetComponent<GameController>().gold >= Cost) 
                { 
                    Inventory.GetComponent<InventorySystemScript>().AddItem(Name, ItemSlots[Selected].transform.Find("Icon").GetComponent<Image>().sprite);
                    gameControllerObject.GetComponent<GameController>().gold -= Cost;
                    return;
                }
            }
        }
        //if (Name != "NULL") { Inventory.GetComponent<InventorySystemScript>().AddItem(Name, ItemSlots[Selected].transform.Find("Icon").GetComponent<Image>().sprite); }
    }
}
