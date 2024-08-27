using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpecialEvent : MonoBehaviour
{
    public GameObject specialPreach;
    GameObject SpecialPreach;
    new Camera camera;

    float lastClickTime = 0f;
    float DoubleClickDelay = 0.3f;
    int clickCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 왼쪽 버튼 클릭 감지
        if (Input.GetMouseButtonDown(0))
        {
            clickCount++;

            if (clickCount == 1)
            {
                lastClickTime = Time.time;
            }
            else if (clickCount == 2)
            {
                // 클릭 간격이 doubleClickThreshold 이하일 때 더블클릭으로 간주
                if (Time.time - lastClickTime < DoubleClickDelay)
                {
                    // 더블클릭 이벤트 실행
                    Spawn_SpacialPreach();
                }
                clickCount = 0;
            }
        }

        // 일정 시간 동안 두 번째 클릭이 없으면 카운트 리셋
        if (clickCount == 1 && Time.time - lastClickTime > DoubleClickDelay)
        {
            clickCount = 0;
        }
    }

    void Spawn_SpacialPreach()
    {
        Vector2 mouse = Input.mousePosition;
        mouse = camera.ScreenToWorldPoint(mouse);
        mouse.y = 0;
        Debug.Log("이벤트");
        SpecialPreach = Instantiate(specialPreach, mouse, Quaternion.identity);
        Invoke("S_break", 0.2f);
    } 

    void S_break()
    {
        Destroy(SpecialPreach.gameObject);
    }
}
