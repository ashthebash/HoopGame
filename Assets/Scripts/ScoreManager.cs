using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int Score { get; set; }
    public Text CounterText;


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

        Score = 0;
    }

    public void AddScore (int scoreToAdd)
    {
        Score+= scoreToAdd;
        CounterText.text = "Score : " + Score;
    }
}
