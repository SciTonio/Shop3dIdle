using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private int score = 0;
    public Action<int> OnScore;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Add(int val)
    {
        score += val;
        OnScore?.Invoke(score);
    }

    public int GetScore() => score;
}
