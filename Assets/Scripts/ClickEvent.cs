using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickEvent : MonoBehaviour
{
    // 마우스 위치에서 가져와야할 정보
    // 1. 마우스 아래에 UI가 있는가
    // 2. 마우스 아래에 특정한 오브젝트가 있는가
    // 마우스의 위치 정보는 실시간으로 가져올 것
    // 만약 마우스 아래에 UI가 존재한다면 배경에서 동작해야하는 특정 오브젝트 클릭 밑 화면 좌우 이동, 확대 축소 등의 기능 정지
    // 만약 마우스 클릭 시점에 특정 오브젝트가 존재하는 경우에는 해당 오브젝트가 이벤트를 수행함
    // 만약 마우스 클릭 시점에 아무것도 없다면 화면 좌우 이동 및 확대 축소 수행
    // 만약 마우스 클릭 이후 일정 시간 내에 한번 더 클릭이 수행된다면 더블 클릭 이벤트 수행

    public Camera mainCamera; // 메인 카메라, 필요 시 인스펙터에서 할당
    private float doubleClickTimeLimit = 0.25f; // 더블 클릭 감지 시간 제한
    private float lastClickTime = -1; // 마지막 클릭 시간을 기록

    Vector3 mousePosition;
    private bool isDragging = false;
    private Vector3 dragStartPosition; // 드래그 시작 시점의 마우스 위치를 저장할 변수
    public float cameraMoveRange_x = 6.0f; // x축 이동 범위

    public GameObject specialPreach; // 특수 이벤트 오브젝트
    GameObject SpecialPreach;

    public GameObject Ingredients; // 건물 이벤트용 오브젝트

    public GameObject coinimage;


    void Update()
    {
        // 1. 마우스 위치 실시간 가져오기
        mousePosition = Input.mousePosition;
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition); // 월드 좌표로 변환

        // 2. 마우스 아래에 UI가 있는지 확인
        if (IsPointerOverUIElement())
        {
            // 마우스가 UI 위에 있을 때는 오브젝트 클릭 및 화면 이동/확대 축소 기능 정지
            return;
        }

        HandleScreenZoomAndOut();

        // 3. 마우스 아래에 특정 오브젝트가 있는지 확인
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 클릭 시
        {
            Vector2 mouseWorldPosition2D = new Vector2(worldPosition.x, worldPosition.y);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition2D, Vector2.zero); // 2D 레이캐스트 사용

            if (hit.collider != null)
            {
                // 특정 오브젝트가 존재하는 경우
                Debug.Log("Clicked on " + hit.collider.name);
                // 해당 오브젝트의 이벤트 처리
                HandleObjectClick(hit.collider.gameObject);
            }
            else
            {
                // 클릭한 위치에 오브젝트가 없는 경우 화면 이동 및 확대/축소 수행
                isDragging = true;
                dragStartPosition = Input.mousePosition; // 드래그 시작 시점의 마우스 위치 저장
            }

            // 4. 더블 클릭 감지
            if (Time.time - lastClickTime < doubleClickTimeLimit)
            {
                HandleDoubleClick();
                lastClickTime = -1; // 더블 클릭 후 초기화
            }
            else
            {
                lastClickTime = Time.time; // 첫 클릭 시간 기록
            }
        }

        // 마우스 왼쪽 버튼을 떼면 드래그 중지
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            // 마우스의 현재 위치와 시작 위치의 차이를 계산
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 difference = dragStartPosition - currentMousePosition;

            // 카메라의 위치를 마우스 드래그에 따라 좌우로 이동
            Vector3 move = new Vector3(difference.x * 2.0f * Time.deltaTime, 0, 0);
            if (Mathf.Abs(mainCamera.transform.position.x + move.x) <= cameraMoveRange_x)
            {
                mainCamera.transform.Translate(move, Space.World);
            }

            // 드래그 시작 위치를 현재 위치로 업데이트
            dragStartPosition = currentMousePosition;
        }
    }

    // 마우스가 UI 위에 있는지 확인하는 함수
    private bool IsPointerOverUIElement()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    // 특정 오브젝트를 클릭했을 때의 처리
    private void HandleObjectClick(GameObject clickedObject)
    {
        // 오브젝트 클릭 시 수행할 동작 구현
        Debug.Log($"Event triggered for {clickedObject.name}");

        if (clickedObject.CompareTag("SubTemple"))
        {
            Ingredients.GetComponent<Coin>().goodsIcon = coinimage;
            Instantiate(Ingredients, clickedObject.gameObject.transform.position + new Vector3(0, 2f), Quaternion.identity);
        }
    }

    // 화면 이동 및 확대/축소 처리 함수
    private void HandleScreenMovement()
    {
        Debug.Log("Screen move or zoom triggered.");
        // 화면 이동 동작 구현
    }

    private void HandleScreenZoomAndOut()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        // 카메라의 FOV 값을 조정하여 줌 인/아웃
        mainCamera.orthographicSize -= scrollInput * 2f;
        cameraMoveRange_x += scrollInput * 3f;

        if (Mathf.Abs(mainCamera.transform.position.x) >= cameraMoveRange_x)
        {
            if (mainCamera.transform.position.x > 0)
            {
                mainCamera.transform.position = new Vector3(cameraMoveRange_x, 0, -10);
            }
            else
            {
                mainCamera.transform.position = new Vector3(-cameraMoveRange_x, 0, -10);
            }
        }

        // FOV 값 제한
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, 5f, 8f);
        cameraMoveRange_x = Mathf.Clamp(cameraMoveRange_x, 12f, 14f);
    }

    // 더블 클릭 처리 함수
    private void HandleDoubleClick()
    {
        Debug.Log("Double-click detected.");
        // 더블 클릭 이벤트 처리 구현
        Vector2 mouse = Input.mousePosition;
        mouse = mainCamera.ScreenToWorldPoint(mouse);
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