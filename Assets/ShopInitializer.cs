using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    public string[] NamesToAdd = new string[20];

    public GameObject ShopObject;

    void Start()
    {
        for (int i = 0; i < NamesToAdd.Length; i++)
        {
            NamesToAdd[i] = NamesToAdd[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeShop()
    {
        for (int i = 0; i < NamesToAdd.Length; i++)
        {
            ShopObject.GetComponent<ShopScript>().SetUpShop(NamesToAdd[i], i);
        }
    }
}
