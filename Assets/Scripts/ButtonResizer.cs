using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonWidthScaler : MonoBehaviour
{
    public Button[] buttons; // 버튼들을 배열로 설정
    public float enlargedWidth = 288f; // 확대된 버튼의 너비
    public float normalWidth = 144f;   // 일반 버튼의 너비

    // 현재 어떤 버튼이 확대되었는지 추적하기 위한 변수
    private Button currentlyEnlargedButton = null;

    private void Start()
    {
        foreach (Button button in buttons)
        {
            // 각 버튼에 클릭 이벤트 리스너 추가
            button.onClick.AddListener(() => OnButtonClick(button));
        }
    }

    private void OnButtonClick(Button clickedButton)
    {
        // 이미 확대된 버튼을 다시 클릭했을 때 원래 너비로 복원
        if (clickedButton == currentlyEnlargedButton)
        {
            currentlyEnlargedButton = null; // 현재 확대된 버튼 초기화
            ResetAllButtonWidths(); // 모든 버튼의 너비를 원래대로 돌리기
        }
        else
        {
            // 모든 버튼의 너비를 원래 크기로 되돌리기
            ResetAllButtonWidths();

            // 클릭된 버튼의 너비를 확대하고 현재 확대된 버튼으로 설정
            SetButtonWidth(clickedButton, enlargedWidth);
            currentlyEnlargedButton = clickedButton;
        }
    }

    // 모든 버튼의 너비를 원래 크기로 초기화
    private void ResetAllButtonWidths()
    {
        foreach (Button button in buttons)
        {
            SetButtonWidth(button, normalWidth);
        }
    }

    // 버튼의 너비를 설정하는 함수
    private void SetButtonWidth(Button button, float width)
    {
        RectTransform rt = button.GetComponent<RectTransform>();
        if (rt != null)
        {
            Vector2 size = rt.sizeDelta;
            rt.sizeDelta = new Vector2(width, size.y);
        }
    }
}
