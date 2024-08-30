using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


/*

정해야 할 것

1. 초당 골드 획득량
2. 초당 신도수 획득량
3. 몇 초마다 저장할 건지
4. 가격을 얼마나 올릴 건지
*/

public class GameManager : MonoBehaviour
{
    //GamaManager 인스턴스
    private static GameManager _instance;

    //재화
    public int[] beliver;
    public int[] gold;
    public int[] tree;
    public int[] bread;
    public int[] rock;
    public int[] GoldGetAmount;


    //선교사
    public int[] missionary;
    public int[] fanatic;
    public int[] cardinal;
    public int[] messia;
    public int[] doctor;


    //건물
    public int[] hut;
    public int[] church2;
    public int[] church3;
    public int[] stone1;
    public int[] stone2;
    public int[] stone3;
    public int[] house1;
    public int[] house2;
    public int[] house3;
    public int[] catstone1;
    public int[] catstone2;
    public int[] catstone3;
     public int[] statue1;
    public int[] statue2;
    public int[] statue3;

    //노동력
    public int[] Labor;
    //기준 비용

    public int[] UpgradeTreeCost;
    public int[] UpgradeRockCost;
    public int[] UpgradeBreadCost;


    //선교사
    public int[] UpgradeMissionaryCost;
    public int[] UpgradeFanaticCost;
    public int[] UpgradeCardinalCost;
    public int[] UpgradeMessiaCost;
    public int[] UpgradeDoctorCost;


    //건물
    public int[] UpgradeHutCost;
    public int[] UpgradeChurch2Cost;
    public int[] UpgradeChurch3Cost;
    public int[] UpgradeStone1Cost;
    public int[] UpgradeStone2Cost;
    public int[] UpgradeStone3Cost;
    public int[] UpgradeHouse1Cost;
    public int[] UpgradeHouse2Cost;
    public int[] UpgradeHouse3Cost;
    public int[] UpgradeCatStone1Cost;
    public int[] UpgradeCatStone2Cost;
    public int[] UpgradeCatStone3Cost;
    public int[] UpgradeStatue1Cost;
    public int[] UpgradeStatue2Cost;
    public int[] UpgradeStatue3Cost;

    //노동력 -> 목재, 빵, 바위 기준치
    public int[] UpgradeLaborCost;

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

    //랜덤 범위 설정
    public int Maxrandom = 10;
    public int MinRandom = 0;

    //신도수당 얻을 수 있는 골드
    public int[] beliverGetGold;

    //초당 얻을 수 있는 신도수
    public int[] beliverGetTime;

    public bool isLoading;
    
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
        Labor = new int[27];
        isLoading = true;
        LoadGameData();
        //텍스트 업로드
        SetUIText();
        //초기 가시화 설정
        random = new System.Random();
        
    }
    void Update()
    {
        if (isLoading == false)
        {
            // 시간 누적
            timer += Time.deltaTime;

            if (timer >= 10f)
            {
                //초에 따라서 골드 얻기
                //AddValue(gold,GoldGetAmount);

                //초당 신도수 더해주기
                AddValue(beliver,beliverGetTime);
                
                //신도수 당 골드 얻기
                for(int i = 0;i<beliver.Length && beliver[0] != 0;i++)
                {
                    for(int j = 0;j<beliver[i];j++)
                    {
                        //AddValue(gold,beliverGetGold);
                    }
                }
                goldText.text = SetText(gold);
                beliverText.text = SetText(beliver);
            }
            // 일정 시간 간격이 지났는지 확인
            if (timer >= SaveTime)
            {
                SaveGameData();

                timer = 0f;
            }
        }

    }
    // 화면에 표시되는 재화, 인구수 설정 함수
    public void SetUIText()
    {

        //바위 비용
        ResourcePanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X " + SetText(UpgradeRockCost);
        ResourcePanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "재화 획득 : " + SetText(rock);

        //나무
        ResourcePanel.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X " + SetText(UpgradeTreeCost);
        ResourcePanel.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "재화 획득 : " + SetText(tree);

        //빵
        ResourcePanel.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X " + SetText(UpgradeBreadCost);
        ResourcePanel.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "재화 획득 : " + SetText(bread); 


        //선교사
        MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X " + SetText(UpgradeMissionaryCost);
        MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "초당 신도 수 + " + SetText(missionary) + "/s"; 

        MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X " + SetText(UpgradeCardinalCost);
        MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "초당 신도 수 + " + SetText(cardinal) + "/s";
        
        MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X " + SetText(UpgradeFanaticCost);
        MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "초당 신도 수 + " + SetText(fanatic) + "/s";
                
        MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X " + SetText(UpgradeMessiaCost);
        MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "초당 신도 수 + " + SetText(messia) + "/s";
        
        MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(4).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X " + SetText(UpgradeDoctorCost);
        MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(4).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "초당 신도 수 + " + SetText(doctor) + "/s";             

        //빌딩
        BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X " + SetText(UpgradeHutCost);
        BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "신도 당 골드 수" + SetText(hut) + "/s";

        
        BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X " + SetText(UpgradeChurch2Cost);
        BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "신도 당 골드 수" + SetText(church2) + "/s";

        
        BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X " + SetText(UpgradeChurch3Cost);
        BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "신도 당 골드 수" + SetText(church3) + "/s";

        
        BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X " + SetText(UpgradeStone1Cost);
        BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "신도 당 골드 수" + SetText(stone1) + "/s";

        
        BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(4).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X " + SetText(UpgradeStone2Cost);
        BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(4).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "신도 당 골드 수" + SetText(stone2) + "/s";

        
        BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(5).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X " + SetText(UpgradeStone3Cost);
        BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(5).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "신도 당 골드 수" + SetText(stone3) + "/s";

        
        BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(6).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X " + SetText(UpgradeHouse1Cost);
        BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(6).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "신도 당 골드 수" + SetText(house1) + "/s";

        
        BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(7).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X " + SetText(UpgradeHouse2Cost);
        BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(7).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "신도 당 골드 수" + SetText(house2) + "/s";

        
        BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(8).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X " + SetText(UpgradeHouse3Cost);
        BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(8).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "신도 당 골드 수" + SetText(house3) + "/s";

        
        BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(9).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X " + SetText(UpgradeCatStone1Cost);
        BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(9).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "신도 당 골드 수" + SetText(catstone1) + "/s";

        
        BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(10).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X " + SetText(UpgradeCatStone3Cost);
        BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(10).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "신도 당 골드 수" + SetText(catstone3) + "/s";

        
        BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(11).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X " + SetText(UpgradeStatue1Cost);
        BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(11).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "신도 당 골드 수" + SetText(statue1) + "/s";

        BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(11).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X " + SetText(UpgradeStatue2Cost);
        BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(11).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "신도 당 골드 수" + SetText(statue2) + "/s";

        BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(11).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X " + SetText(UpgradeStatue3Cost);
        BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(11).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "신도 당 골드 수" + SetText(UpgradeStatue3Cost) + "/s";

        //신보
        WeaponPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X " + SetText(UpgradeLaborCost);
        WeaponPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X " + SetText(UpgradeLaborCost);
        WeaponPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X " + SetText(UpgradeLaborCost);
        isLoading = false;
    
    }
    //현재 재화의 양, 필요한 돈 바꾸기 위한 변수

    //버튼에는 재화를 얻기 위해 내야 되는 비용 , amount 에는 현재 얻을 수 있는 재화의 양을 저장

    TextMeshProUGUI CostText;
    TextMeshProUGUI AmountText;

    //앞의 빈칸은 10000 이전 숫자 저장
    Char[] unit = " ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    // 함수 오버라이딩
    // 배열끼리 더하는 함수

    public void AddValue(int[] targetArray,int[] AddArray)
    {
        //배열끼리 더함 -> 더할 배열의 자릿수까지
        for(int idx = 0;idx<AddArray.Length && AddArray[idx]!=0;idx++)
            targetArray[idx]+=AddArray[idx];
        CheckFlowValue(targetArray);
    }

    // 배열에 정수 값을 더하는 함수
    public void AddValue(int[] targetArray,int AddValue)
    {
        if (targetArray.Length > 0)
            targetArray[0]+=AddValue;
        CheckFlowValue(targetArray);
    }

    public void CheckFlowValue(int[] targetArray)
    {
        //넘친게 있는지 확인
        for(int idx = 0;idx<targetArray.Length-1 && targetArray[idx]!=0;idx++)
        {
            int rest;
            if (targetArray[idx] >= 100000)
            {
                //변수에 일단 저장
                rest = targetArray[idx]/100000;
                targetArray[idx]%=100000;
                //다음 배열에 저장해줌
                targetArray[idx+1]+=rest;
            }
            //마지막 배열은 그냥 넘쳐도 버림
            targetArray[targetArray.Length-1] = Math.Min(targetArray[targetArray.Length-1],99999);
        }
    }
    public int CompareValue(int[] A, int[] B) 
    {
        if (A.Length > B.Length)
            return 1;
        else if (A.Length < B.Length)
            return -1;
        // 길이가 같다는 뜻
        for (int i = A.Length - 1; i >= 0; i--) {
            if (A[i] > B[i])
                return 1;
            else if (A[i] < B[i])
                return -1;
        }
        // for문에서 걸리지 않았다면 같으므로
        return 0;
    }
    public int CompareValue(int[] A,int value)
    {
        if (A.Length > 0 && A[0] > value)
            return 1;
        if (A.Length == 0 && A[0] < value)
            return -1;
        if (A[0] == value)
            return 0;
        int idx = 1;
        int amount = 1;
        while(value >= 100000)
        {
            value/=100000;
            idx++;
            amount*=100000;
        }
        if (A.Length > idx)
            return 1;
        //길이가 같다는 뜻
        for(;idx>=0;idx--)
        {
            if (A[idx] > value/amount)
                return 1;
            if (A[idx] < value/amount)
                return -1;
        }
        //같다는 뜻
        return 0;
    }
    public bool SubValue(int[] targetArray,int[] SubArray)
    {
        //SubArray의 값이 더 크다면
        int compare = CompareValue(targetArray,SubArray);
        if (compare== -1)
            return false;
        if (compare==0)
        {
            //같으니까 다 빼면 0 배열
            targetArray = new int[targetArray.Length];
            return true;
        }
        int SubValue = 0;
        for(int idx = 0;idx<SubArray.Length;idx++)
        {
            targetArray[idx]-=SubValue;
            if (targetArray[idx] < SubArray[idx])
            {
                targetArray[idx] = 100000 + targetArray[idx]-SubArray[idx];
                SubValue = 1;
            }
            else
            {
                targetArray[idx]-=SubArray[idx];
                SubValue  = 0;
            }
        }
        return true;
    }
    public bool SubValue(int[] targetArray,int SubValue)
    {
        int compare = CompareValue(targetArray,SubValue);
        if (compare== -1)
            return false;
        if (compare == 0)
        {
            targetArray = new int[targetArray.Length];
            return true;
        }
        int idx = 0;
        while(SubValue!=0)
        {
            if (targetArray[idx] >= SubValue)
            {
                targetArray[idx]-=SubValue;
                return true;
            }
            targetArray[idx] = 100000 + targetArray[idx]-SubValue;
            //앞에서 땡겨올 거
            SubValue = 1;
            idx++;
        }
        return true;
    }

    // 돈 단위 변경 텍스트

    public String SetText(int[] targetArray)
    {
        //제일 큰 단위를 찾음
        int unitIdx;
        for(unitIdx = 26;unitIdx>=0 && targetArray[unitIdx] == 0;unitIdx--){

        }
        if (unitIdx > 0) //단위가 2개 이상인 경우
        {
            return targetArray[unitIdx].ToString() + unit[unitIdx] + " " + targetArray[unitIdx-1].ToString() + unit[unitIdx-1];
        }
        else if (unitIdx == 0)//단위가 1개인 경우 -> 모든 배열의 합이 1000000 이하인 경우
        {
            return targetArray[unitIdx].ToString() + unit[unitIdx];
        }
        else //모두 빈 배열인 경우
        {
            return "";
        }
    }

    public void OnButtonClickRock()
    {
        if (SubValue(gold,UpgradeRockCost) == false)
            Debug.Log("골드 부족");
        else 
        {
            //배열끼리 더하기
            AddValue(rock,UpgradeRockCost);

            //배열과 숫자 더하기
            AddValue(UpgradeRockCost,UpgradeRockCost[0]);
        }

        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button
        CostText = ResourcePanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = ResourcePanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();

        //이제 텍스트 변경
        CostText.text = "X " + SetText(UpgradeRockCost);


        //다음번에 획득할 수 있는 양
        AmountText.text = "재화 획득 : " + SetText(rock);
    }

    public void OnButtonClickTree()
    {
        if (SubValue(gold,UpgradeTreeCost) == false)
            Debug.Log("골드 부족");
        else
        {
            AddValue(tree,UpgradeTreeCost);
            AddValue(UpgradeTreeCost,UpgradeTreeCost[0]);
        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button
        CostText = ResourcePanel.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = ResourcePanel.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = "X " + SetText(UpgradeTreeCost);

        AmountText.text = "재화 획득 : " + SetText(tree);
    }

    public void OnButtonClickBread()
    {
        if (SubValue(gold,UpgradeBreadCost) == false)
            Debug.Log("골드 부족");
        else
        {
            AddValue(bread,UpgradeBreadCost);
            AddValue(UpgradeBreadCost,UpgradeBreadCost[0]);
        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = ResourcePanel.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = ResourcePanel.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = "X " + SetText(UpgradeBreadCost);

        AmountText.text = "재화 획득 : " + SetText(bread);
    }
    
    //선교사는 골드로 살 수 있음
    public void OnButtonClickMissionary()
    {
        if (SubValue(gold,UpgradeMissionaryCost) == false)
            Debug.Log("골드 부족");
        else
        {
            //초당 신도수 증가율을 높여주기
            AddValue(beliverGetTime,missionary);

            //얻을 수 있는 증가율 바꿔주기
            AddValue(missionary,10);
            
            //비용 올려주기
            AddValue(UpgradeMissionaryCost,UpgradeMissionaryCost[0]);
        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = "X " + SetText(UpgradeMissionaryCost);

        AmountText.text = "신도 수당 골드 획득량 : " + SetText(missionary) + "/s";
    
    }
    public void OnButtonClickFanatic()
    {
        if (SubValue(gold,UpgradeFanaticCost) == false)
            Debug.Log("골드 부족");
        else
        {
            //초당 신도수 증가율을 높여주기
            AddValue(beliverGetTime,fanatic);

            //얻을 수 있는 증가율 바꿔주기
            AddValue(fanatic,10);
            
            //비용 올려주기
            AddValue(UpgradeFanaticCost,UpgradeFanaticCost[0]);
        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = "X " + SetText(UpgradeFanaticCost);

        AmountText.text = "초당 신도수 : "  + SetText(fanatic) + "/s";

    }

    //추기경
    public void OnButtonClickCardinal()
    {
        if (SubValue(gold,UpgradeCardinalCost) == false)
            Debug.Log("골드 부족");
        else
        {
            //초당 신도수 증가율을 높여주기
            AddValue(beliverGetTime,cardinal);

            //얻을 수 있는 증가율 바꿔주기
            AddValue(fanatic,10);
            
            //비용 올려주기
            AddValue(UpgradeCardinalCost,UpgradeCardinalCost[0]);
        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = "X " + SetText(UpgradeCardinalCost);

        AmountText.text = "초당 신도수 : "  + SetText(cardinal) + "/s";

    }
    //메시아
    public void OnButtonClickMessia()
    {
        if (SubValue(gold,UpgradeMessiaCost) == false)
            Debug.Log("골드 부족");
        else
        {
            //초당 신도수 증가율을 높여주기
            AddValue(beliverGetTime,messia);

            //얻을 수 있는 증가율 바꿔주기
            AddValue(messia,10);
            
            //비용 올려주기
            AddValue(UpgradeMessiaCost,UpgradeMessiaCost[0]);
        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = "X " + SetText(UpgradeMessiaCost);

        AmountText.text = "초당 신도수 : "  + SetText(messia) + "/s";

    }
    //달빛치료사
    public void OnButtonClickdocter()
    {
        if (SubValue(gold,UpgradeDoctorCost) == false)
            Debug.Log("골드 부족");
        else
        {
            //초당 신도수 증가율을 높여주기
            AddValue(beliverGetTime,doctor);

            //얻을 수 있는 증가율 바꿔주기
            AddValue(doctor,10);
            
            //비용 올려주기
            AddValue(UpgradeDoctorCost,UpgradeDoctorCost[0]);
        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(4).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = MissionaryPanel.transform.GetChild(0).GetChild(0).GetChild(4).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = "X " + SetText(UpgradeDoctorCost);

        AmountText.text = "초당 신도수 : " + SetText(doctor) + "/s";

    }

    // 빌딩 -> 빌딩은 생산력으로 살 수 있음
    public void OnButtonClickHut()
    {
        if (SubValue(Labor,UpgradeHutCost) == false)
            Debug.Log("노동력 부족");
        else
        {
            AddValue(beliverGetGold,hut);
            AddValue(hut,UpgradeHutCost[0]);
            AddValue(UpgradeHutCost,UpgradeHutCost[0]);

        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = "X " + SetText(UpgradeHutCost);
        AmountText.text = "신도 수당 골드 획득량 : " + SetText(hut) + "/s";
    }
    public void OnButtonClickChurch2()
    {
        if (CompareValue(beliver,100) == -1 || SubValue(Labor,UpgradeChurch2Cost) == false)
            Debug.Log("노동력 부족");
        else
        {
            AddValue(beliverGetGold,church2);
            AddValue(hut,UpgradeChurch2Cost[0]);
            AddValue(UpgradeChurch2Cost,UpgradeChurch2Cost[0]);

        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = "X " + SetText(UpgradeChurch2Cost);
        AmountText.text = "신도 수당 골드 획득량 : " + SetText(church2) + "/s";
    }
    public void OnButtonClickChurch3()
    {
        if (CompareValue(beliver,1000) == -1 || SubValue(Labor,UpgradeChurch3Cost) == false)
            Debug.Log("노동력 부족");
        else
        {
            AddValue(beliverGetGold,church3);
            AddValue(hut,UpgradeChurch3Cost[0]);
            AddValue(UpgradeChurch3Cost,UpgradeChurch3Cost[0]);

        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = "X " + SetText(UpgradeChurch3Cost);
        AmountText.text = "신도 수당 골드 획득량 : " + SetText(church3) + "/s";
    }
    public void OnButtonClickStone1()
    {
        if (CompareValue(beliver,2000) == -1 || SubValue(Labor,UpgradeStone1Cost) == false)
            Debug.Log("노동력 부족");
        else
        {
            AddValue(beliverGetGold,stone1);
            AddValue(stone1,UpgradeStone1Cost[0]);
            AddValue(UpgradeStone1Cost,UpgradeStone1Cost[0]);

        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = "X " + SetText(UpgradeStone1Cost);
        AmountText.text = "신도 수당 골드 획득량 : " + SetText(stone1) + "/s";
    }
    public void OnButtonClickStone2()
    {
        if (CompareValue(beliver,4000) == -1 || SubValue(Labor,UpgradeStone2Cost) == false)
            Debug.Log("노동력 부족");
        else
        {
            AddValue(beliverGetGold,stone2);
            AddValue(stone2,UpgradeStone1Cost[0]);
            AddValue(UpgradeStone1Cost,UpgradeStone1Cost[0]);

        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(4).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(4).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = "X " + SetText(UpgradeStone1Cost);
        AmountText.text = "신도 수당 골드 획득량 : " + SetText(stone2) + "/s";
    }
    public void OnButtonClickStone3()
    {
        if (CompareValue(beliver,8000) == -1 || SubValue(Labor,UpgradeStone3Cost) == false)
            Debug.Log("노동력 부족");
        else
        {
            AddValue(beliverGetGold,stone3);
            AddValue(stone3,UpgradeStone3Cost[0]);
            AddValue(UpgradeStone3Cost,UpgradeStone3Cost[0]);

        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(5).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(5).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = "X " + SetText(UpgradeStone3Cost);
        AmountText.text = "신도 수당 골드 획득량 : " + SetText(stone3) + "/s";
    }
    public void OnButtonClickHouse1()
    {
        if (CompareValue(beliver,16000) == -1 || SubValue(Labor,UpgradeHouse1Cost) == false)
            Debug.Log("노동력 부족");
        else
        {
            AddValue(beliverGetGold,house1);
            AddValue(house1,UpgradeHouse1Cost[0]);
            AddValue(UpgradeHouse1Cost,UpgradeHouse1Cost[0]);

        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(6).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(6).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = "X " + SetText(UpgradeHouse1Cost);
        AmountText.text = "신도 수당 골드 획득량 : " + SetText(house1) + "/s";
    }
    public void OnButtonClickHouse2()
    {
        if (CompareValue(beliver,32000) == -1 || SubValue(Labor,UpgradeHouse2Cost) == false)
            Debug.Log("노동력 부족");
        else
        {
            AddValue(beliverGetGold,house2);
            AddValue(house2,UpgradeHouse2Cost[0]);
            AddValue(UpgradeHouse2Cost,UpgradeHouse2Cost[0]);

        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(7).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(7).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = "X " + SetText(UpgradeHouse2Cost);
        AmountText.text = "신도 수당 골드 획득량 : " + SetText(house2) + "/s";
    }
    public void OnButtonClickHouse3()
    {
        if (CompareValue(beliver,64000) == -1 || SubValue(Labor,UpgradeHouse1Cost) == false)
            Debug.Log("노동력 부족");
        else
        {
            AddValue(beliverGetGold,house3);
            AddValue(house3,UpgradeHouse3Cost[0]);
            AddValue(UpgradeHouse3Cost,UpgradeHouse3Cost[0]);

        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(8).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(8).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = "X " + SetText(UpgradeHouse3Cost);
        AmountText.text = "신도 수당 골드 획득량 : " + SetText(house3) + "/s";
    }
    public void OnButtonClickCatStone1()
    {
        if (CompareValue(beliver,128000) == -1 || SubValue(Labor,UpgradeCatStone1Cost) == false)
            Debug.Log("노동력 부족");
        else
        {
            AddValue(beliverGetGold,catstone1);
            AddValue(catstone1,UpgradeCatStone1Cost[0]);
            AddValue(UpgradeCatStone1Cost,UpgradeCatStone1Cost[0]);

        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(9).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(9).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = "X " + SetText(UpgradeCatStone1Cost);
        AmountText.text = "신도 수당 골드 획득량 : " + SetText(catstone1) + "/s";
    }
    public void OnButtonClickCatStone2()
    {
        if (CompareValue(beliver,256000) == -1 || SubValue(Labor,UpgradeCatStone2Cost) == false)
            Debug.Log("노동력 부족");
        else
        {
            AddValue(beliverGetGold,catstone2);
            AddValue(catstone2,UpgradeCatStone2Cost[0]);
            AddValue(UpgradeCatStone2Cost,UpgradeCatStone2Cost[0]);

        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(10).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(10).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = "X " + SetText(UpgradeCatStone2Cost);
        AmountText.text = "신도 수당 골드 획득량 : " + SetText(catstone2) + "/s";
    }
        public void OnButtonClickCatStone3()
    {
        if (CompareValue(beliver,512000) == -1 || SubValue(Labor,UpgradeCatStone3Cost) == false)
            Debug.Log("노동력 부족");
        else
        {
            AddValue(beliverGetGold,catstone3);
            AddValue(catstone3,UpgradeCatStone3Cost[0]);
            AddValue(UpgradeCatStone3Cost,UpgradeCatStone3Cost[0]);

        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(11).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(11).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = "X " + SetText(UpgradeCatStone3Cost);
        AmountText.text = "신도 수당 골드 획득량 : " + SetText(catstone3) + "/s";
    }
    public void OnButtonClickStatue1()
    {
        if (CompareValue(beliver,1024000) == -1 || SubValue(Labor,UpgradeStatue1Cost) == false)
            Debug.Log("노동력 부족");
        else
        {
            AddValue(beliverGetGold,statue1);
            AddValue(statue1,UpgradeStatue1Cost[0]);
            AddValue(UpgradeStatue1Cost,UpgradeStatue1Cost[0]);

        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(12).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(12).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = "X " + SetText(UpgradeStatue1Cost);
        AmountText.text = "신도 수당 골드 획득량 : " + SetText(statue1) + "/s";
    }
        public void OnButtonClickStatue2()
    {
        if (CompareValue(beliver,2048000) == -1 || SubValue(Labor,UpgradeStatue2Cost) == false)
            Debug.Log("노동력 부족");
        else
        {
            AddValue(beliverGetGold,statue2);
            AddValue(statue2,UpgradeStatue2Cost[0]);
            AddValue(UpgradeStatue2Cost,UpgradeStatue2Cost[0]);

        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(13).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(13).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = "X " + SetText(UpgradeStatue2Cost);
        AmountText.text = "신도 수당 골드 획득량 : " + SetText(statue2) + "/s";
    }
    public void OnButtonClickStatue3()
    {
        if (CompareValue(beliver,5012000) == -1 || SubValue(Labor,UpgradeStatue3Cost) == false)
            Debug.Log("노동력 부족");
        else
        {
            AddValue(beliverGetGold,statue3);
            AddValue(statue3,UpgradeStatue3Cost[0]);
            AddValue(UpgradeStatue3Cost,UpgradeStatue3Cost[0]);

        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        CostText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(14).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        AmountText = BuildingPanel.transform.GetChild(0).GetChild(0).GetChild(14).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        
        //이제 텍스트 변경
        CostText.text = "X " + SetText(UpgradeStatue3Cost);
        AmountText.text = "신도 수당 골드 획득량 : " + SetText(statue3) + "/s";
    }
    
    public void OnButtonClickLabor()
    {
        if (CompareValue(rock,UpgradeLaborCost) == -1 || CompareValue(tree,UpgradeLaborCost) == -1  || CompareValue(bread,UpgradeLaborCost) == -1 )
            Debug.Log("재료 부족");
        else
        {
            AddValue(Labor,random.Next() * (100000 - 20) + 20);
            AddValue(UpgradeLaborCost,UpgradeLaborCost[0]);
        }
        //ResourcePanel 의 ViewPort -> Content -> Panel -> Button

        WeaponPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X " + SetText(UpgradeLaborCost);
        WeaponPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X " + SetText(UpgradeLaborCost);
        WeaponPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X " + SetText(UpgradeLaborCost);
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
    public void SaveGameData()
{
    // 재화
    SaveIntArray("beliver", beliver);
    SaveIntArray("gold", gold);
    SaveIntArray("tree", tree);
    SaveIntArray("bread", bread);
    SaveIntArray("rock", rock);
    SaveIntArray("GoldGetAmount", GoldGetAmount);

    // 선교사
    SaveIntArray("missionary", missionary);
    SaveIntArray("fanatic", fanatic);
    SaveIntArray("cardinal", cardinal);
    SaveIntArray("messia", messia);
    SaveIntArray("doctor", doctor);

    // 건물
    SaveIntArray("hut", hut);
    SaveIntArray("church2", church2);
    SaveIntArray("church3", church3);
    SaveIntArray("stone1", stone1);
    SaveIntArray("stone2", stone2);
    SaveIntArray("stone3", stone3);
    SaveIntArray("house1", house1);
    SaveIntArray("house2", house2);
    SaveIntArray("house3", house3);
    SaveIntArray("catstone1", catstone1);
    SaveIntArray("catstone2", catstone2);
    SaveIntArray("catstone3", catstone3);
    SaveIntArray("statue1", statue1);
    SaveIntArray("statue2", statue2);
    SaveIntArray("statue3", statue3);

    // 노동력
    SaveIntArray("Labor", Labor);

    // 기준 비용
    SaveIntArray("UpgradeTreeCost", UpgradeTreeCost);
    SaveIntArray("UpgradeRockCost", UpgradeRockCost);
    SaveIntArray("UpgradeBreadCost", UpgradeBreadCost);

    // 선교사 업그레이드 비용
    SaveIntArray("UpgradeMissionaryCost", UpgradeMissionaryCost);
    SaveIntArray("UpgradeFanaticCost", UpgradeFanaticCost);
    SaveIntArray("UpgradeCardinalCost", UpgradeCardinalCost);
    SaveIntArray("UpgradeMessiaCost", UpgradeMessiaCost);
    SaveIntArray("UpgradeDoctorCost", UpgradeDoctorCost);

    // 건물 업그레이드 비용
    SaveIntArray("UpgradeHutCost", UpgradeHutCost);
    SaveIntArray("UpgradeChurch2Cost", UpgradeChurch2Cost);
    SaveIntArray("UpgradeChurch3Cost", UpgradeChurch3Cost);
    SaveIntArray("UpgradeStone1Cost", UpgradeStone1Cost);
    SaveIntArray("UpgradeStone2Cost", UpgradeStone2Cost);
    SaveIntArray("UpgradeStone3Cost", UpgradeStone3Cost);
    SaveIntArray("UpgradeHouse1Cost", UpgradeHouse1Cost);
    SaveIntArray("UpgradeHouse2Cost", UpgradeHouse2Cost);
    SaveIntArray("UpgradeHouse3Cost", UpgradeHouse3Cost);
    SaveIntArray("UpgradeCatStone1Cost", UpgradeCatStone1Cost);
    SaveIntArray("UpgradeCatStone2Cost", UpgradeCatStone2Cost);
    SaveIntArray("UpgradeCatStone3Cost", UpgradeCatStone3Cost);
    SaveIntArray("UpgradeStatue1Cost",UpgradeStatue1Cost);
    SaveIntArray("UpgradeStatue2Cost",UpgradeStatue2Cost);
    SaveIntArray("UpgradeStatue3Cost",UpgradeStatue3Cost);
    SaveIntArray("UpgradeLaborCost", UpgradeLaborCost);

    SaveIntArray("beliverGetGold",beliverGetGold);
    SaveIntArray("beliverGetTime",beliverGetTime);

    // 저장 완료
    PlayerPrefs.Save();
}
public void LoadGameData()
{
    // 재화
    beliver = LoadIntArray("beliver",20);
    gold = LoadIntArray("gold",20);
    tree = LoadIntArray("tree",10);
    bread = LoadIntArray("bread",100);
    rock = LoadIntArray("rock",100);
    GoldGetAmount = LoadIntArray("GoldGetAmount",100);

    // 선교사
    missionary = LoadIntArray("missionary",100);
    fanatic = LoadIntArray("fanatic",10);
    cardinal = LoadIntArray("cardinal",10);
    messia = LoadIntArray("messia",10);
    doctor = LoadIntArray("doctor",10);

    // 건물
    hut = LoadIntArray("hut",10);
    church2 = LoadIntArray("church2",20);
    church3 = LoadIntArray("church3",30);
    stone1 = LoadIntArray("stone1",30);
    stone2 = LoadIntArray("stone2",10);
    stone3 = LoadIntArray("stone3",20);
    house1 = LoadIntArray("house1",30);
    house2 = LoadIntArray("house2",10);
    house3 = LoadIntArray("house3",11);
    catstone1 = LoadIntArray("catstone1",12);
    catstone2 = LoadIntArray("catstone2",10);
    catstone3 = LoadIntArray("catstone3",20);
    statue1 = LoadIntArray("statue1",40);
    statue2 = LoadIntArray("statue2",50);
    statue3 = LoadIntArray("statue3",60);

    // 노동력
    Labor = LoadIntArray("Labor",10);

    // 기준 비용
    UpgradeTreeCost = LoadIntArray("UpgradeTreeCost",100);
    UpgradeRockCost = LoadIntArray("UpgradeRockCost",100);
    UpgradeBreadCost = LoadIntArray("UpgradeBreadCost",100);

    // 선교사 업그레이드 비용
    UpgradeMissionaryCost = LoadIntArray("UpgradeMissionaryCost",100);
    UpgradeFanaticCost = LoadIntArray("UpgradeFanaticCost",100);
    UpgradeCardinalCost = LoadIntArray("UpgradeCardinalCost",100);
    UpgradeMessiaCost = LoadIntArray("UpgradeMessiaCost",100);
    UpgradeDoctorCost = LoadIntArray("UpgradeDoctorCost",100);

    // 건물 업그레이드 비용
    UpgradeHutCost = LoadIntArray("UpgradeHutCost",100);
    UpgradeChurch2Cost = LoadIntArray("UpgradeChurch2Cost",100);
    UpgradeChurch3Cost = LoadIntArray("UpgradeChurch3Cost",100);
    UpgradeStone1Cost = LoadIntArray("UpgradeStone1Cost",100);
    UpgradeStone2Cost = LoadIntArray("UpgradeStone2Cost",100);
    UpgradeStone3Cost = LoadIntArray("UpgradeStone3Cost",100);
    UpgradeHouse1Cost = LoadIntArray("UpgradeHouse1Cost",100);
    UpgradeHouse2Cost = LoadIntArray("UpgradeHouse2Cost",100);
    UpgradeHouse3Cost = LoadIntArray("UpgradeHouse3Cost",100);
    UpgradeCatStone1Cost = LoadIntArray("UpgradeCatStone1Cost",100);
    UpgradeCatStone2Cost = LoadIntArray("UpgradeCatStone2Cost",100);
    UpgradeCatStone3Cost = LoadIntArray("UpgradeCatStone3Cost",100);
    UpgradeStatue1Cost = LoadIntArray("UpgradeStatue1Cost",100);
    UpgradeStatue2Cost = LoadIntArray("UpgradeStatue2Cost",100);
    UpgradeStatue3Cost = LoadIntArray("UpgradeStatue3Cost",100);
    UpgradeLaborCost = LoadIntArray("UpgradeLaborCost",10);
    
    beliverGetGold = LoadIntArray("beliverGetGold",10);
    beliverGetTime = LoadIntArray("beliverGetTime",15);
}

    public void SaveIntArray(string key, int[] array)
    {
        if (array == null || array.Length == 0)
        {
            // 비어있는 배열을 저장할 때는 빈 문자열로 저장
            PlayerPrefs.SetString(key, string.Empty);
        }
        else{
            string arrayString = string.Join(",", array);
            PlayerPrefs.SetString(key, arrayString);
        }

    }

    public int[] LoadIntArray(string key,int initNum)
    {
        string arrayString = PlayerPrefs.GetString(key, string.Empty);
        if (string.IsNullOrEmpty(arrayString))
        {
            int[] array = new int[27];
            array[0] = initNum;
            return array;
        }

        string[] stringArray = arrayString.Split(',');

        int[] intArray = new int[stringArray.Length];

        for (int i = 0; i < stringArray.Length; i++)
        {
            intArray[i] = int.Parse(stringArray[i]);
        }

        return intArray;
    }
}
    