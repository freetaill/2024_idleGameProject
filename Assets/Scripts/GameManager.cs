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


    //노동력
    public long Labor1;
    public long Labor2;
    public long Labor3;
    public long Labor4;

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

    //노동력
    public long UpgradeLabor1Cost;
    public long UpgradeLabor2Cost;
    public long UpgradeLabor3Cost;
    public long UpgradeLabor4Cost;

    
    //게임 오브젝트
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI beliverText;
    public GameObject ResourcePanel;
    public GameObject MissionaryPanel;
    public GameObject BuildingPanel;
    public GameObject WeaponPanel;


    //재화 저장 변수
    public float SaveTime = 10;
    private float timer = 0;

    public System.Random random;
    public long Maxrandom = 10;
    public long MinRandom = 0;

    //초당 골드 획득량
    public long GoldGetAmount = 0;

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
        random = new System.Random();
    }
    void Update()
    {
        // 시간 누적
        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            //초에 따라서 골드 얻기
            AddGold(GoldGetAmount);
        }
        // 일정 시간 간격이 지났는지 확인
        if (timer >= SaveTime)
        {
            SaveData();

            timer = 0f;
        }
    }
    // 화면에 표시되는 재화, 인구수 설정 함수
    public void SetUIText()
    {
        goldText.text = "" + gold;
        beliverText.text = "" + beliver;
    }
    // 데이터 저장 함수
    public void SaveData()
    {
 PlayerPrefs.SetString("beliver", beliver.ToString());
        PlayerPrefs.SetString("gold", gold.ToString());
        PlayerPrefs.SetString("tree", tree.ToString());
        PlayerPrefs.SetString("bread", bread.ToString());
        PlayerPrefs.SetString("rock", rock.ToString());

        PlayerPrefs.SetString("missionary", missionary.ToString());
        PlayerPrefs.SetString("fanatic", fanatic.ToString());
        PlayerPrefs.SetString("cardinal", cardinal.ToString());
        PlayerPrefs.SetString("adult", adult.ToString());
        PlayerPrefs.SetString("dragoon", dragoon.ToString());

        PlayerPrefs.SetString("hut", hut.ToString());
        PlayerPrefs.SetString("church2", church2.ToString());
        PlayerPrefs.SetString("church3", church3.ToString());
        PlayerPrefs.SetString("zeolite1", zeolite1.ToString());
        PlayerPrefs.SetString("zeolite2", zeolite2.ToString());
        PlayerPrefs.SetString("zeolite3", zeolite3.ToString());
        PlayerPrefs.SetString("city1", city1.ToString());
        PlayerPrefs.SetString("city2", city2.ToString());
        PlayerPrefs.SetString("city3", city3.ToString());

        PlayerPrefs.SetString("Labor1", Labor1.ToString());
        PlayerPrefs.SetString("Labor2", Labor2.ToString());
        PlayerPrefs.SetString("Labor3", Labor3.ToString());
        PlayerPrefs.SetString("Labor4", Labor4.ToString());

        PlayerPrefs.SetString("UpgradeTreeCost", UpgradeTreeCost.ToString());
        PlayerPrefs.SetString("UpgradeRockCost", UpgradeRockCost.ToString());
        PlayerPrefs.SetString("UpgradeBreadCost", UpgradeBreadCost.ToString());

        PlayerPrefs.SetString("UpgradeMissionaryCost", UpgradeMissionaryCost.ToString());
        PlayerPrefs.SetString("UpgradeFanaticCost", UpgradeFanaticCost.ToString());
        PlayerPrefs.SetString("UpgradeCardinalCost", UpgradeCardinalCost.ToString());
        PlayerPrefs.SetString("UpgradeAdultCost", UpgradeAdultCost.ToString());
        PlayerPrefs.SetString("UpgradeDragoonCost", UpgradeDragoonCost.ToString());

        PlayerPrefs.SetString("UpgradeHutCost", UpgradeHutCost.ToString());
        PlayerPrefs.SetString("UpgradeChurch2Cost", UpgradeChurch2Cost.ToString());
        PlayerPrefs.SetString("UpgradeChurch3Cost", UpgradeChurch3Cost.ToString());
        PlayerPrefs.SetString("UpgradeZeolite1Cost", UpgradeZeolite1Cost.ToString());
        PlayerPrefs.SetString("UpgradeZeolite2Cost", UpgradeZeolite2Cost.ToString());
        PlayerPrefs.SetString("UpgradeZeolite3Cost", UpgradeZeolite3Cost.ToString());
        PlayerPrefs.SetString("UpgradeCity1Cost", UpgradeCity1Cost.ToString());
        PlayerPrefs.SetString("UpgradeCity2Cost", UpgradeCity2Cost.ToString());
        PlayerPrefs.SetString("UpgradeCity3Cost", UpgradeCity3Cost.ToString());

        PlayerPrefs.SetString("UpgradeLabor1Cost", UpgradeLabor1Cost.ToString());
        PlayerPrefs.SetString("UpgradeLabor2Cost", UpgradeLabor2Cost.ToString());
        PlayerPrefs.SetString("UpgradeLabor3Cost", UpgradeLabor3Cost.ToString());
        PlayerPrefs.SetString("UpgradeLabor4Cost", UpgradeLabor4Cost.ToString());

        PlayerPrefs.Save();
    }
    // 데이터 로드 함수
    public void LoadData()
    {
        beliver = long.Parse(PlayerPrefs.GetString("beliver", "0"));
        gold = long.Parse(PlayerPrefs.GetString("gold", "0"));
        tree = long.Parse(PlayerPrefs.GetString("tree", "0"));
        bread = long.Parse(PlayerPrefs.GetString("bread", "0"));
        rock = long.Parse(PlayerPrefs.GetString("rock", "0"));

        missionary = long.Parse(PlayerPrefs.GetString("missionary", "0"));
        fanatic = long.Parse(PlayerPrefs.GetString("fanatic", "0"));
        cardinal = long.Parse(PlayerPrefs.GetString("cardinal", "0"));
        adult = long.Parse(PlayerPrefs.GetString("adult", "0"));
        dragoon = long.Parse(PlayerPrefs.GetString("dragoon", "0"));

        hut = long.Parse(PlayerPrefs.GetString("hut", "0"));
        church2 = long.Parse(PlayerPrefs.GetString("church2", "0"));
        church3 = long.Parse(PlayerPrefs.GetString("church3", "0"));
        zeolite1 = long.Parse(PlayerPrefs.GetString("zeolite1", "0"));
        zeolite2 = long.Parse(PlayerPrefs.GetString("zeolite2", "0"));
        zeolite3 = long.Parse(PlayerPrefs.GetString("zeolite3", "0"));
        city1 = long.Parse(PlayerPrefs.GetString("city1", "0"));
        city2 = long.Parse(PlayerPrefs.GetString("city2", "0"));
        city3 = long.Parse(PlayerPrefs.GetString("city3", "0"));

        Labor1 = long.Parse(PlayerPrefs.GetString("Labor1", "0"));
        Labor2 = long.Parse(PlayerPrefs.GetString("Labor2", "0"));
        Labor3 = long.Parse(PlayerPrefs.GetString("Labor3", "0"));
        Labor4 = long.Parse(PlayerPrefs.GetString("Labor4", "0"));

        UpgradeTreeCost = long.Parse(PlayerPrefs.GetString("UpgradeTreeCost", "0"));
        UpgradeRockCost = long.Parse(PlayerPrefs.GetString("UpgradeRockCost", "0"));
        UpgradeBreadCost = long.Parse(PlayerPrefs.GetString("UpgradeBreadCost", "0"));

        UpgradeMissionaryCost = long.Parse(PlayerPrefs.GetString("UpgradeMissionaryCost", "0"));
        UpgradeFanaticCost = long.Parse(PlayerPrefs.GetString("UpgradeFanaticCost", "0"));
        UpgradeCardinalCost = long.Parse(PlayerPrefs.GetString("UpgradeCardinalCost", "0"));
        UpgradeAdultCost = long.Parse(PlayerPrefs.GetString("UpgradeAdultCost", "0"));
        UpgradeDragoonCost = long.Parse(PlayerPrefs.GetString("UpgradeDragoonCost", "0"));

        UpgradeHutCost = long.Parse(PlayerPrefs.GetString("UpgradeHutCost", "0"));
        UpgradeChurch2Cost = long.Parse(PlayerPrefs.GetString("UpgradeChurch2Cost", "0"));
        UpgradeChurch3Cost = long.Parse(PlayerPrefs.GetString("UpgradeChurch3Cost", "0"));
        UpgradeZeolite1Cost = long.Parse(PlayerPrefs.GetString("UpgradeZeolite1Cost", "0"));
        UpgradeZeolite2Cost = long.Parse(PlayerPrefs.GetString("UpgradeZeolite2Cost", "0"));
        UpgradeZeolite3Cost = long.Parse(PlayerPrefs.GetString("UpgradeZeolite3Cost", "0"));
        UpgradeCity1Cost = long.Parse(PlayerPrefs.GetString("UpgradeCity1Cost", "0"));
        UpgradeCity2Cost = long.Parse(PlayerPrefs.GetString("UpgradeCity2Cost", "0"));
        UpgradeCity3Cost = long.Parse(PlayerPrefs.GetString("UpgradeCity3Cost", "0"));

        UpgradeLabor1Cost = long.Parse(PlayerPrefs.GetString("UpgradeLabor1Cost", "0"));
        UpgradeLabor2Cost = long.Parse(PlayerPrefs.GetString("UpgradeLabor2Cost", "0"));
        UpgradeLabor3Cost = long.Parse(PlayerPrefs.GetString("UpgradeLabor3Cost", "0"));
        UpgradeLabor4Cost = long.Parse(PlayerPrefs.GetString("UpgradeLabor4Cost", "0"));
    }
    //버튼 클릭 함수
    public void ResourceButton_Click()
    {
        ResourcePanel.SetActive(true);
        MissionaryPanel.SetActive(false);
        BuildingPanel.SetActive(false);
        WeaponPanel.SetActive(false);
    }
    public void Missionary_Click()
    {
        ResourcePanel.SetActive(false);
        MissionaryPanel.SetActive(true);
        BuildingPanel.SetActive(false);
        WeaponPanel.SetActive(false);
    }
    public void Building_Click()
    {
        ResourcePanel.SetActive(false);
        MissionaryPanel.SetActive(false);
        BuildingPanel.SetActive(true);
        WeaponPanel.SetActive(false);
    }
    public void Weapon_Click()
    {
        ResourcePanel.SetActive(false);
        MissionaryPanel.SetActive(false);
        BuildingPanel.SetActive(false);
        WeaponPanel.SetActive(true);
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
    public long GetLabor1(){return Instance.Labor1;}
    public long GetLabor2(){return Instance.Labor2;}
    public long GetLabor3(){return Instance.Labor3;}
    public long GetLabor4(){return Instance.Labor4;}

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
    public long AddLabor1(long amount){ Instance.Labor1+=amount;SetUIText(); return Instance.Labor1; }
    public long AddLabor2(long amount){ Instance.Labor2+=amount;SetUIText(); return Instance.Labor2; }
    public long AddLabor3(long amount){ Instance.Labor3+=amount;SetUIText(); return Instance.Labor3; }
    public long AddLabor4(long amount){ Instance.Labor4+=amount;SetUIText(); return Instance.Labor4; }

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
    public long SubLabor1(long amount){ Instance.Labor1-=amount;SetUIText(); return Instance.Labor1; }
    public long SubLabor2(long amount){ Instance.Labor2-=amount;SetUIText(); return Instance.Labor2; }
    public long SubLabor3(long amount){ Instance.Labor3-=amount;SetUIText(); return Instance.Labor3; }
    public long SubLabor4(long amount){ Instance.Labor4-=amount;SetUIText(); return Instance.Labor4; }


    //현재 재화의 양, 필요한 돈 바꾸기 위한 변수

    //버튼에는 골드가 저장 , amount 에는 현재 재화의 양을 저장

    TextMeshProUGUI CostText;
    TextMeshProUGUI AmountText;


    //버튼 클릭 재화 함수 -> 텍스트, 버튼 텍스트 모두 변경
    public void OnButtonClickRock()
    {

        if (GetLabor1() <= UpgradeRockCost)
            Debug.Log("노동력 부족");
        else 
        {
            SubLabor1(UpgradeRockCost);
            AddRock(UpgradeRockCost);
        }

        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button
        CostText = ResourcePanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = ResourcePanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();

        //이제 텍스트 변경
        CostText.text = UpgradeRockCost+"";
        AmountText.text = GetRock()+"";
    }
    public void OnButtonClickTree()
    {
        if (GetLabor1() <= UpgradeTreeCost)
            Debug.Log("노동력 부족");
        else
                {
            SubLabor1(UpgradeTreeCost);
            AddTree(UpgradeTreeCost);
        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button
        CostText = ResourcePanel.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = ResourcePanel.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = UpgradeTreeCost+"";
        AmountText.text = GetTree()+"";
    }
    public void OnButtonClickBread()
    {
        if (GetLabor1() <= UpgradeBreadCost)
            Debug.Log("노동력 부족");
        else
        {
            SubLabor1(UpgradeBreadCost);
            AddBread(UpgradeBreadCost);
        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = ResourcePanel.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = ResourcePanel.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = UpgradeBreadCost+"";
        AmountText.text = GetBread()+"";
    }
    public void OnButtonClickMissionary()
    {
        if (GetLabor1() <= UpgradeMissionaryCost)
            Debug.Log("노동력 부족");
        else
        {
            SubLabor1(UpgradeMissionaryCost);
            AddMissionary(UpgradeMissionaryCost);
        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = UpgradeMissionaryCost+"";
        AmountText.text = GetMissionary()+"";
    
    }
    public void OnButtonClickFanatic()
    {
        if (GetLabor1() <= UpgradeFanaticCost)
            Debug.Log("노동력 부족");
        else
        {
            SubLabor1(UpgradeFanaticCost);
            AddFanatic(UpgradeFanaticCost);
        }
                //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = UpgradeFanaticCost+"";
        AmountText.text = GetFanatic()+"";
    }
    public void OnButtonClickCardinal()
    {
        if (GetLabor1() <= UpgradeCardinalCost)
            Debug.Log("노동력 부족");
        else
        {
            SubLabor1(UpgradeCardinalCost);
            AddCardinal(UpgradeCardinalCost);
        }

        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = UpgradeCardinalCost+"";
        AmountText.text = GetCardinal()+"";
        
        
        
    }
    public void OnButtonClickAdult()
    {
        if (GetLabor1() <= UpgradeMissionaryCost)
            Debug.Log("노동력 부족");
        else
        {
            SubLabor1(UpgradeAdultCost);
            AddAdult(UpgradeAdultCost);
        }
                //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = UpgradeMissionaryCost+"";
        AmountText.text = GetAdult()+"";
    }
    public void OnButtonClickDragoon()
    {
        if (GetLabor1() <= UpgradeDragoonCost)
            Debug.Log("노동력 부족");
        else
        {
            SubLabor1(UpgradeDragoonCost);
            AddDragoon(UpgradeDragoonCost);
        }

        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(4).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(4).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = UpgradeDragoonCost+"";
        AmountText.text = GetDragoon()+"";
    }
    public void OnButtonClickHut()
    {
        if (GetLabor1() <= UpgradeHutCost)
            Debug.Log("노동력 부족");
        else
        {
            SubLabor1(UpgradeHutCost);
            AddHut(UpgradeHutCost);
        }
                //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = UpgradeHutCost+"";
        AmountText.text = GetHut()+"";
    }
    public void OnButtonClickChurch2()
    {
        if (GetLabor1() <= UpgradeChurch2Cost)
            Debug.Log("노동력 부족");
        else
        {
            SubLabor1(UpgradeChurch2Cost);
            AddChurch2(UpgradeChurch2Cost);
        }
                        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = UpgradeChurch2Cost+"";
        AmountText.text = GetChurch2()+"";
    }
    public void OnButtonClickChurch3()
    {
        if (GetLabor1() <= UpgradeChurch3Cost)
            Debug.Log("노동력 부족");
        else{

            SubLabor1(UpgradeChurch3Cost);
            AddChurch3(UpgradeChurch3Cost);
        }

        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = UpgradeChurch3Cost+"";
        AmountText.text = GetChurch3()+"";
    }
    public void OnButtonClickZeolite1()
    {
        if (GetLabor1() <= UpgradeZeolite1Cost)
            Debug.Log("노동력 부족");
        else
        {
            SubLabor1(UpgradeZeolite1Cost);
            AddZeolite1(UpgradeZeolite1Cost);
        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = UpgradeZeolite1Cost+"";
        AmountText.text = GetZeolite1()+"";
    }
    public void OnButtonClickZeolite2()
    {
        if (GetLabor1() <= UpgradeZeolite2Cost)
            Debug.Log("노동력 부족");
        else
        {
            SubLabor1(UpgradeZeolite2Cost);
            AddZeolite2(UpgradeZeolite2Cost);
        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(4).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(4).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = UpgradeZeolite2Cost+"";
        AmountText.text = GetZeolite2()+"";
    }
    public void OnButtonClickZeolite3()
    {
        if (GetLabor1() <= UpgradeZeolite3Cost)
            Debug.Log("노동력 부족");
        else
        {
            SubLabor1(UpgradeZeolite3Cost);
            AddZeolite2(UpgradeZeolite3Cost);
        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(5).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(5).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = UpgradeZeolite3Cost+"";
        AmountText.text = GetZeolite3()+"";
    }
    public void OnButtonClickCity1()
    {
        if (GetLabor1() <= UpgradeCity1Cost)
            Debug.Log("노동력 부족");
        else
        {
            SubLabor1(UpgradeCity1Cost);
            AddCity1(UpgradeCity1Cost);
        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(6).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(6).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = UpgradeCity1Cost+"";
        AmountText.text = GetCity1()+"";
    }
    public void OnButtonClickCity2()
    {
        if (GetLabor1() <= UpgradeCity2Cost)
            Debug.Log("노동력 부족");
        else
        {
            SubLabor1(UpgradeCity2Cost);
            AddCity1(UpgradeCity2Cost);
        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(7).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(7).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = UpgradeCity2Cost+"";
        AmountText.text = GetCity2()+"";
    }
    public void OnButtonClickCity3()
    {
        if (GetLabor1() <= UpgradeCity3Cost)
            Debug.Log("노동력 부족");
        else
        {
            SubLabor1(UpgradeCity3Cost);
            AddCity3(UpgradeCity3Cost);
        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(8).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(8).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = UpgradeCity3Cost+"";
        AmountText.text = GetCity3()+"";
    }

    // 노동력 획득
    
       public void OnButtonClickLabor1()
    {
        if (GetRock() <= UpgradeLabor1Cost || GetBread() <= UpgradeLabor1Cost || GetTree() <= UpgradeLabor1Cost)
            Debug.Log("재료 부족");
        else
        {
            SubRock(UpgradeLabor1Cost);
            SubBread(UpgradeLabor1Cost);
            SubTree(UpgradeLabor1Cost);

            AddLabor1((long)(random.NextDouble() * (Maxrandom - MinRandom) + MinRandom));
        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = WeaponPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = WeaponPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = UpgradeLabor1Cost+"";
        AmountText.text = GetLabor1()+"";
    }
           public void OnButtonClickLabor2()
    {
        if (GetRock() <= UpgradeLabor2Cost || GetBread() <= UpgradeLabor2Cost || GetTree() <= UpgradeLabor2Cost)
            Debug.Log("재료 부족");
        else
        {
            SubRock(UpgradeLabor2Cost);
            SubBread(UpgradeLabor2Cost);
            SubTree(UpgradeLabor2Cost);

            AddLabor2((long)(random.NextDouble() * (Maxrandom - MinRandom) + MinRandom));
        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = WeaponPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = WeaponPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = UpgradeLabor2Cost+"";
        AmountText.text = GetLabor2()+"";
    }
           public void OnButtonClickLabor3()
    {
        if (GetRock() <= UpgradeLabor3Cost || GetBread() <= UpgradeLabor3Cost || GetTree() <= UpgradeLabor3Cost)
            Debug.Log("재료 부족");
        else
        {
            SubRock(UpgradeLabor3Cost);
            SubBread(UpgradeLabor3Cost);
            SubTree(UpgradeLabor3Cost);

            AddLabor3((long)(random.NextDouble() * (Maxrandom - MinRandom) + MinRandom));
        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = WeaponPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = WeaponPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = UpgradeLabor3Cost+"";
        AmountText.text = GetLabor3()+"";
    }
    public void OnButtonClickLabor4()
    {
        if (GetRock() <= UpgradeLabor4Cost || GetBread() <= UpgradeLabor4Cost || GetTree() <= UpgradeLabor4Cost)
            Debug.Log("재료 부족");
        else
        {
            SubRock(UpgradeLabor4Cost);
            SubBread(UpgradeLabor4Cost);
            SubTree(UpgradeLabor4Cost);

            AddLabor4((long)(random.NextDouble() * (Maxrandom - MinRandom) + MinRandom));
        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = WeaponPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = WeaponPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = UpgradeLabor4Cost+"";
        AmountText.text = GetLabor4()+"";
    }
}
    