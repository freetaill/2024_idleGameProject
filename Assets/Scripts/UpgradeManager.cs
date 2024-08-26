using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    // ���׷��̵� �гε�
    public GameObject upgradePanel1;
    public GameObject upgradePanel2;
    public GameObject upgradePanel3;

    // ���� Ȱ��ȭ�� �г��� �����ϴ� ����
    private GameObject activePanel;

    // ��� �г��� ��Ȱ��ȭ
    private void DeactivateAllPanels()
    {
        upgradePanel1.SetActive(false);
        upgradePanel2.SetActive(false);
        upgradePanel3.SetActive(false);
    }

    // ���׷��̵� �г� ���� �Ǵ� �ݱ�
    public void ToggleUpgradePanel(GameObject panel)
    {
        if (activePanel == panel)
        {
            // ���� Ȱ��ȭ�� �гΰ� Ŭ���� �г��� ���ٸ�, �г� �ݱ�
            DeactivateAllPanels();
            activePanel = null; // �ƹ� �гε� Ȱ��ȭ���� �ʵ��� ����
        }
        else
        {
            // ���� Ȱ��ȭ�� �г��� ��Ȱ��ȭ
            DeactivateAllPanels();

            // Ŭ���� �г� Ȱ��ȭ
            panel.SetActive(true);
            activePanel = panel; // ���� Ȱ��ȭ�� �гη� ����
        }
    }

    // ���׷��̵� �г� 1 ���
    public void ToggleUpgradePanel1()
    {
        ToggleUpgradePanel(upgradePanel1);
    }

    // ���׷��̵� �г� 2 ���
    public void ToggleUpgradePanel2()
    {
        ToggleUpgradePanel(upgradePanel2);
    }

    // ���׷��̵� �г� 3 ���
    public void ToggleUpgradePanel3()
    {
        ToggleUpgradePanel(upgradePanel3);
    }
}
