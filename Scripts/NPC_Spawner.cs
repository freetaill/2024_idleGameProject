using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Spawner : MonoBehaviour
{
    public GameObject Citizen;
    public int population;
    public int Gold;

    float delayCount = 0;
    float delaytime = 1;
    // Start is called before the first frame update
    void Start()
    {
        delayCount = delaytime;
    }

    // Update is called once per frame
    void Update()
    {
        //랜덤한 딜레이 마다 시민 소환
        delayCount += Time.deltaTime;
        if (delayCount > delaytime)
        {
            Instantiate(Citizen, transform.position, Quaternion.identity);
            delayCount = 0;
            delaytime = Random.Range(1.7f, 4.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Citizen"))
        {
            //시민이 끝으로 오면 삭제
            Destroy(collision.gameObject);
        }
    }
}
