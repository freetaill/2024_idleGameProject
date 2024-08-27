using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Believer : MonoBehaviour
{
    GameObject Temple;

    public float speed = 1;

    // 활동 재시작 대기시간
    float delayCount = 0;

    // 시민과 접촉 여부
    bool checkCitizenFlag = false;

    Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        // 초기 설정
        dir = new Vector3(1, 0, 0);
        transform.position += new Vector3(0, -0.194f, 0);
        Temple = GameObject.FindGameObjectWithTag("MainTemple");
    }

    // Update is called once per frame
    void Update()
    {
        delayCount += Time.deltaTime;
        // 신전과 신도 사이 거리 측정
        CheckDistencetoTemple();

        if (checkCitizenFlag)
        {
            if (delayCount > 2.0f)
            {
                //다시 움직이기 시작
                checkCitizenFlag = false;
            }
        }
        else
        {
            if (delayCount > 3.0f)
            {
                // 전도 시작
                transform.GetChild(0).gameObject.SetActive(true);
                delayCount = 0.0f;
            }
            transform.position += dir * Time.deltaTime * speed;
        }
    }

    void CheckDistencetoTemple()
    {
        // 신전과 신도 사이의 거리를 구하여 일정 범위 내에서 활동하도록 하는 함수
        float Templedir = Temple.transform.position.x - transform.position.x;
        if (Templedir > 2.0f) { dir = new Vector3(1, 0, 0); }
        else if (Templedir < -2.0f) { dir = new Vector3(-1, 0, 0); }
    }
    
    public void OnTriggerCheckCitizen()
    {
        // PreachZone에서 시민을 감지했을때 동작
        checkCitizenFlag = true;
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
