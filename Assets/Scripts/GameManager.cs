using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 변수

    //GamaManager 인스턴스
    private static GameManager _instance;
    public long gold;
    public long beliver;

    //게임 오브젝트
    public GameObject UpgradePanel;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI beliverText;

    // 함수
    
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<GameManager>();

                if (_instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    _instance = go.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    void Awake()
    {
        // 싱글톤 패턴 유지 -> 씬 전환 유지
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        //데이터를 읽어옴
        LoadData();
        //텍스트 업로드
        SetUIText();
        //초기 가시화 설정
        UpgradePanel.SetActive(false);

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
    // 현재 재화 수 반환 함수
    public static long GetGold() {return Instance.gold;}
    public static long GetBeliver() {return Instance.beliver;}
    // 신도 수 증가 함수
    public void IncreaseBeliver()
    {
        // 증가 값 설정해야 됨
        beliver++;
        SetUIText();
        SaveData();
    }
    // 재화 증가 함수
    public void IncreaseGold(long amount)
    {
        // 매개 변수로 받은 양 만큼 재화 증가
        gold+= amount;
        SetUIText();
        SaveData();
    }
}
