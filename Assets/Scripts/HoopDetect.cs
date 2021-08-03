using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopDetect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            ScoreManager.Instance.AddScore(1);
        }
    }
}
