using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using UnityEngine.EventSystems;

//IPointerEnterHandler 마우스를 대면 실행
//IPointerExitHandler 마우스를 때면 실행
public class BtnType : MonoBehaviour/*, IPointerEnterHandler, IPointerExitHandler*/
{
    public BTNType currentType;

    //public Transform buttonScale;
    //private Vector3 defautScale;

    //INVENTORY 관련
    public GameObject inventoryPanel;

    private void Start()
    {
        // defautScale 에다가 buttonScale의 로컬스케일을 넣음
        //defautScale = buttonScale.localScale;
    }

    public void OnBtnClick()
    {
        //메인메뉴관련
        switch (currentType)
        {
            case BTNType.Character:
                Debug.Log("새게임"); ;
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