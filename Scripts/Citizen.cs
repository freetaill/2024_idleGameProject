using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : MonoBehaviour
{
    // 이동속도
    public int speed = 1;

    // 재이동 대기시간
    float delayCount = 0;

    //신도에게 감지 되었는지 여부
    bool checkBelieverFlag = false;

    Vector3 dir;

    void Start()
    {
        // 위치 초기 설정
        if (transform.position.x < 0) { dir = new Vector3(1, 0, 0); }
        else { dir = new Vector3(-1, 0, 0); }
        transform.position += dir * 0.4f + new Vector3(0, -0.194f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (checkBelieverFlag)
        {
            delayCount += Time.deltaTime;
            if (delayCount > 2.0f)
            {
                // 신도로 전향되었을 때 동작
                TurnedCitizen();
            }
        }
        else
        {
            transform.position += dir * Time.deltaTime * speed;
        }
    }

    void TurnedCitizen()
    {
        // 신도가 된 시민으로 변경하는 함수
        gameObject.tag = "TurnedCitizen";
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        checkBelieverFlag = false;
        GameObject Temple = GameObject.FindGameObjectWithTag("MainTemple");
        if (Temple.transform.position.x - transform.position.x > 0)
        {
            dir = new Vector3(1, 0, 0);
        }
        else
        {
            dir = new Vector3(-1, 0, 0);
        }
    }

    public void Triggered()
    {
        // 신도에게 감지 되었을 때 동작
        checkBelieverFlag = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // 신도로 전향된 상태로 신전의 중앙에 도착하면 오브젝트 파괴 밑 신도 수 증가
        if (collision.CompareTag("MainTemple") && gameObject.CompareTag("TurnedCitizen") && Mathf.Abs(transform.position.x) < 0.1f)
        {
            GameManager.Instance.AddBeliver(1);
            Destroy(gameObject);
        }
    }
}
