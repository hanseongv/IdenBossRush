using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnInventory : MonoBehaviour
{
    public BTNinventory InventorycurrentType;
    public GameObject inventoryPanel;

    public void OnBtnClick()
    {
        switch (InventorycurrentType)
        {
            case BTNinventory.Close:
                inventoryPanel.SetActive(false);
                break;
        }
    }
}