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
        GetResource(transform.GetChild(0).GetChild(closestIndex).GetChild(0).GetComponent<Image>());
    }
    public void GetResource(Image s)
    {
        if (s.sprite == sprites[0])
            GameManager.Instance.AddValue(GameManager.Instance.gold,10);
        if (s.sprite == sprites[1])
            GameManager.Instance.AddValue(GameManager.Instance.bread,10);
        if (s.sprite == sprites[2])
            GameManager.Instance.AddValue(GameManager.Instance.rock,10);
        if (s.sprite == sprites[3])
            GameManager.Instance.AddValue(GameManager.Instance.tree,10);
        if (s.sprite == sprites[4])
            GameManager.Instance.AddValue(GameManager.Instance.beliver,10);
        if (s.sprite == sprites[5])
            GameManager.Instance.AddValue(GameManager.Instance.gold,10);
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
            SoundManager.Instance.Play("whick03");
        }
    }
    public void OnClickGetButtion()
    {
        ResultPanel.SetActive(false);
        SoundManager.Instance.Play("pop1");
    }
}
