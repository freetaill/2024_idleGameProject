using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScaler : MonoBehaviour
{
    public Button[] buttons; // 버튼들을 배열로 설정
    public float enlargedScale = 1.2f; // 확대된 버튼의 스케일
    public float normalScale = 1.0f;   // 일반 버튼의 스케일
    public float reducedScale = 1f; // 축소된 버튼의 스케일

    // 현재 어떤 버튼이 확대되었는지 추적하기 위한 변수
    private Button currentlyEnlargedButton = null;

    private void Start()
    {
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClick(button));
        }
    }

    private void OnButtonClick(Button clickedButton)
    {
        // 이미 확대된 버튼을 다시 클릭했을 때 원래 크기로 복원
        if (clickedButton == currentlyEnlargedButton)
        {
            clickedButton.transform.localScale = new Vector3(normalScale, normalScale, normalScale);
            currentlyEnlargedButton = null; // 현재 확대된 버튼 초기화
            ResetAllButtonScales(); // 모든 버튼의 스케일을 원래대로 돌리기
        }
        else
        {
            // 모든 버튼을 원래 크기로 되돌리기
            foreach (Button button in buttons)
            {
                button.transform.localScale = new Vector3(reducedScale, normalScale, reducedScale);
            }

            // 클릭된 버튼을 확대하고 현재 확대된 버튼으로 설정
            clickedButton.transform.localScale = new Vector3(enlargedScale, normalScale, enlargedScale);
            currentlyEnlargedButton = clickedButton;
        }
    }
    // 모든 버튼의 크기를 원래 크기로 초기화
    private void ResetAllButtonScales()
    {
        foreach (Button button in buttons)
        {
            button.transform.localScale = new Vector3(normalScale, normalScale, normalScale);
        }
    }
}
