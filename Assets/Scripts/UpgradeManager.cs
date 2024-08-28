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

    // 현재 활성화된 패널을 추적하는 변수
    private GameObject activeUpgradePanel;
    private GameObject activeConfigPanel;

    GameObject CameraMove;

    // 모든 패널을 비활성화
    private void DeactivateAllPanels()
    {
        upgradePanel1.SetActive(false);
        upgradePanel2.SetActive(false);
        upgradePanel3.SetActive(false);
        upgradePanel4.SetActive(false);
    }
    
    // 설정 패널 열기 또는 닫기
    public void ToggleConfigPanel(GameObject panel)
    {
        if (activeConfigPanel == panel)
        {
            configPanel.SetActive(false);
            activeConfigPanel = null; // 아무 패널도 활성화되지 않도록 설정
        }
        else
        {
            configPanel.SetActive(false);

            // 클릭된 패널 활성화
            panel.SetActive(true);
            activeConfigPanel = panel; // 현재 활성화된 패널로 설정
        }
    }

    // 업그레이드 패널 열기 또는 닫기
    public void ToggleUpgradePanel(GameObject panel)
    {
        if (activeUpgradePanel == panel)
        {
            // 현재 활성화된 패널과 클릭된 패널이 같다면, 패널 닫기
            DeactivateAllPanels();
            activeUpgradePanel = null; // 아무 패널도 활성화되지 않도록 설정

            gameObject.GetComponent<CameraMoveControl>().k(false);
        }
        else
        {
            // 현재 활성화된 패널을 비활성화
            DeactivateAllPanels();

            // 클릭된 패널 활성화
            panel.SetActive(true);
            activeUpgradePanel = panel; // 현재 활성화된 패널로 설정
            gameObject.GetComponent<CameraMoveControl>().k(true);
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

}
