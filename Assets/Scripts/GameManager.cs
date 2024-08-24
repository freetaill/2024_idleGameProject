using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 변수
    public long gold;
    public long beliver;

    //게임 오브젝트
    public GameObject UpgradePanel;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI beliverText;

    // 함수

    void Start()
    {
        //데이터를 읽어옴
        LoadData();
        //텍스트 업로드
        SetUIText();
        //초기 가시화 설정
        UpgradePanel.SetActive(false);

    }
    void Update()
    {
        //데이터 저장
        SaveData();
    }
    // 화면에 표시되는 재화, 인구수 설정 함수
    void SetUIText()
    {
        goldText.text = "" + gold;
        beliverText.text = "" + beliver;
    }
    // 데이터 저장 함수
    void SaveData()
    {
        PlayerPrefs.SetString("gold",gold.ToString());
        PlayerPrefs.SetString("beliver",beliver.ToString());
    }
    // 데이터 로드 함수
    void LoadData()
    {
        gold = long.Parse(PlayerPrefs.GetString("gold","0"));
        beliver = long.Parse(PlayerPrefs.GetString("beliver","0"));
    }
    // 업그레이드 패널 가시화 설정 함수
    void OnClickedUpgradePanelButton()
    {
        UpgradePanel.SetActive(!UpgradePanel.activeSelf);
    }
    // 신도 수 증가 함수
    void IncreaseBeliver()
    {
        // 증가 값 설정해야 됨
        beliver++;
        SetUIText();
    }
    // 재화 증가 함수
    void IncreaseGold(long amount)
    {
        // 매개 변수로 받은 양 만큼 재화 증가
        gold+= amount;
        SetUIText();
    }
}
