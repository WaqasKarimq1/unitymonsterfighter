using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlotScript : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public int InventorySlotNumber = 0;
    public string Name = "NULL";
    public Sprite Icon;
    public bool IsSelected = false;
    public GameObject InventoryObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelect(BaseEventData eventData)
    {
        InventoryObject.GetComponent<InventorySystemScript>().SlotSelected = InventorySlotNumber;
    }
    public void OnDeselect(BaseEventData eventData)
    {
        IsSelected = false;
    }

    public void Selected()
    {
        InventoryObject.GetComponent<InventorySystemScript>().ChangeInfo(Name, Icon);
    }
}
