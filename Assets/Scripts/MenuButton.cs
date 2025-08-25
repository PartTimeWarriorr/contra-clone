using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private GameObject cursor;
    void Awake()
    {
        cursor = transform.Find("Cursor").gameObject;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cursor.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cursor.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }
}
