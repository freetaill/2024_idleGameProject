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
    public float targetPositionY = 345f; // 패널이 최종적으로 위치할 Y 좌표
    public float hiddenPositionY = -139f; // 패널이 화면 밖에 있을 Y 좌표

    [Header("신보 탭")]
    // 패널 4 전용 애니메이션 설정
    public float targetPositionYPanel4 = 575f; // 패널 4의 최종 위치 Y 좌표
    public float hiddenPositionYPanel4 = -139f; // 패널 4가 화면 밖에 있을 Y 좌표

    // 현재 활성화된 패널을 추적하는 변수
    private GameObject activeUpgradePanel;
    private GameObject activeConfigPanel;

    public new Camera camera;
    // 골드와 신도수 패널
    public GameObject GoldAndBelieverPanel;

    private bool isUpgradeArea = false;
    private bool isUntillUpgradeArea = true;

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if (isUpgradeArea)
        {
            // 카메라의 현재 Y 위치 확인
            if (camera.transform.position.y == 0.0f)
            {
                // 0에서 -1.2f로 이동하는 코루틴 시작
                StartCoroutine(MoveCamera(-1.2f, 10f));
            }
            else if (camera.transform.position.y == -1.2f && !isUntillUpgradeArea)
            {
                // -1.2f에서 0으로 이동하는 코루틴 시작
                StartCoroutine(MoveCamera(0.0f, -140f));
            }
            else
            {
                isUpgradeArea = false;
            }
        }
    }
    IEnumerator MoveCamera(float CameratargetY, float PaneltargetY)
    {
        Vector3 startPosition = camera.transform.position;
        Vector3 targetPosition = new Vector3(startPosition.x, CameratargetY, startPosition.z);
        float elapsedTime = 0.0f;

        // 부드럽게 이동
        while (elapsedTime < 3.0f)
        {
            camera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / 1.0f);
            GoldAndBelieverPanel.transform.localPosition = Vector3.Lerp(GoldAndBelieverPanel.transform.localPosition, new Vector3(0, PaneltargetY, 0), elapsedTime / 10.45f);
            elapsedTime += Time.deltaTime * 8.0f;
            yield return null;
        }

        // 최종 위치를 정확히 설정
        camera.transform.position = targetPosition;
        GoldAndBelieverPanel.transform.localPosition = new Vector3(0, PaneltargetY, 0);
        isUpgradeArea = false;
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
