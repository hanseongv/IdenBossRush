using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using UnityEngine.EventSystems;

//IPointerEnterHandler ���콺�� ��� ����
//IPointerExitHandler ���콺�� ���� ����
public class BtnType : MonoBehaviour/*, IPointerEnterHandler, IPointerExitHandler*/
{
    public BTNType currentType;

    //public Transform buttonScale;
    //private Vector3 defautScale;

    //INVENTORY ����
    public GameObject inventoryPanel;

    private void Start()
    {
        // defautScale ���ٰ� buttonScale�� ���ý������� ����
        //defautScale = buttonScale.localScale;
    }

    public void OnBtnClick()
    {
        //���θ޴�����
        switch (currentType)
        {
            case BTNType.Character:
                Debug.Log("������"); ;
                break;

            case BTNType.Inventory:
                inventoryPanel.SetActive(true);
                break;
        }
    }

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    buttonScale.localScale = defautScale * 1.05f;
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    buttonScale.localScale = defautScale;
    //}
}