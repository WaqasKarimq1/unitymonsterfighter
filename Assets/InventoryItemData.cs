using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemData : MonoBehaviour
{
    public string id;
    public string Name = "";
    public string type = ""; //Consumable/Equippable
    public string subtype = ""; //Weapon, armour, shield, potion
    public float StatGainOne = 0;
    public float StatGainTwo = 0;
    public float StatGainThree = 0;
    public string FlavourText = "";
    public float SellPrice = 0;

    //public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemSelected(string Name)
    {
        if (Name == "Small_Health_Potion")
        {
            Name = "Small Health Potion";
            type = "Consumable";
            subtype = "Health_Potion";

        }
    }
}
