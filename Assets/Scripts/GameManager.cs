using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    //GamaManager 인스턴스
    private static GameManager _instance;

    //재화
    public long beliver;
    public long gold;
    public long tree;
    public long bread;
    public long rock;


    //선교사
    public long missionary;
    public long fanatic;
    public long cardinal;
    public long adult;
    public long dragoon;


    //건물
    public long hut;
    public long church2;
    public long church3;
    public long zeolite1;
    public long zeolite2;
    public long zeolite3;
    public long city1;
    public long city2;
    public long city3;
    

    //기준 비용

    public long UpgradeTreeCost;
    public long UpgradeRockCost;
    public long UpgradeBreadCost;


    //선교사
    public long UpgradeMissionaryCost;
    public long UpgradeFanaticCost;
    public long UpgradeCardinalCost;
    public long UpgradeAdultCost;
    public long UpgradeDragoonCost;


    //건물
    public long UpgradeHutCost;
    public long UpgradeChurch2Cost;
    public long UpgradeChurch3Cost;
    public long UpgradeZeolite1Cost;
    public long UpgradeZeolite2Cost;
    public long UpgradeZeolite3Cost;
    public long UpgradeCity1Cost;
    public long UpgradeCity2Cost;
    public long UpgradeCity3Cost;
    
    //게임 오브젝트
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI beliverText;
    public GameObject ResourcePanel;
    public GameObject MissionaryPanel;
    public GameObject BuildingPanel;

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
    }
    // 화면에 표시되는 재화, 인구수 설정 함수
    public void SetUIText()
    {
        goldText.text = "" + gold;
        beliverText.text = "" + beliver;
    }
    // 데이터 저장 함수
    public void SaveData(String name,String value)
    {
        PlayerPrefs.SetString(name,value);
    }
    // 데이터 로드 함수
    public void LoadData()
    {
        gold = long.Parse(PlayerPrefs.GetString("gold","0"));
        beliver = long.Parse(PlayerPrefs.GetString("beliver","0"));
    }
    //버튼 클릭 함수
    public void ResourceButton_Click()
    {
        ResourcePanel.SetActive(true);
        MissionaryPanel.SetActive(false);
        BuildingPanel.SetActive(false);
    }
    public void Missionary_Click()
    {
        ResourcePanel.SetActive(false);
        MissionaryPanel.SetActive(true);
        BuildingPanel.SetActive(false);
    }
    public void Building_Click()
    {
        ResourcePanel.SetActive(false);
        MissionaryPanel.SetActive(false);
        BuildingPanel.SetActive(true);
    }
    // 현재 재화 수 반환 함수
    public long GetGold() {return Instance.gold;}
    public long GetBeliver() {return Instance.beliver;}
    public long GetRock() {return Instance.rock;}
    public long GetTree(){return Instance.tree;}
    public long GetBread(){return Instance.bread;}
    public long GetMissionary() {return Instance.missionary;}
    public long GetFanatic() {return Instance.fanatic;}
    public long GetCardinal(){return Instance.cardinal;}
    public long GetAdult(){return Instance.adult;}
    public long GetDragoon() {return Instance.dragoon;}
    public long GetHut() {return Instance.hut;}
    public long GetChurch2(){return Instance.church2;}
    public long GetChurch3(){return Instance.church3;}
    public long GetZeolite1() {return Instance.zeolite1;}
    public long GetZeolite2() {return Instance.zeolite2;}
    public long GetZeolite3(){return Instance.zeolite3;}
    public long GetCity1(){return Instance.city1;}
    public long GetCity2(){return Instance.city2;}
    public long GetCity3(){return Instance.city3;}

    //재화 수 증가 함수

    public long AddGold(long amount) { Instance.gold+=amount;SetUIText(); return Instance.gold; }
    public long AddBeliver(long amount) { Instance.beliver+=amount;SetUIText(); return Instance.beliver; }
    public long AddRock(long amount) { Instance.rock+=amount;SetUIText(); return Instance.rock; }
    public long AddTree(long amount){ Instance.tree+=amount;SetUIText(); return Instance.tree; }
    public long AddBread(long amount){ Instance.bread+=amount;SetUIText(); return Instance.bread; }
    public long AddMissionary(long amount) { Instance.missionary+=amount;SetUIText(); return Instance.missionary; }
    public long AddFanatic(long amount) { Instance.fanatic+=amount;SetUIText(); return Instance.fanatic; }
    public long AddCardinal(long amount){ Instance.cardinal+=amount;SetUIText(); return Instance.cardinal; }
    public long AddAdult(long amount){ Instance.adult+=amount;SetUIText(); return Instance.adult; }
    public long AddDragoon(long amount) { Instance.dragoon+=amount;SetUIText(); return Instance.dragoon; }
    public long AddHut(long amount) { Instance.hut+=amount;SetUIText(); return Instance.hut; }
    public long AddChurch2(long amount){ Instance.church2+=amount;SetUIText(); return Instance.church2; }
    public long AddChurch3(long amount){ Instance.church3+=amount;SetUIText(); return Instance.church3; }
    public long AddZeolite1(long amount) { Instance.zeolite1+=amount;SetUIText(); return Instance.zeolite1; }
    public long AddZeolite2(long amount) { Instance.zeolite2+=amount;SetUIText(); return Instance.zeolite2; }
    public long AddZeolite3(long amount){ Instance.zeolite3+=amount;SetUIText(); return Instance.zeolite3; }
    public long AddCity1(long amount){ Instance.city1+=amount;SetUIText(); return Instance.city1; }
    public long AddCity2(long amount){ Instance.city2+=amount;SetUIText(); return Instance.city2; }
    public long AddCity3(long amount){ Instance.city3+=amount;SetUIText(); return Instance.city3; }

    //재화 수 감소 함수
    public long SubGold(long amount) { Instance.gold-=amount;SetUIText(); return Instance.gold; }
    public long SubBeliver(long amount) { Instance.beliver-=amount;SetUIText(); return Instance.beliver; }
    public long SubRock(long amount) { Instance.rock-=amount;SetUIText(); return Instance.rock; }
    public long SubTree(long amount){ Instance.tree-=amount;SetUIText(); return Instance.tree; }
    public long SubBread(long amount){ Instance.bread-=amount;SetUIText(); return Instance.bread; }
    public long SubMissionary(long amount) { Instance.missionary-=amount;SetUIText(); return Instance.missionary; }
    public long SubFanatic(long amount) { Instance.fanatic-=amount;SetUIText(); return Instance.fanatic; }
    public long SubCardinal(long amount){ Instance.cardinal-=amount;SetUIText(); return Instance.cardinal; }
    public long SubAdult(long amount){ Instance.adult-=amount;SetUIText(); return Instance.adult; }
    public long SubDragoon(long amount) { Instance.dragoon-=amount;SetUIText(); return Instance.dragoon; }
    public long SubHut(long amount) { Instance.hut-=amount;SetUIText(); return Instance.hut; }
    public long SubChurch2(long amount){ Instance.church2-=amount;SetUIText(); return Instance.church2; }
    public long SubChurch3(long amount){ Instance.church3-=amount;SetUIText(); return Instance.church3; }
    public long SubZeolite1(long amount) { Instance.zeolite1-=amount;SetUIText(); return Instance.zeolite1; }
    public long SubZeolite2(long amount) { Instance.zeolite2-=amount;SetUIText(); return Instance.zeolite2; }
    public long SubZeolite3(long amount){ Instance.zeolite3-=amount;SetUIText(); return Instance.zeolite3; }
    public long SubCity1(long amount){ Instance.city1-=amount;SetUIText(); return Instance.city1; }
    public long SubCity2(long amount){ Instance.city2-=amount;SetUIText(); return Instance.city2; }
    public long SubCity3(long amount){ Instance.city3-=amount;SetUIText(); return Instance.city3; }


    //버튼 클릭 재화 함수
    public void OnButtonClickRock()
    {
        if (GetGold() <= UpgradeRockCost)
            Debug.Log("골드 부족");
        else 
        {
            SubGold(UpgradeMissionaryCost);
            AddRock(UpgradeMissionaryCost);
        }
    }
    public void OnButtonClickTree()
    {
        if (GetGold() <= UpgradeTreeCost)
            Debug.Log("골드 부족");
        else
                {
            SubGold(UpgradeTreeCost);
            AddTree(UpgradeTreeCost);
        }
    }
    public void OnButtonClickBread()
    {
        if (GetGold() <= UpgradeBreadCost)
            Debug.Log("골드 부족");
        else
        {
            SubGold(UpgradeBreadCost);
            AddBread(UpgradeBreadCost);
        }
    }
    public void OnButtonClickMissionary()
    {
        if (GetGold() <= UpgradeMissionaryCost)
            Debug.Log("골드 부족");
        else
        {
            SubGold(UpgradeMissionaryCost);
            AddMissionary(UpgradeMissionaryCost);
        }
    }
    public void OnButtonClickFanatic()
    {
        if (GetGold() <= UpgradeFanaticCost)
            Debug.Log("골드 부족");
        else
        {
            SubGold(UpgradeFanaticCost);
            AddFanatic(UpgradeFanaticCost);
        }
    }
    public void OnButtonClickCardinal()
    {
        if (GetGold() <= UpgradeCardinalCost)
            Debug.Log("골드 부족");
        else
        {
            SubGold(UpgradeCardinalCost);
            AddCardinal(UpgradeCardinalCost);
        }
    }
    public void OnButtonClickAdult()
    {
        if (GetGold() <= UpgradeMissionaryCost)
            Debug.Log("골드 부족");
        else
        {
            SubGold(UpgradeAdultCost);
            AddAdult(UpgradeAdultCost);
        }
    }
    public void OnButtonClickDragoon()
    {
        if (GetGold() <= UpgradeDragoonCost)
            Debug.Log("골드 부족");
        else
        {
            SubGold(UpgradeDragoonCost);
            AddDragoon(UpgradeDragoonCost);
        }
    }
    public void OnButtonClickHut()
    {
        if (GetGold() <= UpgradeHutCost)
            Debug.Log("골드 부족");
        else
        {
            SubGold(UpgradeHutCost);
            AddHut(UpgradeHutCost);
        }
    }
    public void OnButtonClickChurch2()
    {
        if (GetGold() <= UpgradeChurch2Cost)
            Debug.Log("골드 부족");
        else
        {
            SubGold(UpgradeChurch2Cost);
            AddChurch2(UpgradeChurch2Cost);
        }
    }
    public void OnButtonClickChurch3()
    {
        if (GetGold() <= UpgradeChurch3Cost)
            Debug.Log("골드 부족");
        else
            AddGold(UpgradeChurch3Cost);
    }
    public void OnButtonClickZeolite1()
    {
        if (GetGold() <= UpgradeZeolite1Cost)
            Debug.Log("골드 부족");
        else
            AddGold(UpgradeZeolite1Cost);
    }
    public void OnButtonClickZeolite2()
    {
        if (GetGold() <= UpgradeZeolite2Cost)
            Debug.Log("골드 부족");
        else
            AddGold(UpgradeZeolite2Cost);
    }
    public void OnButtonClickZeolite3()
    {
        if (GetGold() <= UpgradeZeolite3Cost)
            Debug.Log("골드 부족");
        else
            AddGold(UpgradeZeolite3Cost);
    }
    public void OnButtonClickCity1()
    {
        if (GetGold() <= UpgradeCity1Cost)
            Debug.Log("골드 부족");
        else
            AddGold(UpgradeCity1Cost);
    }
    public void OnButtonClickCity2()
    {
        if (GetGold() <= UpgradeCity2Cost)
            Debug.Log("골드 부족");
        else
            AddGold(UpgradeCity2Cost);
    }
    public void OnButtonClickCity3()
    {
        if (GetGold() <= UpgradeCity3Cost)
            Debug.Log("골드 부족");
        else
            AddGold(UpgradeCity3Cost);
    }
}
