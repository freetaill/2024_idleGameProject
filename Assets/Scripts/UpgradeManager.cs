using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    // 업그레이드 패널들
    public GameObject upgradePanel1;
    public GameObject upgradePanel2;
    public GameObject upgradePanel3;
    public GameObject upgradePanel4;
    public GameObject configPanel;
    public GameObject configExternalPanel;

    // 애니메이션 설정
    public float animationDuration = 0.5f; // 애니메이션이 걸리는 시간
    public float targetPositionY = 0f; // 패널이 최종적으로 위치할 Y 좌표
    public float hiddenPositionY = -1000f; // 패널이 화면 밖에 있을 Y 좌표

    [Header("신보 탭")]
    // 패널 4 전용 애니메이션 설정
    public float targetPositionYPanel4 = 500f; // 패널 4의 최종 위치 Y 좌표
    public float hiddenPositionYPanel4 = -139f; // 패널 4가 화면 밖에 있을 Y 좌표

    // 현재 활성화된 패널을 추적하는 변수
    private GameObject activeUpgradePanel;
    private GameObject activeConfigPanel;

    public new Camera camera;

    private bool isUpgradeArea = false;
    private bool isUntillUpgradeArea = true;

    private void Start()
    {
        camera = Camera.main;
    }

    // 모든 패널을 비활성화
    private void DeactivateAllPanels()
    {
        if (activeUpgradePanel != null)
        {
            StartCoroutine(AnimatePanel(activeUpgradePanel, false));
            activeUpgradePanel = null;
        }
    }
    // 패널 애니메이션 함수
    private IEnumerator AnimatePanel(GameObject panel, bool isActive)
    {
        RectTransform rectTransform = panel.GetComponent<RectTransform>();
        Vector3 startPos = rectTransform.anchoredPosition;
        Vector3 endPos;

        if (panel == upgradePanel4)
        {
            // 패널 4일 경우 별도의 위치 값을 사용
            endPos = new Vector3(rectTransform.anchoredPosition.x, isActive ? targetPositionYPanel4 : hiddenPositionYPanel4);
        }
        else
        {
            // 일반 패널의 경우
            endPos = new Vector3(rectTransform.anchoredPosition.x, isActive ? targetPositionY : hiddenPositionY);
        }

        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            rectTransform.anchoredPosition = Vector3.Lerp(startPos, endPos, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = endPos;

        if (!isActive)
        {
            panel.SetActive(false);
        }
    }
    public void k(bool value)
    {
        isUpgradeArea = true;
        isUntillUpgradeArea = value;
    }

    // 업그레이드 패널 열기 또는 닫기
    public void ToggleUpgradePanel(GameObject panel)
    {
        if (activeUpgradePanel == panel)
        {
            // 현재 활성화된 패널과 클릭된 패널이 같다면, 패널 닫기
            StartCoroutine(AnimatePanel(panel, false));
            activeUpgradePanel = null; // 아무 패널도 활성화되지 않도록 설정

            k(false);
        }
        else
        {
            // 현재 활성화된 패널을 비활성화
            DeactivateAllPanels();

            // 클릭된 패널 활성화
            panel.SetActive(true);
            StartCoroutine(AnimatePanel(panel, true));
            activeUpgradePanel = panel; // 현재 활성화된 패널로 설정
            k(true);
        }
    }

    // 업그레이드 패널 1 토글
    public void ToggleUpgradePanel1()
    {
        ToggleUpgradePanel(upgradePanel1);
    }

    // 업그레이드 패널 2 토글
    public void ToggleUpgradePanel2()
    {
        ToggleUpgradePanel(upgradePanel2);
    }

    // 업그레이드 패널 3 토글
    public void ToggleUpgradePanel3()
    {
        ToggleUpgradePanel(upgradePanel3);
    }

    // 업그레이드 패널 4 토글
    public void ToggleUpgradePanel4()
    {
        ToggleUpgradePanel(upgradePanel4);
    }

    // 설정 패널 열기 또는 닫기
    public void ToggleConfigPanel(GameObject panel)
    {
        if (activeConfigPanel == panel)
        {
            configPanel.SetActive(false);
            configExternalPanel.SetActive(false);
            activeConfigPanel = null; // 아무 패널도 활성화되지 않도록 설정
        }
        else
        {
            if (activeConfigPanel != null)
            {
                configPanel.SetActive(false);
                configExternalPanel.SetActive(false);
            }

            // 클릭된 패널 활성화
            panel.SetActive(true);
            configExternalPanel.SetActive(true); // 함께 열림

            activeConfigPanel = panel; // 현재 활성화된 패널로 설정
        }
    }

}
