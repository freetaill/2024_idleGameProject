using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Roulette : MonoBehaviour
{
    //public
    public Sprite[] sprites; // 스프라이트 배열
    public bool loop;
    public int roletteCount  = 6; //룰렛 개수
    public float RotateSpeed = 500f;

    //private
    private Image[] images;
    private Vector3 rotationCenter;
    private GameObject ResultPanel;
    private TextMeshProUGUI RouletteControl;
    private bool IsSlowing = false;
    private int buttonState;
    private float currentSpeed;


    void Start()
    {
        images = new Image[roletteCount];
        buttonState = 0;
        gameObject.SetActive(false);
        loop = false;
        ResultPanel = transform.GetChild(6).gameObject;
        ResultPanel.SetActive(false);
        RouletteControl = transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        rotationCenter = transform.GetChild(1).position;
        currentSpeed = RotateSpeed;

        for(int i = 0;i<roletteCount;i++)
        {
            //이미지 가져오기
            images[i] = transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<Image>();
            images[i].sprite = sprites[i];
        }
    }

    public void OnClickRouletteControllButton()
    {
        //시작일때 누르면 이제 움직여야 함
        if (buttonState == 0)
        {
            buttonState = 1;
            loop = true;
            RouletteControl.text = "Stop";
        }
        //Stop일 때 누르면 움직임 스탑
        else if(buttonState == 1)
        {
            IsSlowing = true;
        }
    }
    public void OnClickCloseButton()
    {
        gameObject.SetActive(false);
        loop = false;
    }
    public void OnClickOpenButton()
    {
        gameObject.SetActive(true);
        buttonState = 0;
        RouletteControl.text = "시작 !";
    }

    void Rotate()
    {
        /*
        for(int i = 0;i<roletteCount;i++)
        {
            Vector3 direction = transform.GetChild(0)GetChild(i).transform.position - rotationCenter;
            float angle = currentSpeed * Time.deltaTime;

            // 중심점을 기준으로 회전
            transform.GetChild(i).transform.RotateAround(rotationCenter, Vector3.forward, angle);
        }
        */
        transform.GetChild(0).Rotate(0f, 0f, -currentSpeed * Time.deltaTime);
    }
    void FindClosestSlot()
    {
        // 스핀점과 가장 가까운 슬롯을 찾기
        Vector3 trianglePosition = transform.GetChild(4).transform.position;
        float closestDistance = float.MaxValue;
        int closestIndex = 0;

        for(int i = 0;i<roletteCount;i++)
        {
            float distance = Vector3.Distance(trianglePosition, transform.GetChild(0).GetChild(i).transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }
        ResultPanel.SetActive(true);
        ResultPanel.transform.GetChild(0).GetComponent<Image>().sprite = transform.GetChild(0).GetChild(closestIndex).GetChild(0).GetComponent<Image>().sprite;
    }
    public float deceleration = 100f;

    void Update()
    {
        if (loop){
            if (IsSlowing)
                currentSpeed -= deceleration * Time.deltaTime;
            // 속도가 0 이하로 떨어지면 회전을 멈춤
            if (currentSpeed <= 0f)
            {
                FindClosestSlot();
                currentSpeed = RotateSpeed;
                IsSlowing = false;
                loop = false;
                RouletteControl.text = "시작 !";
                buttonState = 0;
            }
            else
                Rotate();
            Debug.Log("위치 : "  + transform.GetChild(0).GetChild(0).transform.position);
        }
    }
    public void OnClickGetButtion()
    {
        ResultPanel.SetActive(false);
    }
}
