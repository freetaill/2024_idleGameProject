using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMoveControl : MonoBehaviour
{
    public Camera camera;
    public float cameraMoveRange_x = 6.0f; // x축 이동 범위

    public float dragSpeed = 2.0f; // 마우스 드래그 속도 조정
    private Vector3 dragOrigin;    // 마우스 드래그 시작 위치
    private bool isDragging = false;

    private bool isUpgradeArea = false;

    void Update()
    {
        // 마우스 왼쪽 버튼을 눌렀을 때 드래그 시작
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            isDragging = true;
        }

        // 마우스 왼쪽 버튼을 떼면 드래그 중지
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // 드래그 중일 때
        if (isDragging)
        {
            // 마우스의 현재 위치와 시작 위치의 차이를 계산
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 difference = dragOrigin - currentMousePosition;

            // 카메라의 위치를 마우스 드래그에 따라 좌우로 이동
            Vector3 move = new Vector3(difference.x * dragSpeed * Time.deltaTime, 0, 0);
            if (Mathf.Abs(camera.transform.position.x + move.x) <= cameraMoveRange_x)
            {
                camera.transform.Translate(move, Space.World);
            }

            // 드래그 시작 위치를 현재 위치로 업데이트
            dragOrigin = currentMousePosition;
        }

        if (isUpgradeArea)
        {
            // 카메라의 현재 Y 위치 확인
            if (camera.transform.position.y == 0.0f)
            {
                // 0에서 -1.2f로 이동하는 코루틴 시작
                StartCoroutine(MoveCamera(-1.2f));
            }
            else if (camera.transform.position.y == -1.2f)
            {
                // -1.2f에서 0으로 이동하는 코루틴 시작
                StartCoroutine(MoveCamera(0.0f));
            }
            else
            {
                isUpgradeArea = false;
            }
        }
    }

    IEnumerator MoveCamera(float targetY)
    {
        Vector3 startPosition = camera.transform.position;
        Vector3 targetPosition = new Vector3(startPosition.x, targetY, startPosition.z);
        float elapsedTime = 0.0f;

        // 부드럽게 이동
        while (elapsedTime < 3.0f)
        {
            camera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / 1.0f);
            elapsedTime += Time.deltaTime * 5.0f;
            yield return null;
        }

        // 최종 위치를 정확히 설정
        camera.transform.position = targetPosition;
        isUpgradeArea = false;
    }

    public void k()
    {
        isUpgradeArea = true;
    }
}
