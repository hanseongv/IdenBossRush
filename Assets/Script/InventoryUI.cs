using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BTNinventory
{
    Close,
    defult
}

public class InventoryUI : MonoBehaviour
{
    // inventoryPanel 게임 오브젝트를 불러온다
    // inventoryPanel은 유니티에서 InventoryUI를 드래그해서 넣어놨다.
    public GameObject inventoryPanel;

    // activeInventory을 불로 생성하고 false 상태로 해놓는다
    // 인벤토리가 꺼져있는 상태로 시작하기 위해서.
    private bool activeInventory = false;

    private void Start()
    {
        // 시작하면 개체(inventoryPanel)를 활성화/비활성화(activeInventory 상태에 따라) 한다.
        // 시작하면 위에 선언 할 때 activeInventory가 false기 때문에 인벤토리는 비활성화 됨
        inventoryPanel.SetActive(activeInventory);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            // 현재 상태를 반전 시킨다 false면 true로
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
    }
}