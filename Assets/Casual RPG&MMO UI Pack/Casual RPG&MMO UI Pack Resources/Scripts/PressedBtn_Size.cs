using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PressedBtn_Size : MonoBehaviour , IPointerDownHandler ,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{

    public float scale = 1f;
    Button btn;

    Transform myIcon;

    private void Start()
    {
        btn = GetComponent<Button>();


        myIcon = transform;
    }
 

    public  void OnClick () {

        if(myIcon!=null)
        myIcon.localScale = Vector3.one ;
	}

    public void OnPressed () {
        if (myIcon != null)
            myIcon.localScale = Vector3.one * scale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPressed();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      OnClick();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       // OnPressed();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
     //   OnClick();
    }
}
