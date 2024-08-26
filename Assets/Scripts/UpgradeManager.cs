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

    // 현재 활성화된 패널을 추적하는 변수
    private GameObject activePanel;

    // 모든 패널을 비활성화
    private void DeactivateAllPanels()
    {
        upgradePanel1.SetActive(false);
        upgradePanel2.SetActive(false);
        upgradePanel3.SetActive(false);
    }

    // 업그레이드 패널 열기 또는 닫기
    public void ToggleUpgradePanel(GameObject panel)
    {
        if (activePanel == panel)
        {
            // 현재 활성화된 패널과 클릭된 패널이 같다면, 패널 닫기
            DeactivateAllPanels();
            activePanel = null; // 아무 패널도 활성화되지 않도록 설정
        }
        else
        {
            // 현재 활성화된 패널을 비활성화
            DeactivateAllPanels();

            // 클릭된 패널 활성화
            panel.SetActive(true);
            activePanel = panel; // 현재 활성화된 패널로 설정
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
}
