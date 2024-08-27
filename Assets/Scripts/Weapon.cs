using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Weapon : MonoBehaviour
{
    public Sprite[] sprites; // 스프라이트 배열
    public float frameRate = 10f; // 초당 프레임 수
    public bool loop = false; // 애니메이션 반복 여부
    public float scaleMultiplier = 1.5f; // 애니메이션 중단 시 크기 배율
    public float scaleDuration = 0.5f; // 크기 변경 시간
    public float scaleBackDuration = 0.5f; // 크기 되돌리기 시간

    private SpriteRenderer spriteRenderer;
    private float timer;
    private int currentFrame;
    private float frameDuration;
    private Vector3 originalScale;
    private Vector3 targetScale;
    private bool isScalingUp = false;
    private bool isScalingDown = false;
    private float scaleTimer;

    //panel 가시성
    public GameObject WeaponPanel;

    //버튼이 0이면 Get, 버튼이 1이면 Stop
    private int buttonState;

    //Get 인지 Stop인지를 표지할 텍스트
    public TextMeshProUGUI WeaponControlText;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale; // 처음 객체의 스케일 저장

        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[0];
            frameDuration = 1f / frameRate;
        }
        buttonState = 0;
        WeaponPanel.SetActive(false);
        spriteRenderer.enabled = false;
    }

    public void OnClickWeaponButton()
    {
        //Get일때 누르면 이제 움직여야 함
        if (buttonState == 0)
        {
            timer = 0f;
            buttonState = 1;
            loop = true;
            WeaponControlText.text = "Stop";
        }
        //Stop일 때 누르면 움직임 스탑
        else if(buttonState == 1)
        {
            Debug.Log("1인데 눌림");
            loop = false;
            timer = 0f;
            buttonState = 0;
            WeaponControlText.text = "Get";
            StartScalingUp();
        }
    }
    public void OnClickCloseButton()
    {
        WeaponPanel.SetActive(false);
        loop = false;
        spriteRenderer.enabled = false;
        enabled = false;
        buttonState = 0;
    }
    public void OnClickOpenButton()
    {
        WeaponPanel.SetActive(true);
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = sprites[currentFrame];
        enabled = true;
        buttonState = 0;
    }

    void Update()
    {
        Debug.Log("버튼 상태 : " + buttonState);
            // 크기 증가 및 감소 처리
        if (isScalingUp)
        {
            ScaleUp();
        }

        if (isScalingDown)
        {
            ScaleDown();
        }
        if (loop && sprites.Length > 0)
        {
            timer += Time.deltaTime;
            if (timer >= frameDuration)
            {
                timer -= frameDuration;
                currentFrame = (currentFrame + 1) % sprites.Length;
                spriteRenderer.sprite = sprites[currentFrame];
            }
        }
    }

    private void StartScalingUp()
    {
        targetScale = originalScale * scaleMultiplier; // 목표 크기 계산
        isScalingUp = true; // 크기 증가 시작 플래그 설정
        scaleTimer = 0f; // 타이머 리셋
    }

    private void ScaleUp()
    {
        scaleTimer += Time.deltaTime;
        float progress = Mathf.Clamp01(scaleTimer / scaleDuration);
        transform.localScale = Vector3.Lerp(originalScale, targetScale, progress);

        if (progress >= 1f)
        {
            isScalingUp = false; // 크기 증가 완료
            StartScalingDown(); // 크기 되돌리기 시작
        }
    }

    private void StartScalingDown()
    {
        isScalingDown = true; // 크기 감소 시작 플래그 설정
        scaleTimer = 0f; // 타이머 리셋
    }

    private void ScaleDown()
    {
        scaleTimer += Time.deltaTime;
        float progress = Mathf.Clamp01(scaleTimer / scaleBackDuration);
        transform.localScale = Vector3.Lerp(targetScale, originalScale, progress);

        if (progress >= 1f)
        {
            isScalingDown = false; // 크기 감소 완료
            // 필요시 이 시점에서 GameObject 비활성화 등 추가 로직 처리 가능
        }
    }
}
