using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopMove : MonoBehaviour
{

    public enum Axis { X, Y, Z}

    public Axis axis;

    [Range(.1f, 5f)]
    public float speed = .25f;

    private int distance = 17;

    float moveDelay;

    bool isMoving = false;

    private void Awake()
    {
        StartCoroutine(AddDelay());
    }

    IEnumerator AddDelay()
    {
        while (!isMoving)
        {
            moveDelay = Time.time;
            yield return null;
        }
    }    

    // Update is called once per frame
    void Update()
    {
        float moveTime = Time.time;

        if (ScoreManager.Instance.Score >= 2)
        {
            isMoving = true;

            float movement = Mathf.Sin(speed * (Time.time - moveDelay));

            switch (axis)
            {
                case Axis.X:
                    transform.position = new Vector3(movement * distance, transform.position.y, transform.position.z);
                    break;
                case Axis.Y:
                    transform.position = new Vector3(transform.position.x, movement * distance, transform.position.z);
                    break;
                case Axis.Z:
                    transform.position = new Vector3(transform.position.x, transform.position.y, movement * distance);
                    break;
                default:
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                    break;
            }
        }
    }
}
