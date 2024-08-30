using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuideQuestManager : MonoBehaviour
{
    public TextMeshProUGUI questText; // 퀘스트 상태를 표시할 UI 텍스트
    public TextMeshProUGUI completionText; // 퀘스트 완료 시 잠시 동안 표시될 UI 텍스트

    private long currentGold; // 현재 골드 양
    private int currentQuestIndex = 0; // 현재 퀘스트 인덱스
    private bool isQuestCompleted = false; // 현재 퀘스트 완료 여부

    // 퀘스트 목표 목록 (순차적으로 진행)
    private int[] questGoals = { 1000, 5000, 10000, 20000, 50000 };

    void Start()
    {
        UpdateQuestStatus(); // 퀘스트 상태를 초기화
        completionText.gameObject.SetActive(false); // 퀘스트 완료 텍스트를 비활성화
    }

    void Update()
    {
        // 실시간으로 골드 수를 업데이트하여 퀘스트 달성 여부를 확인
        currentGold = GameManager.Instance.CompareValue(GameManager.Instance.gold, questGoals[currentQuestIndex]);

        if (currentGold >= questGoals[currentQuestIndex] && !isQuestCompleted)
        {
            CompleteQuest();
        }

        UpdateQuestStatus();
    }

    // 퀘스트 상태를 업데이트하는 함수
    void UpdateQuestStatus()
    {
        if (!isQuestCompleted)
        {
            questText.text = $"목표:\n" +
                $" {currentGold} / {questGoals[currentQuestIndex]}";
        }
        else
        {
            questText.text = "목표 달성";
        }
    }

    // 퀘스트 완료 시 호출되는 함수
    void CompleteQuest()
    {
        isQuestCompleted = true;

        // 다음 퀘스트 목표로 이동
        currentQuestIndex++;

        // 완료 메시지 표시
        StartCoroutine(ShowCompletionText());

        if (currentQuestIndex < questGoals.Length)
        {
            // 다음 퀘스트가 있으면 진행
            isQuestCompleted = false;
        }
        else
        {
            // 모든 퀘스트 완료
            questText.text = "모든 목표 달성";
        }
        // 퀘스트 완료 메시지를 잠시 동안만 표시하는 코루틴
        IEnumerator ShowCompletionText()
        {
            completionText.gameObject.SetActive(true);
            completionText.text = $"목표 달성! 보상 지급";
            yield return new WaitForSeconds(2f); // 2초 동안 표시
            completionText.gameObject.SetActive(false);
        }
    }
}
