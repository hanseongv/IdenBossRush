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
    // inventoryPanel ���� ������Ʈ�� �ҷ��´�
    // inventoryPanel�� ����Ƽ���� InventoryUI�� �巡���ؼ� �־����.
    public GameObject inventoryPanel;

    // activeInventory�� �ҷ� �����ϰ� false ���·� �س��´�
    // �κ��丮�� �����ִ� ���·� �����ϱ� ���ؼ�.
    private bool activeInventory = false;

    private void Start()
    {
        // �����ϸ� ��ü(inventoryPanel)�� Ȱ��ȭ/��Ȱ��ȭ(activeInventory ���¿� ����) �Ѵ�.
        // �����ϸ� ���� ���� �� �� activeInventory�� false�� ������ �κ��丮�� ��Ȱ��ȭ ��
        inventoryPanel.SetActive(activeInventory);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            // ���� ���¸� ���� ��Ų�� false�� true��
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
    }
}