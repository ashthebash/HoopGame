using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    BallManager manager;

    Vector3 startPos;

    private void Awake()
    {
        manager = BallManager.Instance;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ResetBall()
    {
        yield return null;

        transform.position = startPos;
        manager.PosHistory.Clear();
        manager.PosHistory.Add(transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Manager"))
        {
            
            Destroy(this.gameObject, 5f);
        }
    }
}
