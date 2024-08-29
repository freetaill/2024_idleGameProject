using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject goodsIcon; // 목표 위치
    [Header("퍼지는 정도")]
    public float scatterRadius = 1.5f; // 퍼지는 반경
    public float scatterDuration = 0.8f; // 퍼지는 시간
    public float scatterSpeed = 0.5f; // 퍼지는 속도 조절
    [Header("목표까지 이동")]
    public float moveDuration = 0.5f; // 목표 위치로 이동하는 시간
    public float moveSpeed = 3f; // 목표로 이동하는 속도 조절

    private Vector3 startPosition;
    private Vector3 scatterPosition;
    private float startTime;

    private void Start()
    {
        startPosition = transform.position;
        scatterPosition = startPosition + Random.insideUnitSphere * scatterRadius;
        scatterPosition.z = startPosition.z; // Z축을 같게 설정

        StartCoroutine(ScatterAndMoveToTarget());
    }
    private IEnumerator ScatterAndMoveToTarget()
    {
        // 퍼지는 애니메이션
        float elapsedTime = 0f;
        while (elapsedTime < scatterDuration)
        {
            float t = elapsedTime / scatterDuration;
            transform.position = Vector3.Lerp(startPosition, scatterPosition, t * scatterSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = scatterPosition;

        // 목표 위치로 이동하는 애니메이션
        elapsedTime = 0f;
        Vector3 originalPosition = transform.position;
        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;

            // Lerp 함수의 마지막 인자는 t값에 따라 0에서 1까지 변함
            transform.position = Vector3.Lerp(originalPosition, goodsIcon.transform.position, t * moveSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = goodsIcon.transform.position; 

        // 목표에 도착하면 사라짐
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GoodsIcon"))
        {
            Destroy(this.gameObject);
        }
    }
}
