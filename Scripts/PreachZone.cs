using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreachZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Citizen"))
        {
            // 신도가 시민 감지 시 양측에 신호 전송
            Debug.Log("감지");
            transform.parent.gameObject.GetComponent<Believer>().OnTriggerCheckCitizen();
            collision.transform.gameObject.GetComponent<Citizen>().Triggered();
        }
    }
}
