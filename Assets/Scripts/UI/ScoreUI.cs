using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TMP_Text score;

    void Start()
    {
        ScoreManager.Instance.OnScore += (val) => score.text = $"{val}";
    }

}
