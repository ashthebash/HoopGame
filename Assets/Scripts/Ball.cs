using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Vector3 startPos;

    private void Awake()
    {
        startPos = transform.position;
    }

    IEnumerator ResetBall()
    {
        yield return null;

        transform.position = startPos;
        BallManager.Instance.PosHistory.Clear();
        BallManager.Instance.PosHistory.Add(transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Manager"))
        {
            Destroy(this.gameObject, 5f);
        }
    }
}
