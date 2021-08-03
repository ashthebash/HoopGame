using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public static BallManager Instance { get; private set; }

    public GameObject ballPrefab;
    GameObject currentBall;
    [Range(.2f, 5f)]
    public float zDistance = 2f;
    private List<Vector3> posHistory = new List<Vector3>();


    Plane plane;

    float throwTollerance = .01f;
    float throwMultiplier = 1300f;

    public List<Vector3> PosHistory { get => posHistory; set => posHistory = value; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {
        StartCoroutine(GetNewBall());
    }

    /// <summary>
    /// Destroy current ball if there is one and then spawn a new ball with collider disabled.
    /// </summary>
    IEnumerator GetNewBall()
    {
        yield return new WaitForSecondsRealtime(2f);

        currentBall = Instantiate(ballPrefab, ballPrefab.transform.position, Quaternion.identity);
        currentBall.GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBall != null)
        {
            transform.position = currentBall.transform.position; //  Set Manager to position of current ball.

        }

    }

    private void OnMouseDown()
    {
        if (currentBall == null)
        {
            return;
        }


        plane.SetNormalAndPosition(Camera.main.transform.forward, currentBall.transform.position);

        PosHistory.Clear();
        PosHistory.Add(currentBall.transform.position);
    }

    private void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float dist;
        plane.Raycast(ray, out dist);
        Vector3 mousePos = ray.GetPoint(dist);
        mousePos.z = currentBall.transform.position.z;

        currentBall.transform.position = mousePos;

        if (PosHistory.Count > 0)
        {
            if (PosHistory.Count > 4)
            {
                if (Vector3.Distance(currentBall.transform.position, PosHistory[PosHistory.Count - 1]) >= throwTollerance)
                {
                    PosHistory.RemoveAt(0);
                    PosHistory.Add(currentBall.transform.position);

                }
            }
            else
            {
                if (Vector3.Distance(currentBall.transform.position, PosHistory[PosHistory.Count - 1]) >= throwTollerance)
                {
                    PosHistory.Add(currentBall.transform.position);
                }
            }
        }
        else
        {
            PosHistory.Add(currentBall.transform.position);
        }
    }

    private void OnMouseUp()
    {
        if (currentBall != null)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 endPos = mousePos;
            endPos.z = currentBall.transform.position.z - Camera.main.transform.position.z;
            endPos = Camera.main.ScreenToWorldPoint(endPos);

            int lastPosIndex = PosHistory.Count - 2;

            if (lastPosIndex < 0)
            {
                lastPosIndex = 0;
            }

            Vector3 throwVector = endPos - PosHistory[lastPosIndex];

            if (throwVector.magnitude < .09f)
            {
                currentBall.GetComponent<Ball>().StartCoroutine("ResetBall");
                return;
            }

            throwVector.z = throwVector.magnitude;
            //throwForce.y /= 2f;
            //throwForce.x /= 2f;

            Rigidbody ballRb = currentBall.GetComponent<Rigidbody>();
            currentBall.GetComponent<Collider>().enabled = true;
            ballRb.useGravity = true;
            ballRb.AddForce(throwVector * throwMultiplier);

            StartCoroutine(GetNewBall());
        }
    }
}


