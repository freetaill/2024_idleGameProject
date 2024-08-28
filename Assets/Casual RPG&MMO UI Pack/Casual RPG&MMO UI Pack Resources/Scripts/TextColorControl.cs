using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UI.CoroutineTween;

public class TextColorControl : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public Color normalColor;
    public Color highlightColor;

    public void Start()
    {
      GetComponent<TextMeshProUGUI>().color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<TextMeshProUGUI>().color = highlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       GetComponent<TextMeshProUGUI>().color = normalColor ;
    }

 
}
